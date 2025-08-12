using System;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
	private Vector2Int moveDirection = Vector2Int.up;
	private Vector2Int gridPosition = Vector2Int.zero;

	private const float BoostMultiplier = 2f;
	private float movementMultiplier = 1f;

	private const float GridMoveTimerMax = 0.1f;
	private float gridMoveTimer;

	private int snakeBodySize = 0;
	private readonly List<Vector2Int> snakeBodyPositions = new();
	private readonly List<SnakeBody> snakeBodies = new();

	private bool isStopped = false;

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

		snakeInputManager.OneKeyPressEvent += () => SetMovementMultiplier(BoostMultiplier);
		snakeInputManager.OneKeyReleaseEvent += () => SetMovementMultiplier(1f);
	}

	private void Update() {
		Move();
	}

	public void AddBodyPart(SnakeBody snakeBody) {
		snakeBodySize++;
		snakeBodies.Add(snakeBody);
	}

	public void Stop(bool isStopped) {
		this.isStopped = isStopped;
	}

	private void Move() {
		if (isStopped)
			return;

		if (!ShouldMove())
			return;

		AddHeadPosition();
		MoveHead();
		RemoveTailPosition();
		MoveBodyParts();

		OnSnakeMove.Invoke();
	}

	private bool ShouldMove() {
		gridMoveTimer += Time.deltaTime * movementMultiplier;
		if (gridMoveTimer < GridMoveTimerMax)
			return false;

		gridMoveTimer = 0;
		return true;
	}

	private void AddHeadPosition() {
		snakeBodyPositions.Insert(0, gridPosition);
	}

	private void MoveHead() {
		gridPosition += moveDirection;
		transform.position = new Vector3(gridPosition.x, gridPosition.y);
	}

	private void RemoveTailPosition() {
		if (snakeBodyPositions.Count >= snakeBodySize + 1)
			snakeBodyPositions.RemoveAt(snakeBodyPositions.Count - 1);
	}


	private void MoveBodyParts() {
		for (int i = 0; i < snakeBodies.Count; i++) {
			Vector2Int snakeBodyPosition = snakeBodyPositions[i];
			snakeBodies[i].transform.position = new Vector3(snakeBodyPosition.x, snakeBodyPosition.y);
		}
	}

	private void MoveToDirection(Vector2Int direction) {
		if (moveDirection == -direction)
			return;

		if (snakeBodyPositions.Count > 0 && gridPosition + direction == snakeBodyPositions[0])
			return;

		moveDirection = direction;
	}

	private void SetMovementMultiplier(float multiplier) {
		this.movementMultiplier = multiplier;
	}

	public Vector2Int GetGridPosition() => gridPosition;
	public List<SnakeBody> GetBodyParts() => snakeBodies;
	public List<Vector2Int> GetSnakeBodyPositions() => snakeBodyPositions;
}
