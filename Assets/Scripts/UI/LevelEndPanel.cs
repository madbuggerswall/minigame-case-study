using Minigames;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Contexts;
using Utilities.Tweens.Easing;
using Utilities.Tweens.TransformTweens;

namespace UI {
	public class LevelEndPanel : MonoBehaviour {
		[SerializeField] private Button restartButton;
		[SerializeField] private Button menuButton;
		[SerializeField] private RectTransform rectTransform;

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

		private void EnableButtons(bool isEnabled) {
			restartButton.interactable = isEnabled;
			menuButton.interactable = isEnabled;
		}

		public void PlayEntryTween() {
			const float tweenDuration = 0.6f;
			
			gameObject.SetActive(true);
			rectTransform.localScale = Vector3.zero;
			EnableButtons(false);

			ScaleTween scaleTween = rectTransform.PlayScale(Vector3.one, tweenDuration);
			scaleTween.SetEase(Ease.Type.InOutQuad);
			scaleTween.SetOnComplete(delegate { EnableButtons(true); });
			scaleTween.Play();
		}
	}
}
