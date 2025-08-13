using System;
using SnakeGame.Elements;
using SnakeGame.Factories;
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
			if (!snake.IsOverItself())
				return;

			snake.Stop(true);
			LevelFailEvent.Invoke();
		}

		private void CheckForWalls() {
			if (snakeGrid.IsInsideGrid(snake.GetGridPosition()))
				return;

			snake.Stop(true);
			LevelFailEvent.Invoke();
		}
	}
}
