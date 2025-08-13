using Minigames;
using Minigames.Loader;
using RunnerGame.Mechanics;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Contexts;

namespace RunnerGame.UI {
	public class RunnerUIController : MonoBehaviour, IInitializable {
		[Header("Level State Panels")]
		[SerializeField] private LevelEndPanel levelSuccessPanel;
		[SerializeField] private LevelEndPanel levelFailPanel;

		[Header("Score Panels")]
		[SerializeField] private ScorePanel scorePanel;
		[SerializeField] private ScorePanel highScorePanel;

		[Header("Bottom")]
		[SerializeField] private Button menuButton;

		// RunnerContext Dependencies
		private RunnerScoreManager snakeScoreManager;
		private RunnerLevelManager levelManager;

		// MainContext Dependencies
		private MinigameLoader minigameLoader;

		public void Initialize() {
			snakeScoreManager = RunnerContext.GetInstance().Get<RunnerScoreManager>();
			levelManager = RunnerContext.GetInstance().Get<RunnerLevelManager>();
			minigameLoader = SceneContext.GetInstance().Get<MinigameLoader>();

			UpdateScore(snakeScoreManager.GetScore());
			UpdateHighScore(snakeScoreManager.GetHighScore());

			snakeScoreManager.ScoreUpdateEvent += UpdateScore;
			snakeScoreManager.HighScoreUpdateEvent += UpdateHighScore;

			levelManager.LevelFailEvent += ShowLevelFailPanel;
			levelManager.LevelSuccessEvent += ShowLevelSuccessPanel;

			menuButton.onClick.AddListener(OnMenuButtonClick);
		}

		private void ShowLevelSuccessPanel() {
			menuButton.interactable = false;
			levelSuccessPanel.PlayEntryTween();
		}

		private void ShowLevelFailPanel() {
			menuButton.interactable = false;
			levelFailPanel.PlayEntryTween();
		}
		
		private void OnMenuButtonClick() {
			MinigameDefinition minigameDefinition = minigameLoader.GetActiveMinigameDefinition();
			if (minigameDefinition is null)
				return;

			minigameLoader.UnloadMinigame(minigameDefinition);
		}

		private void UpdateScore(int score) => scorePanel.UpdateScore(score);
		private void UpdateHighScore(int score) => highScorePanel.UpdateScore(score);
	}
}
