using System.Collections.Generic;
using RunnerGame.Factories;
using RunnerGame.Level;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace RunnerGame.Elements {
	// TODO This will require altered logic
	public class ObstacleGenerator : MonoBehaviour, IInitializable {
		[Header("Spawn Settings")]
		[SerializeField] private float spawnPeriod = 1;
		[SerializeField] private float movePeriod = 1;

		[Header("Perlin Settings")]
		[SerializeField] private float obstacleRatio = 0.2f;
		[SerializeField] private float noiseScale = 1f;
		[SerializeField] private float noiseOffset = 0f;

		private readonly List<Obstacle> spawnedObstacles = new();
		private float spawnTime = 0;
		private float moveTime = 0;

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
			SpawnRowPeriodically();
			MoveObstaclesDownwards();
		}

		private void MoveObstaclesDownwards() {
			moveTime += Time.deltaTime;
			if (moveTime < movePeriod)
				return;

			moveTime = 0;
			RunnerGrid runnerGrid = levelManager.GetRunnerGrid();

			for (int i = 0; i < spawnedObstacles.Count; i++) {
				Obstacle obstacle = spawnedObstacles[i];
				Vector2Int updatedPosition = obstacle.GetGridPosition() + Vector2Int.down;
				if (runnerGrid.IsInsideGrid(updatedPosition))
					obstacle.SetGridPosition(updatedPosition);
				else {
					spawnedObstacles[i] = spawnedObstacles[^1];
					spawnedObstacles.RemoveAt(spawnedObstacles.Count - 1);
					objectPool.Despawn(obstacle);
				}
			}
		}

		private void SpawnRowPeriodically() {
			spawnTime += Time.deltaTime;
			if (spawnTime < spawnPeriod)
				return;

			spawnTime = 0f;

			RunnerGrid runnerGrid = levelManager.GetRunnerGrid();
			SpawnRow(runnerGrid, obstacleRatio);
		}


		private void SpawnRow(RunnerGrid runnerGrid, float obstacleRatio) {
			Vector2Int gridSize = runnerGrid.GetGridSize();
			Vector2Int gridPosition = runnerGrid.GetGridPosition();
			int yMax = gridPosition.y + gridSize.y / 2;
			int xMin = gridPosition.x - gridSize.x / 2;
			int xMax = gridPosition.x + gridSize.x / 2;

			for (int x = xMin; x <= xMax; x++) {
				if (!SamplePerlinNoise1D(x, noiseScale, obstacleRatio, noiseOffset))
					continue;

				Obstacle obstacle = obstacleFactory.CreateObstacle(new Vector2Int(x, yMax - 1));
				spawnedObstacles.Add(obstacle);
			}
		}

		private static bool SamplePerlinNoise1D(float posX, float noiseScale, float threshold, float noiseOffset) {
			return Mathf.PerlinNoise(noiseOffset + posX * noiseScale, 0f) > threshold;
		}


		// TODO
		// private Vector2Int GetRandomObstaclePosition(Vector2Int gridSize, Snake snake) {
		// 	Vector2Int randomPosition;
		//
		// 	do randomPosition = GetRandomPositionInGrid(gridSize);
		// 	while (IsOverSnake(randomPosition, snake));
		//
		// 	return randomPosition;
		// }
		//
		// // TODO
		// private static bool IsOverSnake(Vector2Int position, Snake snake) {
		// 	Vector2Int headPosition = snake.GetGridPosition();
		// 	if (position == headPosition)
		// 		return true;
		//
		// 	List<Vector2Int> snakeBodyPositions = snake.GetSnakeBodyPositions();
		// 	for (int i = 0; i < snakeBodyPositions.Count; i++)
		// 		if (snakeBodyPositions[i] == position)
		// 			return true;
		//
		// 	return false;
		// }
		//
		// private static Vector2Int GetRandomPositionInGrid(Vector2Int gridSize) {
		// 	Vector2Int position = Vector2Int.zero;
		// 	while (position == Vector2Int.zero) {
		// 		int posX = Random.Range(-gridSize.x / 2 + 1, gridSize.x / 2 - 1);
		// 		int posY = Random.Range(-gridSize.y / 2 + 1, gridSize.y / 2 - 1);
		// 		position = new Vector2Int(posX, posY);
		// 	}
		//
		// 	return position;
		// }
	}
}
