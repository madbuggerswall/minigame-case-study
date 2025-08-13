using System;
using System.Collections.Generic;
using SnakeGame.Elements;
using SnakeGame.Factories;
using UnityEngine;
using Utilities.Contexts;

namespace SnakeGame.Level {
	public class SnakeLevelManager : IInitializable {
		private Snake snake;
		private Food food;
		private SnakeGrid snakeGrid;

		public Action LevelFailEvent { get; set; } = delegate { };
		public Action LevelSuccessEvent { get; set; } = delegate { };

		// Dependencies
		private SnakeLevelInitializer levelInitializer;
		private SnakeBodyFactory snakeBodyFactory;
		private FoodGenerator foodGenerator;
		private SnakeScoreManager snakeScoreManager;

		public void Initialize() {
			levelInitializer = SnakeContext.GetInstance().Get<SnakeLevelInitializer>();
			foodGenerator = SnakeContext.GetInstance().Get<FoodGenerator>();
			snakeBodyFactory = SnakeContext.GetInstance().Get<SnakeBodyFactory>();
			snakeScoreManager = SnakeContext.GetInstance().Get<SnakeScoreManager>();

			snake = levelInitializer.GetSnake();
			food = levelInitializer.GetFood();
			snakeGrid = levelInitializer.GetSnakeGrid();

			snake.OnSnakeMove += OnSnakeMove;
		}

		private void OnSnakeMove() {
			CheckForFood();
			CheckForSnakeBody();
			CheckForWalls();
		}

		private void CheckForFood() {
			if (snake.GetGridPosition() != food.GetGridPosition())
				return;

			SnakeBody snakeBody = snakeBodyFactory.CreateSnake(snake.GetGridPosition());
			snake.AddBodyPart(snakeBody);

			foodGenerator.DespawnFood(food);
			food = foodGenerator.SpawnFood(snakeGrid.GetGridSize(), snake);

			snakeScoreManager.IncrementScore();
		}

		private void CheckForSnakeBody() {
			if (!IsOverItself(snake))
				return;

			snake.Stop(true);
			LevelFailEvent.Invoke();
			
		}

		private void CheckForWalls() {
			if (IsInsideGrid(snake, snakeGrid))
				return;

			snake.Stop(true);
			LevelFailEvent.Invoke();
		}

		private static bool IsInsideGrid(Snake snake, SnakeGrid snakeGrid) {
			Vector2Int headPosition = snake.GetGridPosition();
			Vector2Int gridSize = snakeGrid.GetGridSize();
			Vector2Int gridPosition = snakeGrid.GetGridPosition();

			int xMax = gridPosition.x + gridSize.x / 2;
			int xMin = gridPosition.x - gridSize.x / 2;
			int yMax = gridPosition.y + gridSize.y / 2;
			int yMin = gridPosition.y - gridSize.y / 2;

			int x = headPosition.x;
			int y = headPosition.y;
			return x > xMin && x < xMax && y > yMin && y < yMax;
		}

		private static bool IsOverItself(Snake snake) {
			Vector2Int headPosition = snake.GetGridPosition();
			List<Vector2Int> bodyPositions = snake.GetSnakeBodyPositions();

			for (int i = 0; i < bodyPositions.Count; i++)
				if (headPosition == bodyPositions[i])
					return true;

			return false;
		}
	}
}
