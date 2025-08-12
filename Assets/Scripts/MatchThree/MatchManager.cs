using System.Collections.Generic;
using MatchThree.PuzzleElements;
using UnityEngine;

// For a match to occur, it should be greater than 3 blocks in all directions or 2x2
// NOTE Rename it to MatchFinder or MatchHelper
namespace MatchThree {
	public class MatchManager {
		private static readonly Vector2Int[] NeighborDirections = new[] {
			new Vector2Int(1, 0),  // Right
			new Vector2Int(0, 1),  // Up
			new Vector2Int(-1, 0), // Left
			new Vector2Int(0, -1)  // Down
		};

		private readonly PuzzleGrid puzzleGrid;
		private readonly PuzzleCell[,] cells;

		private readonly bool[,] visitedIndices;
		private readonly Queue<Vector2Int> indicesToVisit;

		private readonly List<MatchModel> matchModels;

		public MatchManager(PuzzleGrid puzzleGrid) {
			this.puzzleGrid = puzzleGrid;
			this.cells = puzzleGrid.GetCells();

			Vector2Int gridSize = puzzleGrid.GetGridSize();
			this.visitedIndices = new bool[gridSize.x, gridSize.y];
			this.indicesToVisit = new Queue<Vector2Int>();
			this.matchModels = new List<MatchModel>();
		}

		public List<MatchModel> FindMatches() {
			ResetVisitedIndices();
			matchModels.Clear();
			Vector2Int gridSize = puzzleGrid.GetGridSize();
			for (int y = 0; y < gridSize.y; y++) {
				for (int x = 0; x < gridSize.x; x++) {
					Vector2Int currentCellIndex = new Vector2Int(x, y);
					if (!CanVisitCell(currentCellIndex))
						continue;

					indicesToVisit.Clear();
					indicesToVisit.Enqueue(currentCellIndex);
					visitedIndices[x, y] = true;

					MatchModel currentMatchModel = FindMatch();
					currentMatchModel.EvaluateAxes();
					if (currentMatchModel.IsValid())
						matchModels.Add(currentMatchModel);
				}
			}

			return matchModels;
		}

		private MatchModel FindMatch() {
			MatchModel matchModel = new();

			while (indicesToVisit.Count > 0) {
				Vector2Int currentCellIndex = indicesToVisit.Dequeue();
				matchModel.AddCellIndex(currentCellIndex);

				for (int i = 0; i < NeighborDirections.Length; i++) {
					Vector2Int neighborCellIndex = currentCellIndex + NeighborDirections[i];

					if (!IsCellInsideBoard(neighborCellIndex)
					 || !CanVisitCell(neighborCellIndex)
					 || !DropColorsMatch(currentCellIndex, neighborCellIndex))
						continue;

					visitedIndices[neighborCellIndex.x, neighborCellIndex.y] = true;
					indicesToVisit.Enqueue(neighborCellIndex);
				}
			}

			return matchModel;
		}

		private bool CanVisitCell(Vector2Int cellIndex) {
			return !(visitedIndices[cellIndex.x, cellIndex.y] || cells[cellIndex.x, cellIndex.y].IsEmpty());
		}

		private bool IsCellInsideBoard(Vector2Int cellIndex) {
			Vector2Int gridSize = puzzleGrid.GetGridSize();
			return cellIndex.x >= 0 && cellIndex.x < gridSize.x && cellIndex.y >= 0 && cellIndex.y < gridSize.y;
		}

		private bool DropColorsMatch(Vector2Int cellIndex, Vector2Int neighborCellIndex) {
			PuzzleCell currentCell = cells[cellIndex.x, cellIndex.y];
			PuzzleCell neighborCell = cells[neighborCellIndex.x, neighborCellIndex.y];
			if (currentCell.IsEmpty() || neighborCell.IsEmpty())
				return false;

			PuzzleElementDefinition currentDropDefinition = currentCell.GetColorDrop().GetDefinition();
			PuzzleElementDefinition neighborDropDefinition = neighborCell.GetColorDrop().GetDefinition();
			return currentDropDefinition == neighborDropDefinition;
		}

		private void ResetVisitedIndices() {
			Vector2Int gridSize = puzzleGrid.GetGridSize();
			for (int y = 0; y < gridSize.y; y++)
				for (int x = 0; x < gridSize.x; x++)
					visitedIndices[x, y] = false;
		}
	}
}
