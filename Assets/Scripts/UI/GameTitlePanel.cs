using Minigames;
using TMPro;
using UnityEngine;
using Utilities.Contexts;

namespace UI {
	public class GameTitlePanel : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI titleText;

		// Dependencies
		private MinigameLoader minigameLoader;

		private void Start() {
			minigameLoader = SceneContext.GetInstance().Get<MinigameLoader>();

			MinigameDefinition activeMinigameDefinition = minigameLoader.GetActiveMinigameDefinition();
			titleText.text = activeMinigameDefinition.GetMinigameName();
		}
	}
}
