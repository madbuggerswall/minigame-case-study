using System;
using RunnerGame.Elements;
using Utilities.Contexts;

namespace RunnerGame.Mechanics {
	public class RunnerLevelManager : IInitializable {
		private Runner runner;
		private RunnerGrid runnerGrid;

		public Action LevelFailEvent { get; set; } = delegate { };
		public Action LevelSuccessEvent { get; set; } = delegate { };

		// Dependencies
		private RunnerLevelInitializer levelInitializer;
		private ObstacleGenerator obstacleGenerator;

		private RunnerScoreManager runnerScoreManager;

		public void Initialize() {
			levelInitializer = RunnerContext.GetInstance().Get<RunnerLevelInitializer>();
			obstacleGenerator = RunnerContext.GetInstance().Get<ObstacleGenerator>();
			runnerScoreManager = RunnerContext.GetInstance().Get<RunnerScoreManager>();

			runnerGrid = levelInitializer.GetRunnerGrid();
			runner = levelInitializer.GetRunner();
		}

		public void OnPlayerHitObstacle() {
			obstacleGenerator.Stop(true);
			runner.Stop(true);

			LevelFailEvent.Invoke();
		}

		public void OnObstacleRowSpawned() {
			runnerScoreManager.IncrementScore();
		}

		public Runner GetRunner() { return runner; }
		public RunnerGrid GetRunnerGrid() { return runnerGrid; }
	}
}
