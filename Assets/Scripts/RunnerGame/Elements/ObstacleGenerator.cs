using System.Collections.Generic;
using RunnerGame.Factories;
using RunnerGame.Level;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace RunnerGame.Elements {
	public class ObstacleGenerator : MonoBehaviour, IInitializable {
		[Header("Spawn Settings")]
		[SerializeField] private float spawnPeriod = 1;
		[SerializeField] private float movePeriod = .05f;

		[Header("Perlin Settings")]
		[SerializeField] private float obstacleRatio = 0.8f;

		private readonly List<Obstacle> spawnedObstacles = new();
		private float spawnTime = 0;
		private float moveTime = 0;

		private bool isStopped = false;

		// Dependencies
		private ObstacleFactory obstacleFactory;
		private RunnerLevelManager levelManager;
		private ObjectPool objectPool;


		public void Initialize() {
			objectPool = RunnerContext.GetInstance().Get<ObjectPool>();
			obstacleFactory = RunnerContext.GetInstance().Get<ObstacleFactory>();
			levelManager = RunnerContext.GetInstance().Get<RunnerLevelManager>();
		}

		private void Update() {
			if (isStopped)
				return;

			MoveObstaclesDownwardsPeriodically();
			SpawnRowPeriodically();
		}

		public void Stop(bool isStopped) {
			this.isStopped = isStopped;
		}

		private void MoveObstaclesDownwardsPeriodically() {
			moveTime += Time.deltaTime;
			if (moveTime < movePeriod)
				return;

			moveTime = 0;

			MoveObstaclesDownwards();
		}

		private void MoveObstaclesDownwards() {
			RunnerGrid runnerGrid = levelManager.GetRunnerGrid();
			Runner runner = levelManager.GetRunner();

			for (int i = spawnedObstacles.Count - 1; i >= 0; i--) {
				Obstacle obstacle = spawnedObstacles[i];
				Vector2Int updatedPosition = obstacle.GetGridPosition() + Vector2Int.down;
				obstacle.SetGridPosition(updatedPosition);

				// Since this is called periodically, there's a chance that runner will pass through
				if (updatedPosition == runner.GetGridPosition())
					OnPlayerHit();

				if (runnerGrid.IsInsideGrid(updatedPosition))
					continue;

				spawnedObstacles[i] = spawnedObstacles[^1];
				spawnedObstacles.RemoveAt(spawnedObstacles.Count - 1);
				objectPool.Despawn(obstacle);
			}
		}

		private void OnPlayerHit() {
			Stop(true);
			levelManager.OnPlayerHitObstacle();
		}

		private void SpawnRowPeriodically() {
			spawnTime += Time.deltaTime;
			if (spawnTime < spawnPeriod)
				return;

			spawnTime = 0f;

			RunnerGrid runnerGrid = levelManager.GetRunnerGrid();
			SpawnRow(runnerGrid, obstacleRatio);
			levelManager.OnObstacleRowSpawned();
		}


		private void SpawnRow(RunnerGrid runnerGrid, float obstacleRatio) {
			Vector2Int gridSize = runnerGrid.GetGridSize();
			Vector2Int gridPosition = runnerGrid.GetGridPosition();
			int yMax = gridPosition.y + gridSize.y / 2;
			int xMin = gridPosition.x - gridSize.x / 2;
			int xMax = gridPosition.x + gridSize.x / 2;

			for (int x = xMin + 1; x < xMax; x++) {
				if (Random.value >= obstacleRatio)
					continue;

				Obstacle obstacle = obstacleFactory.CreateObstacle(new Vector2Int(x, yMax - 1));
				spawnedObstacles.Add(obstacle);
			}
		}
	}
}
