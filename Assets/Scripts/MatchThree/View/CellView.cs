using UnityEngine;

namespace MatchThree.View {
	public class CellView : MonoBehaviour {
		[SerializeField] private SpriteRenderer spriteRenderer;

		public void Initialize(Vector2Int cellIndex) {
			int indexSum = cellIndex.x + cellIndex.y;
			spriteRenderer.color = indexSum % 2 == 0 ? Color.white : Color.gray;
		}
	}
}
