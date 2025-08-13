using UnityEngine;

namespace RunnerGame.Elements {
	public class RunnerGrid : MonoBehaviour {
		[SerializeField] private SpriteRenderer backgroundImage;
		[SerializeField] private Vector2 padding = Vector2.zero;

		private Vector2Int gridSize;
		private Vector2Int gridPosition;

		public void Initialize(Vector2Int gridPosition, Vector2Int gridSize) {
			this.gridPosition = gridPosition;
			this.gridSize = gridSize;

			backgroundImage.transform.position = new Vector3(gridPosition.x, gridPosition.y);
			backgroundImage.size = new Vector2(gridSize.x + padding.x, gridSize.y + padding.y);
		}

		public bool IsInsideGrid(Vector2Int position) {
			int xMax = gridPosition.x + gridSize.x / 2;
			int xMin = gridPosition.x - gridSize.x / 2;
			int yMax = gridPosition.y + gridSize.y / 2;
			int yMin = gridPosition.y - gridSize.y / 2;

			int x = position.x;
			int y = position.y;
			return x > xMin && x < xMax && y > yMin && y < yMax;
		}

		public Vector2Int GetGridSize() => gridSize;
		public Vector2Int GetGridPosition() => gridPosition;
	}
}
