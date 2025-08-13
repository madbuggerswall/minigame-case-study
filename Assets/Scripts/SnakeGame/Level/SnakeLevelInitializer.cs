using SnakeGame.Elements;
using SnakeGame.Factories;
using UnityEngine;
using Utilities.Contexts;

namespace SnakeGame.Level {
	public class SnakeLevelInitializer : MonoBehaviour, IInitializable {
		[SerializeField] private Vector2Int gridSize;

		private SnakeGrid snakeGrid;
		private Elements.Snake snake;
		private Food food;

		// Dependencies
		private SnakeGridFactory snakeGridFactory;
		private SnakeFactory snakeFactory;
		private FoodFactory foodFactory;
		private FoodGenerator foodGenerator;

		private CameraController cameraController;

		public void Initialize() {
			this.snakeGridFactory = SnakeContext.GetInstance().Get<SnakeGridFactory>();
			this.snakeFactory = SnakeContext.GetInstance().Get<SnakeFactory>();
			this.foodGenerator = SnakeContext.GetInstance().Get<FoodGenerator>();

			this.cameraController = SceneContext.GetInstance().Get<CameraController>();

			this.snakeGrid = snakeGridFactory.CreateSnakeGrid(Vector2Int.zero, gridSize);
			this.snake = snakeFactory.CreateSnake(Vector2Int.zero);
			this.food = foodGenerator.SpawnFood(gridSize, snake);

			cameraController.PlayCameraPositionTween(Vector3.zero);
			cameraController.PlayOrthoSizeTween(gridSize);
		}

	

		public Elements.Snake GetSnake() => snake;
		public Food GetFood() => food;
		public SnakeGrid GetSnakeGrid() => snakeGrid;
	}
}
