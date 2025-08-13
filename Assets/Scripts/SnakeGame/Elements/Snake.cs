using System;
using System.Collections.Generic;
using SnakeGame.Input;
using UnityEngine;

namespace SnakeGame.Elements {
	public class Snake : MonoBehaviour {
		private Vector2Int moveDirection = Vector2Int.up;
		private Vector2Int gridPosition = Vector2Int.zero;

		private const float BoostMultiplier = 2f;
		private float movementMultiplier = 1f;

		private const float GridMovePeriod = 0.1f;
		private float gridMoveTime;

		private int snakeBodySize = 0;
		private readonly List<Vector2Int> bodyPositions = new();
		private readonly List<SnakeBody> snakeBodies = new();

		private bool isStopped = false;

		public Action OnSnakeMove { get; set; } = delegate { };

		// Dependencies
		private SnakeInputManager snakeInputManager;

		private void Start() {
			snakeInputManager = SnakeContext.GetInstance().Get<SnakeInputManager>();

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
			gridMoveTime += Time.deltaTime * movementMultiplier;
			if (gridMoveTime < GridMovePeriod)
				return false;

			gridMoveTime = 0;
			return true;
		}

		private void AddHeadPosition() {
			bodyPositions.Insert(0, gridPosition);
		}

		private void MoveHead() {
			gridPosition += moveDirection;
			transform.position = new Vector3(gridPosition.x, gridPosition.y);
		}

		private void RemoveTailPosition() {
			if (bodyPositions.Count >= snakeBodySize + 1)
				bodyPositions.RemoveAt(bodyPositions.Count - 1);
		}


		private void MoveBodyParts() {
			for (int i = 0; i < snakeBodies.Count; i++) {
				Vector2Int snakeBodyPosition = bodyPositions[i];
				snakeBodies[i].transform.position = new Vector3(snakeBodyPosition.x, snakeBodyPosition.y);
			}
		}

		private void MoveToDirection(Vector2Int direction) {
			if (moveDirection == -direction)
				return;

			if (bodyPositions.Count > 0 && gridPosition + direction == bodyPositions[0])
				return;

			moveDirection = direction;
		}

		private void SetMovementMultiplier(float multiplier) {
			movementMultiplier = multiplier;
		}
		
		
		public void AddBodyPart(SnakeBody snakeBody) {
			snakeBodySize++;
			snakeBodies.Add(snakeBody);
		}

		public void Stop(bool isStopped) {
			this.isStopped = isStopped;
		}

		public bool IsOverItself() {
			Vector2Int headPosition = GetGridPosition();

			for (int i = 0; i < bodyPositions.Count; i++)
				if (gridPosition == bodyPositions[i])
					return true;

			return false;
		}

		public Vector2Int GetGridPosition() => gridPosition;
		public List<SnakeBody> GetBodyParts() => snakeBodies;
		public List<Vector2Int> GetSnakeBodyPositions() => bodyPositions;
	}
}
