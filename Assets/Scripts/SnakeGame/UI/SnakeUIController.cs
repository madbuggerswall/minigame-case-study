using Minigames;
using Minigames.Loader;
using SnakeGame.Level;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Contexts;

namespace SnakeGame.UI {
	public class SnakeUIController : MonoBehaviour, IInitializable {
		[Header("Level State Panels")]
		[SerializeField] private LevelEndPanel levelSuccessPanel;
		[SerializeField] private LevelEndPanel levelFailPanel;

		[Header("Score Panels")]
		[SerializeField] private ScorePanel scorePanel;
		[SerializeField] private ScorePanel highScorePanel;

		[Header("Bottom")]
		[SerializeField] private Button menuButton;

		// Dependencies
		private SnakeScoreManager snakeScoreManager;
		private SnakeLevelManager levelManager;

		// MainContext Dependencies
		private MinigameLoader minigameLoader;

		public void Initialize() {
			snakeScoreManager = SnakeContext.GetInstance().Get<SnakeScoreManager>();
			levelManager = SnakeContext.GetInstance().Get<SnakeLevelManager>();
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
