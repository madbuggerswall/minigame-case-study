using System;
using Snake;
using Utilities.Contexts;
using Utilities.Signals;
using IInitializable = Utilities.Contexts.IInitializable;

public class ScoreManager : IInitializable {
	private int score = 0;
	private int highScore = 0;

	public Action<int> ScoreUpdateEvent { get; set; } = delegate { };
	public Action<int> HighScoreUpdateEvent { get; set; } = delegate { };

	private SignalBus signalBus;
	private SnakeSaveManager saveManager;

	public void Initialize() {
		this.signalBus = SceneContext.GetInstance().Get<SignalBus>();
		this.saveManager = SnakeContext.GetInstance().Get<SnakeSaveManager>();

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
		if (!saveManager.TryLoadScoreData(out ScoreDTO scoreDTO))
			return;

		highScore = scoreDTO.GetHighScore();
	}

	private void OnStartMinigameUnload(StartUnloadingMinigameSignal signal) => saveManager.SaveScoreData(this);
	private void OnStartMinigameRestart(StartRestartingMinigameSignal signal) => saveManager.SaveScoreData(this);

	public int GetScore() => score;
	public int GetHighScore() => highScore;
}
