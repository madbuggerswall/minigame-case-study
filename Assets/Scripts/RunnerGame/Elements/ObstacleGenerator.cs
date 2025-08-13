using System.Collections.Generic;
using RunnerGame.Factories;
using SnakeGame.Elements;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace RunnerGame.Elements {
	// TODO This will require altered logic
	public class ObstacleGenerator : IInitializable {
		// Dependencies
		private ObstacleFactory obstacleFactory;
		private ObjectPool objectPool;

		public void Initialize() {
			objectPool = RunnerContext.GetInstance().Get<ObjectPool>();
			obstacleFactory = RunnerContext.GetInstance().Get<ObstacleFactory>();
		}

		public Obstacle SpawnObstacle(Vector2Int gridSize, Snake snake) {
			Vector2Int randomFoodPosition = GetRandomObstaclePosition(gridSize, snake);
			return obstacleFactory.CreateObstacle(randomFoodPosition);
		}

		public void DespawnObstacle(Obstacle obstacle) {
			objectPool.Despawn(obstacle);
		}


		// TODO
		private Vector2Int GetRandomObstaclePosition(Vector2Int gridSize, Snake snake) {
			Vector2Int randomPosition;

			do randomPosition = GetRandomPositionInGrid(gridSize);
			while (IsOverSnake(randomPosition, snake));

			return randomPosition;
		}

		// TODO
		private static bool IsOverSnake(Vector2Int position, Snake snake) {
			Vector2Int headPosition = snake.GetGridPosition();
			if (position == headPosition)
				return true;

			List<Vector2Int> snakeBodyPositions = snake.GetSnakeBodyPositions();
			for (int i = 0; i < snakeBodyPositions.Count; i++)
				if (snakeBodyPositions[i] == position)
					return true;

			return false;
		}

		private static Vector2Int GetRandomPositionInGrid(Vector2Int gridSize) {
			Vector2Int position = Vector2Int.zero;
			while (position == Vector2Int.zero) {
				int posX = Random.Range(-gridSize.x / 2 + 1, gridSize.x / 2 - 1);
				int posY = Random.Range(-gridSize.y / 2 + 1, gridSize.y / 2 - 1);
				position = new Vector2Int(posX, posY);
			}

			return position;
		}
	}
}
