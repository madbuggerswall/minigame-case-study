using MatchThree.Model;
using UnityEngine;

namespace MatchThree.View {
	public class BoardView : MonoBehaviour {
		[SerializeField] private SpriteRenderer spriteRenderer;

		public void Initialize(BoardModel boardModel) {
			int boardWidth = boardModel.GetBoardWidth();
			int boardHeight = boardModel.GetBoardHeight();
			
			spriteRenderer.size = new Vector2(boardWidth, boardHeight);
		}
	}
}
