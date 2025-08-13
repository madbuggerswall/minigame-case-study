using UnityEngine;
using Utilities.Contexts;

public class ScoreManager : IInitializable {
	private int score = 0;
	private int highScore = 0;
	
	public void Initialize() { }

	public void IncrementScore() {
		score++;
		highScore = Mathf.Max(score, highScore);
	}
	
	public int GetScore() => score;
	public int GetHighScore() => highScore;
}
