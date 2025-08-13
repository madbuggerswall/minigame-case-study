using System;
using Minigames.Loader.Signals;
using RunnerGame.Persistence;
using SnakeGame.Persistence;
using Utilities.Contexts;
using Utilities.Signals;

namespace RunnerGame.Mechanics {
    // TODO This might be a common class
    public class RunnerScoreManager : IInitializable
    {
        private int score = 0;
        private int highScore = 0;

        public Action<int> ScoreUpdateEvent { get; set; } = delegate { };
        public Action<int> HighScoreUpdateEvent { get; set; } = delegate { };

        private SignalBus signalBus;
        private RunnerSaveManager saveManager;

        public void Initialize() {
            signalBus = SceneContext.GetInstance().Get<SignalBus>();
            saveManager = RunnerContext.GetInstance().Get<RunnerSaveManager>();

            signalBus.SubscribeTo<StartUnloadingMinigameSignal>(OnStartMinigameUnload);
            signalBus.SubscribeTo<StartRestartingMinigameSignal>(OnStartMinigameRestart);

            LoadScoreData();
        }

        public void IncrementScore() {
            score++;
            ScoreUpdateEvent.Invoke(score);

            UpdateHighScore();
        }

        private void UpdateHighScore() {
            if (score <= highScore)
                return;

            highScore = score;
            HighScoreUpdateEvent.Invoke(highScore);
        }

        private void LoadScoreData() {
            if (!saveManager.TryLoadScoreData(out SnakeScoreDTO scoreDTO))
                return;

            highScore = scoreDTO.GetHighScore();
        }

        private void OnStartMinigameUnload(StartUnloadingMinigameSignal signal) => saveManager.SaveScoreData(this);
        private void OnStartMinigameRestart(StartRestartingMinigameSignal signal) => saveManager.SaveScoreData(this);

        public int GetScore() => score;
        public int GetHighScore() => highScore;
    }
}
