using SnakeGame.Elements;
using SnakeGame.Factories;
using UnityEngine;
using Utilities.Contexts;

namespace SnakeGame.Mechanics {
	public class SnakeLevelInitializer : MonoBehaviour, IInitializable {
		[SerializeField] private Vector2Int gridSize;

		private SnakeGrid snakeGrid;
		private Snake snake;
		private Food food;

		// Dependencies
		private SnakeGridFactory snakeGridFactory;
		private SnakeFactory snakeFactory;
		private FoodFactory foodFactory;
		private FoodGenerator foodGenerator;

		private CameraController cameraController;

		public void Initialize() {
			snakeGridFactory = SnakeContext.GetInstance().Get<SnakeGridFactory>();
			snakeFactory = SnakeContext.GetInstance().Get<SnakeFactory>();
			foodGenerator = SnakeContext.GetInstance().Get<FoodGenerator>();

			cameraController = SceneContext.GetInstance().Get<CameraController>();

			snakeGrid = snakeGridFactory.CreateSnakeGrid(Vector2Int.zero, gridSize);
			snake = snakeFactory.CreateSnake(Vector2Int.zero);
			food = foodGenerator.SpawnFood(gridSize, snake);

			cameraController.PlayCameraPositionTween(Vector3.zero);
			cameraController.PlayOrthoSizeTween(gridSize);
		}

	

		public Snake GetSnake() => snake;
		public Food GetFood() => food;
		public SnakeGrid GetSnakeGrid() => snakeGrid;
	}
}
