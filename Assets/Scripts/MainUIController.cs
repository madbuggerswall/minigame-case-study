using Minigames;
using Minigames.Loader;
using Minigames.Loader.Signals;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Contexts;
using Utilities.Signals;

public class MainUIController : MonoBehaviour, IInitializable {
	[SerializeField] private VerticalLayoutGroup layoutGroup;
	[SerializeField] private MinigameButton minigameButtonPrefab;

	// Dependencies
	private MinigameDefinitionManager minigameDefinitionManager;
	private MinigameLoader minigameLoader;
	private SignalBus signalBus;

	public void Initialize() {
		signalBus = SceneContext.GetInstance().Get<SignalBus>();
		minigameDefinitionManager = SceneContext.GetInstance().Get<MinigameDefinitionManager>();
		minigameLoader = SceneContext.GetInstance().Get<MinigameLoader>();
		
		signalBus.SubscribeTo<MinigameLoadedSignal>(OnMinigameLoad);
		signalBus.SubscribeTo<MinigameUnloadedSignal>(OnMinigameUnload);
		
		InitializeMinigameButtons();
	}

	private void InitializeMinigameButtons() {
		MinigameDefinition[] minigameDefinitions = minigameDefinitionManager.GetMinigameDefinitions();
		for (int i = 0; i < minigameDefinitions.Length; i++) {

			MinigameDefinition minigameDefinition = minigameDefinitions[i];
			MinigameButton minigameButton = Instantiate(minigameButtonPrefab, layoutGroup.transform);
			minigameButton.Initialize(minigameDefinition);

			Button button = minigameButton.GetButton();
			button.onClick.AddListener(delegate { minigameLoader.LoadMinigame(minigameDefinition); });
		}
	}

	private void OnMinigameLoad(MinigameLoadedSignal signal) {
		layoutGroup.gameObject.SetActive(false);
	}

	private void OnMinigameUnload(MinigameUnloadedSignal signal) {
		layoutGroup.gameObject.SetActive(true);
	}
}
