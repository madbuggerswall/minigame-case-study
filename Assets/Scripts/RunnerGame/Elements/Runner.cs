using RunnerGame.Input;
using UnityEngine;

namespace RunnerGame.Elements {
	public class Runner : MonoBehaviour {
		private Vector2Int gridPosition;

		private RunnerInputManager runnerInputManager;

		private void Start() {
			runnerInputManager = RunnerContext.GetInstance().Get<RunnerInputManager>();
			
			runnerInputManager.UpKeyPressEvent += () => MoveToDirection(Vector2Int.up);
			runnerInputManager.DownKeyPressEvent += () => MoveToDirection(Vector2Int.down);
			runnerInputManager.LeftKeyPressEvent += () => MoveToDirection(Vector2Int.left);
			runnerInputManager.RightKeyPressEvent += () => MoveToDirection(Vector2Int.right);
		}

		private void MoveToDirection(Vector2Int moveDirection) {
			gridPosition += moveDirection;
			transform.position = new Vector3(gridPosition.x, gridPosition.y);
		}
		
		// TODO Check walls and obstacles here maybe

		public Vector2Int GetGridPosition() => gridPosition;
	}
}
