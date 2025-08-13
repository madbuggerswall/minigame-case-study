using System;
using System.Collections.Generic;
using SnakeGame.Input;
using UnityEngine;

namespace SnakeGame.Elements {
	public class Snake : MonoBehaviour {
		private Vector2Int moveDirection = Vector2Int.up;
		private Vector2Int headPosition = Vector2Int.zero;

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
			if (isStopped)
				return;

			MovePeriodically();
		}

		private void MovePeriodically() {
			gridMoveTime += Time.deltaTime * movementMultiplier;
			if (gridMoveTime < GridMovePeriod)
				return;

			gridMoveTime = 0;
			Move();
		}

		private void Move() {
			AddHeadPosition();
			MoveHead();
			RemoveTailPosition();
			MoveBodyParts();

			OnSnakeMove.Invoke();
		}


		private void AddHeadPosition() {
			bodyPositions.Insert(0, headPosition);
		}

		private void MoveHead() {
			headPosition += moveDirection;
			transform.position = new Vector3(headPosition.x, headPosition.y);
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

			if (bodyPositions.Count > 0 && headPosition + direction == bodyPositions[0])
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

		public bool IsOverItself() {
			for (int i = 0; i < bodyPositions.Count; i++)
				if (headPosition == bodyPositions[i])
					return true;

			return false;
		}

		public void Stop(bool isStopped) => this.isStopped = isStopped;

		public Vector2Int GetGridPosition() => headPosition;
		public List<SnakeBody> GetBodyParts() => snakeBodies;
		public List<Vector2Int> GetSnakeBodyPositions() => bodyPositions;
	}
}
