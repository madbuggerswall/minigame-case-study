using UnityEngine;

namespace MatchThree.Model {
	public class BoardModel {
		private readonly CellModel[,] cellModels;
		private readonly int boardWidth;
		private readonly int boardHeight;

		public BoardModel(Vector2Int gridSize) {
			this.boardWidth = gridSize.x;
			this.boardHeight = gridSize.y;
			this.cellModels = new CellModel[gridSize.x, gridSize.y];

			InitializeCellModels();
		}

		private void InitializeCellModels() {
			for (int i = 0; i < boardWidth; i++)
				for (int j = 0; j < boardHeight; j++)
					cellModels[i, j] = new CellModel();
		}

		public CellModel[,] GetCellModels() => cellModels;
		public int GetBoardWidth() => boardWidth;
		public int GetBoardHeight() => boardHeight;
	}
}
