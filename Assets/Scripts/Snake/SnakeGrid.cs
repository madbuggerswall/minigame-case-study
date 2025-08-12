using UnityEngine;
using Utilities.Grids;

public class SnakeGrid : MonoBehaviour {
	[SerializeField] private SpriteRenderer backgroundImage;
	[SerializeField] private Vector2 padding = Vector2.zero;

	public void Initialize(Vector2Int gridPosition, Vector2Int gridSize) {
		backgroundImage.transform.position = new Vector3(gridPosition.x, gridPosition.y);
		backgroundImage.size = new Vector2(gridSize.x + padding.x, gridSize.y + padding.y);
	}
}
