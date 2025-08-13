using Minigames;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Contexts;

namespace UI {
	public class LevelEndPanel : MonoBehaviour {
		[SerializeField] private Button restartButton;
		[SerializeField] private Button menuButton;

		// Dependencies
		private MinigameLoader minigameLoader;

		private void Start() {
			minigameLoader = SceneContext.GetInstance().Get<MinigameLoader>();

			restartButton.onClick.AddListener(OnRestartButtonClick);
			menuButton.onClick.AddListener(OnMenuButtonClick);
		}

		private void OnMenuButtonClick() {
			MinigameDefinition minigameDefinition = minigameLoader.GetActiveMinigameDefinition();
			if (minigameDefinition is null)
				return;

			minigameLoader.UnloadMinigame(minigameDefinition);
		}

		private void OnRestartButtonClick() {
			MinigameDefinition minigameDefinition = minigameLoader.GetActiveMinigameDefinition();
			if (minigameDefinition is null)
				return;

			minigameLoader.RestartMinigame(minigameDefinition);
		}
	}
}
