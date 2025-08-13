using System.IO;
using Snake.Level;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Persistence;

namespace Snake.Persistence {
	public class SnakeSaveManager : IInitializable {
		private const string ScoreDataFileName = "ScoreData.dat";
		public void Initialize() { }

		private string GetScoreDataFilePath() {
			return Path.Combine(Application.persistentDataPath, ScoreDataFileName);
		}

		private bool ScoreDataFileExists() {
			return BinaryJsonUtility.Exists(GetScoreDataFilePath());
		}

		public void SaveScoreData(ScoreManager scoreManager) {
			ScoreDTO scoreDTO = new ScoreDTO(scoreManager);
			BinaryJsonUtility.Save(scoreDTO, GetScoreDataFilePath());
		}

		public bool TryLoadScoreData(out ScoreDTO scoreDTO) {
			scoreDTO = null;
			if(!ScoreDataFileExists())
				return false;
		
			scoreDTO = BinaryJsonUtility.Load<ScoreDTO>(GetScoreDataFilePath());
			return true;
		}
	}
}
