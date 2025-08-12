using System.Collections.Generic;
using MatchThree.PuzzleElements;
using UnityEngine;

namespace MatchThree.MechanicsHelpers {
	public class FallHelper {
		private readonly PuzzleLevelManager levelManager;
		private readonly List<PuzzleElement> fallenElements = new();

		public FallHelper(PuzzleLevelManager levelManager) {
			this.levelManager = levelManager;
		}

		public void ApplyFall() {
			PuzzleGrid puzzleGrid = levelManager.GetPuzzleGrid();
			Vector2Int gridSize = puzzleGrid.GetGridSize();
			PuzzleCell[,] cells = puzzleGrid.GetCells();
			fallenElements.Clear();

			for (int x = 0; x < gridSize.x; x++) {
				ApplyFallToColumn(x);
			}
		}

		private void ApplyFallToColumn(int x) {
			PuzzleGrid puzzleGrid = levelManager.GetPuzzleGrid();
			Vector2Int gridSize = puzzleGrid.GetGridSize();
			PuzzleCell[,] cells = puzzleGrid.GetCells();

			// Tracks the next available empty row in this column
			int emptyY = -1;

			for (int y = 0; y < gridSize.y; y++) {
				PuzzleCell cell = cells[x, y];

				if (cell.IsEmpty()) {
					// If we haven't marked an empty slot yet, mark this row
					emptyY = (emptyY == -1) ? y : emptyY;
				} else if (emptyY != -1) {
					// We found a filled cell above an empty slot
					PuzzleCell targetCell = cells[x, emptyY];
					ColorDrop fallingDrop = cell.GetColorDrop();
					targetCell.SetColorDrop(fallingDrop);
					cell.SetColorDrop(null);

					// Advance emptyY to the next empty row above
					emptyY++;

					fallenElements.Add(fallingDrop);
				}
			}
		}


		// Getters
		public List<PuzzleElement> GetFallenElements() => fallenElements;
	}
}
