using System;
using RunnerGame.Elements;
using RunnerGame.UI;
using Utilities.Contexts;

namespace RunnerGame.Level {
	public class RunnerLevelManager :  IInitializable {
		private Runner runner;
		private RunnerGrid runnerGrid;
		
		public Action LevelFailEvent { get; set; } = delegate { };
		public Action LevelSuccessEvent { get; set; } = delegate { };

		// Dependencies
		private RunnerLevelInitializer levelInitializer;
		private ObstacleGenerator obstacleGenerator;

		private RunnerUIController uiController;
		private RunnerScoreManager runnerScoreManager;

		public void Initialize() {
			levelInitializer = RunnerContext.GetInstance().Get<RunnerLevelInitializer>();
			obstacleGenerator = RunnerContext.GetInstance().Get<ObstacleGenerator>();

			uiController = RunnerContext.GetInstance().Get<RunnerUIController>();
			runnerScoreManager = RunnerContext.GetInstance().Get<RunnerScoreManager>();

			runnerGrid = levelInitializer.GetRunnerGrid();
			runner = levelInitializer.GetRunner();
		}

		// TODO Check for obstacles
		public void OnPlayerHitObstacle() {
			LevelFailEvent.Invoke();
		}

		public void OnObstacleRowSpawned() {
			runnerScoreManager.IncrementScore();
		}

		public Runner GetRunner() { return runner; }
		public RunnerGrid GetRunnerGrid() { return runnerGrid; }
	}
}
