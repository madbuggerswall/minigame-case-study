using Core.UI;
using MatchThree.Targets;
using MatchThree.Turns;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities.Contexts;

namespace MatchThree.UI {
	public class PuzzleLevelUIController : MonoBehaviour, IInitializable {
		[SerializeField] private LevelEndPanel levelSuccessPanel;
		[SerializeField] private LevelEndPanel levelFailPanel;
		[SerializeField] private ElementTargetsPanel elementTargetsPanel;
		[SerializeField] private ScorePanel scorePanel;
		[SerializeField] private Button restartButton;

		// Dependencies
		private TargetManager targetManager;
		private TurnManager turnManager;

		public void Initialize() {
			targetManager = PuzzleContext.GetInstance().Get<TargetManager>();
			turnManager = PuzzleContext.GetInstance().Get<TurnManager>();

			elementTargetsPanel.Initialize(targetManager.GetElementTargets());
			scorePanel.UpdateScore(turnManager.GetRemainingTurnCount());

			restartButton.onClick.AddListener(OnRestartButtonClick);
		}

		public void ShowLevelSuccessPanel() {
			levelSuccessPanel.gameObject.SetActive(true);
		}

		public void ShowLevelFailPanel() {
			levelFailPanel.gameObject.SetActive(true);
		}

		public void UpdateElementTargetView(PuzzleElementTarget target) {
			elementTargetsPanel.UpdateTargetView(target);
		}

		public void UpdateRemainingTurnsPanel(int remainingMoves) {
			scorePanel.UpdateScore(remainingMoves);
		}

		private void OnRestartButtonClick() {
			Scene activeScene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(activeScene.buildIndex);
		}
	}
}
