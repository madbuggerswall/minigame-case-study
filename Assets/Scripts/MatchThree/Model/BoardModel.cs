using MatchThree.PuzzleElements;
using UnityEngine;
using Utilities.Grids;

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
			for (int j = 0; j < boardHeight; j++)
				for (int i = 0; i < boardWidth; i++)
					cellModels[i, j] = new CellModel();
		}

		public CellModel[,] GetCellModels() => cellModels;
		public int GetWidth() => boardWidth;
		public int GetHeight() => boardHeight;
	}

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

					ColorDrop otherElement = cell.GetColorDrop();
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

	public class PuzzleCell : SquareCell {
		// TODO Replace this with puzzle element
		private ColorDrop colorDrop;

		public PuzzleCell(Vector3 worldPosition, float diameter) : base(worldPosition, diameter) { }


		public void SetColorDrop(ColorDrop colorDrop) => this.colorDrop = colorDrop;
		public bool IsEmpty() => colorDrop is null;
		public ColorDrop GetColorDrop() => colorDrop;
	}

	public class PuzzleCellFactory : CellFactory<PuzzleCell> {
		public override PuzzleCell Create(Vector3 cellPosition, float diameter) {
			return new PuzzleCell(cellPosition, diameter);
		}
	}
}
