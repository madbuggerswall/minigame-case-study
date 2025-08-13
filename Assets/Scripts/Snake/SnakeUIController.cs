using Core.UI;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Contexts;

namespace Snake {
	public class SnakeUIController : MonoBehaviour, IInitializable {
		[Header("Level State Panels")]
		[SerializeField] private LevelEndPanel levelSuccessPanel;
		[SerializeField] private LevelEndPanel levelFailPanel;

		[Header("Score Panels")]
		[SerializeField] private ScorePanel scorePanel;
		[SerializeField] private ScorePanel highScorePanel;

		[Header("Bottom")]
		[SerializeField] private Button restartButton;
		[SerializeField] private Button menuButton;

		// Dependencies

		public void Initialize() { }

		public void ShowLevelSuccessPanel() {
			levelSuccessPanel.gameObject.SetActive(true);
		}

		public void ShowLevelFailPanel() {
			levelFailPanel.gameObject.SetActive(true);
		}

		public void UpdateScore(int score) {
			scorePanel.UpdateScore(score);
		}

		public void UpdateHighScore(int score) {
			highScorePanel.UpdateScore(score);
		}

		private void OnMenuButtonClick() {
			
		}
	}
}
