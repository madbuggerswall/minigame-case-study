using RunnerGame.Level;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Contexts;

namespace RunnerGame.UI {
    // TODO This might be a common class
    public class RunnerUIController : MonoBehaviour, IInitializable
    {
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
        private RunnerScoreManager snakeScoreManager;
        private RunnerLevelManager levelManager;

        public void Initialize() {
            snakeScoreManager = RunnerContext.GetInstance().Get<RunnerScoreManager>();
            levelManager = RunnerContext.GetInstance().Get<RunnerLevelManager>();

            UpdateScore(snakeScoreManager.GetScore());
            UpdateHighScore(snakeScoreManager.GetHighScore());

            snakeScoreManager.ScoreUpdateEvent += UpdateScore;
            snakeScoreManager.HighScoreUpdateEvent += UpdateHighScore;

            levelManager.LevelFailEvent += ShowLevelFailPanel;
            levelManager.LevelSuccessEvent += ShowLevelSuccessPanel;
        }


        private void ShowLevelSuccessPanel() {
            levelSuccessPanel.gameObject.SetActive(true);
        }

        private void ShowLevelFailPanel() {
            levelFailPanel.gameObject.SetActive(true);
        }

        private void UpdateScore(int score) {
            scorePanel.UpdateScore(score);
        }

        private void UpdateHighScore(int score) {
            highScorePanel.UpdateScore(score);
        }

        private void OnMenuButtonClick() { }
        private void OnMenuRestartClick() { }
    }
}
