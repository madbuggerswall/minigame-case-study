using System.Collections.Generic;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

public class SnakeLevelManager : IInitializable {
	private Snake snake;
	private Food food;
	private SnakeGrid snakeGrid;

	// Dependencies
	private SnakeLevelInitializer levelInitializer;
	private SnakeBodyFactory snakeBodyFactory;
	private FoodGenerator foodGenerator;

	public void Initialize() {
		this.levelInitializer = SnakeContext.GetInstance().Get<SnakeLevelInitializer>();
		this.foodGenerator = SnakeContext.GetInstance().Get<FoodGenerator>();
		this.snakeBodyFactory = SnakeContext.GetInstance().Get<SnakeBodyFactory>();

		this.snake = levelInitializer.GetSnake();
		this.food = levelInitializer.GetFood();
		this.snakeGrid = levelInitializer.GetSnakeGrid();

		snake.OnSnakeMove += this.OnSnakeMove;
	}

	private void OnSnakeMove() {
		if (snake.GetGridPosition() != food.GetGridPosition())
			return;

		foodGenerator.DespawnFood(food);

		SnakeBody snakeBody = snakeBodyFactory.CreateSnake(snake.GetGridPosition());
		snake.AddBodyPart(snakeBody);

		food = foodGenerator.SpawnFood(snakeGrid.GetGridSize(), snake);
	}
}

public class FoodGenerator : IInitializable {
	// Dependencies
	private FoodFactory foodFactory;
	private ObjectPool objectPool;

	public void Initialize() {
		this.objectPool = SnakeContext.GetInstance().Get<ObjectPool>();
		this.foodFactory = SnakeContext.GetInstance().Get<FoodFactory>();
	}

	public Food SpawnFood(Vector2Int gridSize, Snake snake) {
		Vector2Int randomFoodPosition = GetRandomFoodPosition(gridSize, snake);
		return foodFactory.CreateFood(randomFoodPosition);
	}

	public void DespawnFood(Food food) {
		objectPool.Despawn(food);
	}


	private Vector2Int GetRandomFoodPosition(Vector2Int gridSize, Snake snake) {
		Vector2Int randomPosition;

		do randomPosition = GetRandomPositionInGrid(gridSize);
		while (IsOverSnake(randomPosition, snake));

		return randomPosition;
	}

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
