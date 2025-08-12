using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
	// TODO Has snake segment behaviours 

	private Vector2Int moveDirection = Vector2Int.up;
	private Vector2Int gridPosition = Vector2Int.zero;

	private float gridMoveTimer;
	private float gridMoveTimerMax = 0.32f;

	private int snakeBodySize = 0;
	private readonly List<Vector2Int> snakeBodyPositions = new();
	private readonly List<SnakeBody> snakeBodies = new();

	// Dependencies
	private SnakeInputManager snakeInputManager;

	private void Start() {
		this.snakeInputManager = SnakeContext.GetInstance().Get<SnakeInputManager>();

		// SnakeController
		snakeInputManager.UpKeyPressEvent += () => MoveToDirection(Vector2Int.up);
		snakeInputManager.DownKeyPressEvent += () => MoveToDirection(Vector2Int.down);
		snakeInputManager.LeftKeyPressEvent += () => MoveToDirection(Vector2Int.left);
		snakeInputManager.RightKeyPressEvent += () => MoveToDirection(Vector2Int.right);

		// Body init, might be extra
		// for (int i = 0; i < snakeBodies.Length; i++) {
		// 	Vector3 bodyPosition = snakeBodies[i].position;
		// 	snakeBodyPositions.Add(new Vector2Int((int) bodyPosition.x, (int) bodyPosition.y));
		// }
	}

	private void Update() {
		Move();
	}

	private void Move() {
		gridMoveTimer += Time.deltaTime;
		if (gridMoveTimer < gridMoveTimerMax)
			return;

		gridMoveTimer = 0;
		
		snakeBodyPositions.Insert(0, gridPosition);

		gridPosition += moveDirection;
		transform.position = new Vector3(gridPosition.x, gridPosition.y);

		if (snakeBodyPositions.Count >= snakeBodySize + 1) {
			snakeBodyPositions.RemoveAt(snakeBodyPositions.Count - 1);
		}

		for (int i = 0; i < snakeBodies.Count; i++) {
			Vector2Int snakeBodyPosition = snakeBodyPositions[i];
			Vector3 bodyPosition = new Vector3(snakeBodyPosition.x, snakeBodyPosition.y);
			snakeBodies[i].transform.position = bodyPosition;
		}
	}

	private void OnUpPress() {
		if (moveDirection != Vector2Int.down && gridPosition + Vector2Int.up != snakeBodyPositions[0])
			moveDirection = Vector2Int.up;
	}

	private void OnDownPress() {
		if (moveDirection != Vector2Int.up && gridPosition + Vector2Int.down != snakeBodyPositions[0])
			moveDirection = Vector2Int.down;
	}

	private void OnRightPress() {
		if (moveDirection != Vector2Int.left && gridPosition + Vector2Int.right != snakeBodyPositions[0])
			moveDirection = Vector2Int.right;
	}

	private void OnLeftPress() {
		if (moveDirection != Vector2Int.right && gridPosition + Vector2Int.left != snakeBodyPositions[0])
			moveDirection = Vector2Int.left;
	}

	private void MoveToDirection(Vector2Int direction) {
		if (moveDirection == -direction)
			return;

		if (snakeBodyPositions.Count > 0 && gridPosition + direction == snakeBodyPositions[0])
			return;

		moveDirection = direction;
	}
}
