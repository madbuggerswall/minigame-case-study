using System.Collections.Generic;
using Snake.Factories;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace Snake.Elements {
	public class FoodGenerator : IInitializable {
		// Dependencies
		private FoodFactory foodFactory;
		private ObjectPool objectPool;

		public void Initialize() {
			this.objectPool = SnakeContext.GetInstance().Get<ObjectPool>();
			this.foodFactory = SnakeContext.GetInstance().Get<FoodFactory>();
		}

		public Food SpawnFood(Vector2Int gridSize, Elements.Snake snake) {
			Vector2Int randomFoodPosition = GetRandomFoodPosition(gridSize, snake);
			return foodFactory.CreateFood(randomFoodPosition);
		}

		public void DespawnFood(Food food) {
			objectPool.Despawn(food);
		}


		private Vector2Int GetRandomFoodPosition(Vector2Int gridSize, Elements.Snake snake) {
			Vector2Int randomPosition;

			do randomPosition = GetRandomPositionInGrid(gridSize);
			while (IsOverSnake(randomPosition, snake));

			return randomPosition;
		}

		private static bool IsOverSnake(Vector2Int position, Elements.Snake snake) {
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
