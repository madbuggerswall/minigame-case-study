using System.IO;
using SnakeGame.Level;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Persistence;

namespace SnakeGame.Persistence {
	public class SnakeSaveManager : IInitializable {
		private const string ScoreDataFileName = "ScoreData.dat";

		public void Initialize() { }

		private string GetScoreDataFilePath() {
			return Path.Combine(Application.persistentDataPath, ScoreDataFileName);
		}

		private bool ScoreDataFileExists() {
			return BinaryJsonUtility.Exists(GetScoreDataFilePath());
		}

		public void SaveScoreData(SnakeScoreManager snakeScoreManager) {
			SnakeScoreDTO snakeScoreDTO = new SnakeScoreDTO(snakeScoreManager);
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
