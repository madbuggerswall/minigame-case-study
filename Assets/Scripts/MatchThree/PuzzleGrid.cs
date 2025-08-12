using MatchThree.PuzzleElements;
using UnityEngine;
using Utilities.Grids;

namespace MatchThree {
	public class PuzzleGrid : SquareGrid<PuzzleCell> {
		public PuzzleGrid(GridParams gridParams, CellParams cellParams) : base(gridParams, cellParams) { }


		// TODO Optimize this
		public Vector2Int GetCellIndex(PuzzleCell puzzleCell) {
			for (int y = 0; y < gridSize.y; y++) {
				for (int x = 0; x < gridSize.x; x++) {
					if (puzzleCell == cells[x, y])
						return new Vector2Int(x, y);
				}
			}

			return -Vector2Int.one;
		}

		// TODO Optimize this
		public bool TryGetPuzzleCell(PuzzleElement puzzleElement, out PuzzleCell puzzleCell) {
			for (int y = 0; y < gridSize.y; y++) {
				for (int x = 0; x < gridSize.x; x++) {
					PuzzleCell cell = cells[x, y];
					if (cell.IsEmpty())
						continue;

					PuzzleElement otherElement = cell.GetPuzzleElement();
					if (otherElement != puzzleElement)
						continue;

					puzzleCell = cell;
					return true;
				}
			}

			puzzleCell = null;
			return false;
		}
	}
}
