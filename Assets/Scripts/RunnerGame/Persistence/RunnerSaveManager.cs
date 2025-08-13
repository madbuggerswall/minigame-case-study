using System.IO;
using RunnerGame.Mechanics;
using SnakeGame.Persistence;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Persistence;

namespace RunnerGame.Persistence {
    public class RunnerSaveManager : IInitializable
    {
        private const string ScoreDataFileName = "RunnerScoreData.dat";

        public void Initialize() { }

        private string GetScoreDataFilePath() {
            return Path.Combine(Application.persistentDataPath, ScoreDataFileName);
        }

        private bool ScoreDataFileExists() {
            return BinaryJsonUtility.Exists(GetScoreDataFilePath());
        }

        // TODO
        public void SaveScoreData(RunnerScoreManager runnerScoreManager) {
            RunnerScoreDTO snakeScoreDTO = new(runnerScoreManager);
            BinaryJsonUtility.Save(snakeScoreDTO, GetScoreDataFilePath());
        }

        public bool TryLoadScoreData(out SnakeScoreDTO snakeScoreDTO) {
            snakeScoreDTO = null;
            if (!ScoreDataFileExists())
                return false;

            snakeScoreDTO = BinaryJsonUtility.Load<SnakeScoreDTO>(GetScoreDataFilePath());
            return true;
        }
    }
}
