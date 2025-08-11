using UnityEngine;

namespace MatchThree.Model {
	public class FillManager {
		private BoardModel boardModel;
		private CellModel[,] cellModels;

		public FillManager(BoardModel boardModel) {
			this.boardModel = boardModel;
			this.cellModels = boardModel.GetCellModels();
		}
		
		public void FillEmptyCells() {}
		
		public void DropIntoEmptyCells() {
			int boardWidth = boardModel.GetBoardWidth();
			int boardHeight = boardModel.GetBoardHeight();

			for (int x = 0; x < boardWidth; x++) {
				int emptyY = -1; // Tracks the next available empty row in this column

				for (int y = 0; y < boardHeight; y++) {
					CellModel cell = cellModels[x, y];

					if (cell.IsEmpty()) {
						// If we haven't marked an empty slot yet, mark this row
						if (emptyY == -1)
							emptyY = y;
					}
					else if (emptyY != -1) {
						// We found a filled cell above an empty slot
						CellModel targetCell = cellModels[x, emptyY];
						targetCell.SetDropModel(cell.GetDropModel());
						cell.SetDropModel(null);

						// Advance emptyY to the next empty row above
						emptyY++;
					}
				}
			}
		}

	}
}
