using System.Collections.Generic;
using UnityEngine;

// For a match to occur, it should be greater than 3 blocks in all directions or 2x2
// NOTE Rename it to MatchFinder or MatchHelper
namespace MatchThree.Model {
	public class MatchManager {
		private static readonly Vector2Int[] NeighborDirections = new[] {
			new Vector2Int(1, 0),  // Right
			new Vector2Int(0, 1),  // Up
			new Vector2Int(-1, 0), // Left
			new Vector2Int(0, -1)  // Down
		};

		private BoardModel boardModel;
		private readonly CellModel[,] cellModels;
		private readonly int boardWidth;
		private readonly int boardHeight;

		private readonly bool[,] visitedIndices;
		private readonly Queue<Vector2Int> indicesToVisit;

		private readonly List<MatchModel> matchModels;

		public MatchManager(BoardModel boardModel) {
			this.boardModel = boardModel;
			this.cellModels = boardModel.GetCellModels();
			this.boardWidth = boardModel.GetWidth();
			this.boardHeight = boardModel.GetHeight();

			this.visitedIndices = new bool[boardWidth, boardHeight];
			this.indicesToVisit = new Queue<Vector2Int>();
			this.matchModels = new List<MatchModel>();
		}

		public List<MatchModel> FindMatches() {
			ResetVisitedIndices();
			matchModels.Clear();

			for (int y = 0; y < boardHeight; y++) {
				for (int x = 0; x < boardWidth; x++) {
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
			return !(visitedIndices[cellIndex.x, cellIndex.y] || cellModels[cellIndex.x, cellIndex.y].IsEmpty());
		}

		private bool IsCellInsideBoard(Vector2Int cellIndex) {
			return cellIndex.x >= 0 && cellIndex.x < boardWidth && cellIndex.y >= 0 && cellIndex.y < boardHeight;
		}

		private bool DropColorsMatch(Vector2Int cellIndex, Vector2Int neighborCellIndex) {
			CellModel currentCell = cellModels[cellIndex.x, cellIndex.y];
			CellModel neighborCell = cellModels[neighborCellIndex.x, neighborCellIndex.y];
			if (currentCell.IsEmpty() || neighborCell.IsEmpty())
				return false;

			DropColor currentDropColor = currentCell.GetDropModel().GetDropColor();
			DropColor neighborDropColor = neighborCell.GetDropModel().GetDropColor();
			return currentDropColor == neighborDropColor;
		}

		private void ResetVisitedIndices() {
			for (int y = 0; y < boardHeight; y++)
				for (int x = 0; x < boardWidth; x++)
					visitedIndices[x, y] = false;
		}
	}
}
