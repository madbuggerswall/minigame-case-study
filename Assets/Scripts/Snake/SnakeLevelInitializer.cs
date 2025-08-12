using UnityEngine;
using Utilities.Contexts;

public class SnakeLevelInitializer : MonoBehaviour, IInitializable {
	[SerializeField] private Vector2Int gridSize;

	private SnakeGrid snakeGrid;
	private Snake snake;
	private Food food;

	// Dependencies
	private SnakeGridFactory snakeGridFactory;
	private SnakeFactory snakeFactory;
	private SnakeBodyFactory snakeBodyFactory;
	private FoodFactory foodFactory;

	private CameraController cameraController;

	public void Initialize() {
		this.snakeGridFactory = SnakeContext.GetInstance().Get<SnakeGridFactory>();
		this.snakeFactory = SnakeContext.GetInstance().Get<SnakeFactory>();
		this.snakeBodyFactory = SnakeContext.GetInstance().Get<SnakeBodyFactory>();
		this.foodFactory = SnakeContext.GetInstance().Get<FoodFactory>();

		this.cameraController = SceneContext.GetInstance().Get<CameraController>();

		this.snakeGrid = snakeGridFactory.CreateSnakeGrid(Vector2Int.zero, gridSize);
		this.snake = snakeFactory.CreateSnake(Vector2Int.zero);
		this.food = foodFactory.CreateFood(GetRandomFoodPosition(gridSize));

		cameraController.PlayCameraPositionTween(Vector3.zero);
		cameraController.PlayOrthoSizeTween(gridSize);
	}

	private Vector2Int GetRandomFoodPosition(Vector2Int gridSize) {
		Vector2Int foodPosition = Vector2Int.zero;
		while (foodPosition == Vector2Int.zero) {
			int posX = Random.Range(-gridSize.x / 2, gridSize.x / 2);
			int posY = Random.Range(-gridSize.y / 2, gridSize.y / 2);
			foodPosition = new Vector2Int(posX, posY);
		}

		return foodPosition;
	}
}
