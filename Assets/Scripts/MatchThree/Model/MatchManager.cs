using System.Collections.Generic;
using MatchThree.Model;
using UnityEngine;

public class MatchManager {
	// For a match to occur, it should be greater than 3 blocks in all directions or 2x2

	private static readonly Vector2Int[] NeighborDirections = new[] {
		new Vector2Int(1, 0),  // Right
		new Vector2Int(0, 1),  // Up
		new Vector2Int(-1, 0), // Left
		new Vector2Int(0, -1)  // Down
	};

	// Dependencies
	private BoardModel boardModel;

	public void Initialize(BoardModel boardModel) {
		this.boardModel = boardModel;
	}

	public List<MatchModel> FindMatches() {
		CellModel[,] cellModels = boardModel.GetCellModels();
		int boardWidth = cellModels.GetLength(0);
		int boardHeight = cellModels.GetLength(1);

		bool[,] visitedIndices = new bool[boardWidth, boardHeight];
		List<MatchModel> matchModels = new List<MatchModel>();

		for (int y = 0; y < boardHeight; y++) {
			for (int x = 0; x < boardWidth; x++) {
				CellModel currentCellModel = cellModels[x, y];
				if (visitedIndices[x, y])
					continue;

				if (currentCellModel.IsEmpty())
					continue;
				
				Queue<Vector2Int> cellsToVisit = new();
				cellsToVisit.Enqueue(new Vector2Int(x, y));

				MatchModel currentMatchModel = new();
				visitedIndices[x, y] = true;

				while (cellsToVisit.Count > 0) {
					Vector2Int cellIndex = cellsToVisit.Dequeue();
					currentMatchModel.AddCellIndex(cellIndex);

					for (int i = 0; i < NeighborDirections.Length; i++) {
						Vector2Int neighborCellIndex = cellIndex + NeighborDirections[i];
						if (!IsCellInsideBoard(neighborCellIndex, boardWidth, boardHeight))
							continue;

						if (visitedIndices[neighborCellIndex.x, neighborCellIndex.y])
							continue;

						CellModel neighborCellModel = cellModels[neighborCellIndex.x, neighborCellIndex.y];
						if (neighborCellModel.IsEmpty())
							continue;

						if (!DropColorsMatch(currentCellModel.GetDropModel(), neighborCellModel.GetDropModel()))
							continue;

						visitedIndices[neighborCellIndex.x, neighborCellIndex.y] = true;
						cellsToVisit.Enqueue(neighborCellIndex);
					}
				}

				currentMatchModel.EvaluateAxes();
				if (currentMatchModel.IsValid())
					matchModels.Add(currentMatchModel);
			}
		}

		return matchModels;
	}

	private bool IsCellInsideBoard(Vector2Int cellIndex, int boardWidth, int boardHeight) {
		return cellIndex.x >= 0 && cellIndex.x < boardWidth && cellIndex.y >= 0 && cellIndex.y < boardHeight;
	}

	private bool DropColorsMatch(DropModel current, DropModel neighbor) {
		return current.GetDropColor() == neighbor.GetDropColor();
	}
}
