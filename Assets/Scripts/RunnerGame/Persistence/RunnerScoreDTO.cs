using RunnerGame.Mechanics;
using UnityEngine;

namespace RunnerGame.Persistence {
	[System.Serializable]
	public class RunnerScoreDTO {
		[SerializeField] private int highScore;

		public RunnerScoreDTO(RunnerScoreManager runnerScoreManager) {
			highScore = runnerScoreManager.GetHighScore();
		}

		public int GetHighScore() => highScore;
	}
}
