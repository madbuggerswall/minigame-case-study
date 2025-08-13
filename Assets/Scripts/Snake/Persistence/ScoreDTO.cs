using Snake.Level;
using UnityEngine;

namespace Snake.Persistence {
	[System.Serializable]
	public class ScoreDTO {
		[SerializeField] private int highScore;

		public ScoreDTO(ScoreManager scoreManager) {
			this.highScore = scoreManager.GetHighScore();
		}

		public int GetHighScore() => this.highScore;
	}
}
