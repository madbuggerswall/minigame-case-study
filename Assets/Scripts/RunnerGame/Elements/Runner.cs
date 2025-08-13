using RunnerGame.Input;
using RunnerGame.Mechanics;
using UnityEngine;

namespace RunnerGame.Elements {
	public class Runner : MonoBehaviour {
		private Vector2Int runnerPosition;
		private bool isStopped = false;

		// Dependencies
		private RunnerLevelManager levelManager;
		private RunnerInputManager runnerInputManager;

		private void Start() {
			runnerInputManager = RunnerContext.GetInstance().Get<RunnerInputManager>();
			levelManager = RunnerContext.GetInstance().Get<RunnerLevelManager>();

			runnerInputManager.UpKeyPressEvent += () => MoveToDirection(Vector2Int.up);
			runnerInputManager.DownKeyPressEvent += () => MoveToDirection(Vector2Int.down);
			runnerInputManager.LeftKeyPressEvent += () => MoveToDirection(Vector2Int.left);
			runnerInputManager.RightKeyPressEvent += () => MoveToDirection(Vector2Int.right);
		}

		private void MoveToDirection(Vector2Int moveDirection) {
			if (isStopped)
				return;

			runnerPosition = ClampRunnerPosition(runnerPosition + moveDirection);
			transform.position = new Vector3(runnerPosition.x, runnerPosition.y);
		}

		private Vector2Int ClampRunnerPosition(Vector2Int position) {
			RunnerGrid runnerGrid = levelManager.GetRunnerGrid();
			Vector2Int gridPosition = runnerGrid.GetGridPosition();
			Vector2Int gridSize = runnerGrid.GetGridSize();

			int xMax = gridPosition.x + gridSize.x / 2;
			int xMin = gridPosition.x - gridSize.x / 2;
			int yMax = gridPosition.y + gridSize.y / 2;
			int yMin = gridPosition.y - gridSize.y / 2;

			int clampedX = Mathf.Clamp(position.x, xMin + 1, xMax - 1);
			int clampedY = Mathf.Clamp(position.y, yMin + 1, yMax - 1);
			return new Vector2Int(clampedX, clampedY);
		}


		public void Stop(bool isStopped) => this.isStopped = isStopped;
		public Vector2Int GetGridPosition() => runnerPosition;
	}
}
