using System;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
	// TODO Has snake segment behaviours 

	private Vector2Int moveDirection = Vector2Int.up;
	private Vector2Int gridPosition = Vector2Int.zero;

	private float gridMoveTimer;
	private float gridMoveTimerMax = 0.1f;

	private int snakeBodySize = 0;
	private readonly List<Vector2Int> snakeBodyPositions = new();
	private readonly List<SnakeBody> snakeBodies = new();

	public Action OnSnakeMove { get; set; } = delegate { };

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

	public void AddBodyPart(SnakeBody snakeBody) {
		snakeBodySize++;
		snakeBodies.Add(snakeBody);
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
		
		OnSnakeMove.Invoke();
	}

	private void MoveToDirection(Vector2Int direction) {
		if (moveDirection == -direction)
			return;

		if (snakeBodyPositions.Count > 0 && gridPosition + direction == snakeBodyPositions[0])
			return;

		moveDirection = direction;
	}

	public Vector2Int GetGridPosition() => gridPosition;
	public List<SnakeBody> GetBodyParts() => snakeBodies;

	public List<Vector2Int> GetSnakeBodyPositions() => snakeBodyPositions;
}
