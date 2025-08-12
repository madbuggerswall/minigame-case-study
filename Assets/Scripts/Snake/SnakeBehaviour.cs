using System.Collections.Generic;
using UnityEngine;

public class SnakeBehaviour : MonoBehaviour {
	// TODO Has snake segment behaviours 

	private Vector2Int moveDirection = Vector2Int.up;
	private Vector2Int gridPosition = Vector2Int.zero;

	private float gridMoveTimer;
	private float gridMoveTimerMax = 0.32f;

	private int snakeBodySize = 4;
	private List<Vector2Int> snakeMovePositions = new();

	[SerializeField] private Transform[] snakeBodyTransforms;

	// Dependencies
	private SnakeInputManager snakeInputManager;

	private void Start() {
		this.snakeInputManager = SnakeContext.GetInstance().Get<SnakeInputManager>();

		snakeInputManager.UpKeyPressEvent += OnUpPress;
		snakeInputManager.DownKeyPressEvent += OnDownPress;
		snakeInputManager.LeftKeyPressEvent += OnLeftPress;
		snakeInputManager.RightKeyPressEvent += OnRightPress;

		for (int i = 0; i < snakeBodyTransforms.Length; i++) {
			Vector3 bodyPosition = snakeBodyTransforms[i].position;
			snakeMovePositions.Insert(0, new Vector2Int((int) bodyPosition.x, (int) bodyPosition.y));
		}
	}

	private void Update() {
		Move();
	}

	private void Move() {
		gridMoveTimer += Time.deltaTime;
		if (!(gridMoveTimer >= gridMoveTimerMax))
			return;

		snakeMovePositions.Insert(0, gridPosition);

		gridPosition += moveDirection;
		gridMoveTimer = 0;
		transform.position = new Vector3(gridPosition.x, gridPosition.y);

		if (snakeMovePositions.Count >= snakeBodySize + 1) {
			snakeMovePositions.RemoveAt(snakeMovePositions.Count - 1);
		}

		for (int i = 0; i < snakeBodyTransforms.Length; i++) {
			Vector2Int snakeMovePosition = snakeMovePositions[i];
			Vector3 bodyPosition = new Vector3(snakeMovePosition.x, snakeMovePosition.y);
			snakeBodyTransforms[i].position = bodyPosition;
		}
	}

	private void OnUpPress() {
		if (moveDirection != Vector2Int.down)
			moveDirection = Vector2Int.up;
	}

	private void OnDownPress() {
		if (moveDirection != Vector2Int.up)
			moveDirection = Vector2Int.down;
	}

	private void OnRightPress() {
		if (moveDirection != Vector2Int.left)
			moveDirection = Vector2Int.right;
	}

	private void OnLeftPress() {
		if (moveDirection != Vector2Int.right)
			moveDirection = Vector2Int.left;
	}
}
