using System;
using System.Collections.Generic;
using RunnerGame.Elements;
using RunnerGame.UI;
using UnityEngine;
using Utilities.Contexts;

namespace RunnerGame.Level {
	public class RunnerLevelManager : IInitializable {
		private Runner runner;
		private List<Obstacle> obstacles;
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

		private void OnLevelMove() { }

		private void OnRunnerMove() {
			// CheckForSnakeBody();
			// CheckForWalls();
		}

		// TODO Check for obstacles
		private void CheckForObstacles() {
			LevelFailEvent.Invoke();
		}


		private void CheckForWalls() {
			if (IsInsideGrid(runner, runnerGrid))
				return;

			// TODO Make level and runner stop
			// snake.Stop(true);

			LevelFailEvent.Invoke();
		}

		private static bool IsInsideGrid(Runner runner, RunnerGrid runnerGrid) {
			Vector2Int headPosition = runner.GetGridPosition();
			Vector2Int gridSize = runnerGrid.GetGridSize();
			Vector2Int gridPosition = runnerGrid.GetGridPosition();

			int xMax = gridPosition.x + gridSize.x / 2;
			int xMin = gridPosition.x - gridSize.x / 2;
			int yMax = gridPosition.y + gridSize.y / 2;
			int yMin = gridPosition.y - gridSize.y / 2;

			int x = headPosition.x;
			int y = headPosition.y;
			return x > xMin && x < xMax && y > yMin && y < yMax;
		}
	}
}
