using System;
using System.Collections.Generic;
using SnakeGame.Elements;
using SnakeGame.Factories;
using SnakeGame.UI;
using UnityEngine;
using Utilities.Contexts;

namespace SnakeGame.Level {
	public class SnakeLevelManager : IInitializable {
		private Elements.Snake snake;
		private Food food;
		private SnakeGrid snakeGrid;

		public Action LevelFailEvent { get; set; } = delegate { };
		public Action LevelSuccessEvent { get; set; } = delegate { };

		// Dependencies
		private SnakeLevelInitializer levelInitializer;
		private SnakeBodyFactory snakeBodyFactory;
		private FoodGenerator foodGenerator;

		private SnakeUIController uiController;
		private SnakeScoreManager snakeScoreManager;

		public void Initialize() {
			this.levelInitializer = SnakeContext.GetInstance().Get<SnakeLevelInitializer>();
			this.foodGenerator = SnakeContext.GetInstance().Get<FoodGenerator>();
			this.snakeBodyFactory = SnakeContext.GetInstance().Get<SnakeBodyFactory>();

			this.uiController = SnakeContext.GetInstance().Get<SnakeUIController>();
			this.snakeScoreManager = SnakeContext.GetInstance().Get<SnakeScoreManager>();

			this.snake = levelInitializer.GetSnake();
			this.food = levelInitializer.GetFood();
			this.snakeGrid = levelInitializer.GetSnakeGrid();

			snake.OnSnakeMove += this.OnSnakeMove;
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

		private static bool IsInsideGrid(Elements.Snake snake, SnakeGrid snakeGrid) {
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

		private static bool IsOverItself(Elements.Snake snake) {
			Vector2Int headPosition = snake.GetGridPosition();
			List<Vector2Int> bodyPositions = snake.GetSnakeBodyPositions();

			for (int i = 0; i < bodyPositions.Count; i++)
				if (headPosition == bodyPositions[i])
					return true;

			return false;
		}
	}
}
