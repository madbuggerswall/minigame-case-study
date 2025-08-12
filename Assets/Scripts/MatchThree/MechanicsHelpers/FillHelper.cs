using System.Collections.Generic;
using MatchThree.PuzzleElements;

namespace MatchThree.MechanicsHelpers {
	public class FillHelper {
		private readonly PuzzleLevelManager levelManager;
		private readonly HashSet<PuzzleElement> filledElements = new();

		public FillHelper(PuzzleLevelManager levelManager) {
			this.levelManager = levelManager;
		}

		public void ApplyFill() {
			// TODO Replace with the other fill operation
			
			// PuzzleGrid puzzleGrid = levelManager.GetPuzzleGrid();
			// Vector2Int gridSize = puzzleGrid.GetGridSize();
			// filledElements.Clear();
			//
			// // Assumes that a fall operation has already resolved empty spaces
			// for (int columnIndex = 0; columnIndex < gridSize.x; columnIndex++) {
			// 	for (int rowIndex = 0; rowIndex < gridSize.y; rowIndex++) {
			// 		PuzzleCell columnCell = puzzleGrid.GetCell(rowIndex * gridSize.x + columnIndex);
			// 		if (columnCell.TryGetPuzzleElement(out _))
			// 			continue;
			//
			// 		ColorDrop colorDrop = levelManager.CreateRandomColorChip();
			// 		columnCell.SetPuzzleElement(colorDrop);
			// 		filledElements.Add(colorDrop);
			// 	}
			// }
		}

		public HashSet<PuzzleElement> GetFilledElements() => filledElements;
	}
}
