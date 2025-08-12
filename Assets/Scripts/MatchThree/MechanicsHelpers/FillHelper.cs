using System.Collections.Generic;
using MatchThree.PuzzleElements;
using UnityEngine;

namespace MatchThree.MechanicsHelpers {
	public class FillHelper {
		private readonly PuzzleLevelManager levelManager;
		private readonly List<PuzzleElement> filledElements = new();

		public FillHelper(PuzzleLevelManager levelManager) {
			this.levelManager = levelManager;
		}

		public void ApplyFill() {
			PuzzleGrid puzzleGrid = levelManager.GetPuzzleGrid();
			Vector2Int gridSize = puzzleGrid.GetGridSize();
			PuzzleCell[,] cells = puzzleGrid.GetCells();

			// Top to bottom
			for (int x = 0; x < gridSize.x; x++) {
				for (int y = gridSize.y - 1; y >= 0; y--) {
					PuzzleCell cell = cells[x, y];
					if (!cell.IsEmpty())
						continue;

					// Create a new drop model
					ColorDrop colorDrop = levelManager.CreateRandomColorChip();
					cell.SetPuzzleElement(colorDrop);
					filledElements.Add(colorDrop);
				}
			}
		}

		public List<PuzzleElement> GetFilledElements() => filledElements;
	}
}
