using UnityEngine;

namespace RunnerGame.Elements {
	public class Obstacle : MonoBehaviour {
		private Vector2Int gridPosition;

		public void Initialize(Vector2Int gridPosition) {
			SetGridPosition(gridPosition);
		}

		public Vector2Int GetGridPosition() => gridPosition;

		public void SetGridPosition(Vector2Int gridPosition) {
			this.gridPosition = gridPosition;
			transform.position = new Vector3(gridPosition.x, gridPosition.y);
		}
	}
}
