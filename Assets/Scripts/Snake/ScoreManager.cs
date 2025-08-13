using System;
using IInitializable = Utilities.Contexts.IInitializable;

public class ScoreManager : IInitializable {
	private int score = 0;
	private int highScore = 0;

	public Action<int> ScoreUpdateEvent { get; set; } = delegate { };
	public Action<int> HighScoreUpdateEvent { get; set; } = delegate { };

	public void Initialize() { }

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

	public int GetScore() => score;
	public int GetHighScore() => highScore;
}
