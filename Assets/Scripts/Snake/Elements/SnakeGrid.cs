using UnityEngine;

namespace Snake {
	public class SnakeGrid : MonoBehaviour {
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

		public Vector2Int GetGridSize() => gridSize;
		public Vector2Int GetGridPosition() => gridPosition;
	}
}
