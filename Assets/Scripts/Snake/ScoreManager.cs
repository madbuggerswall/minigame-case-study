using Utilities.Contexts;

public class ScoreManager : IInitializable {
	int score = 0;
	public void Initialize() { }
	public void IncrementScore() { score++; }
	public int GetScore() => score;
}
