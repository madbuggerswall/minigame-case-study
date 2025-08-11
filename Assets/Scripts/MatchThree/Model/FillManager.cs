using UnityEngine;

namespace MatchThree.Model {
	public class FillManager {
		private BoardModel boardModel;
		private CellModel[,] cellModels;

		public FillManager(BoardModel boardModel) {
			this.boardModel = boardModel;
			this.cellModels = boardModel.GetCellModels();
		}

		public void FillEmptyCells() {
			int boardWidth = boardModel.GetBoardWidth();
			int boardHeight = boardModel.GetBoardHeight();

			for (int x = 0; x < boardWidth; x++) {
				for (int y = boardHeight - 1; y >= 0; y--) {
					// Top to bottom
					CellModel cell = cellModels[x, y];
					if (!cell.IsEmpty())
						continue;

					// Create a new drop model
					DropModel newDrop = new DropModel(GetRandomDropColor());

					// Assign the drop to this cell
					cell.SetDropModel(newDrop);
				}
			}
		}

		public void DropIntoEmptyCells() {
			int boardWidth = boardModel.GetBoardWidth();
			for (int x = 0; x < boardWidth; x++)
				ApplyFallToColumn(x);
		}

		private void ApplyFallToColumn(int columnIndex) {
			// Tracks the next available empty row in this column
			int emptyY = -1; 
			int boardHeight = boardModel.GetBoardHeight();

			for (int y = 0; y < boardHeight; y++) {
				CellModel cell = cellModels[columnIndex, y];

				if (cell.IsEmpty()) {
					// If we haven't marked an empty slot yet, mark this row
					if (emptyY == -1)
						emptyY = y;
				} else if (emptyY != -1) {
					// We found a filled cell above an empty slot
					CellModel targetCell = cellModels[columnIndex, emptyY];
					targetCell.SetDropModel(cell.GetDropModel());
					cell.SetDropModel(null);

					// Advance emptyY to the next empty row above
					emptyY++;
				}
			}
		}

		private DropColor GetRandomDropColor() {
			int randomIndex = Random.Range(0, 3);
			return randomIndex switch {
				0 => DropColor.Blue,
				1 => DropColor.Red,
				2 => DropColor.Green,
				3 => DropColor.Yellow,
				_ => DropColor.Blue
			};
		}
	}
}
