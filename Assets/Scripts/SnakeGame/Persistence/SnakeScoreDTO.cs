using SnakeGame.Level;
using UnityEngine;

namespace SnakeGame.Persistence {
	[System.Serializable]
	public class SnakeScoreDTO {
		[SerializeField] private int highScore;

		public SnakeScoreDTO(SnakeScoreManager snakeScoreManager) {
			this.highScore = snakeScoreManager.GetHighScore();
		}

		public int GetHighScore() => this.highScore;
	}
}
