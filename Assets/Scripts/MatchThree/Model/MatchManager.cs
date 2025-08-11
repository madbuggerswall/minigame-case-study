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
				CellModel cellModel = cellModels[x, y];
				if (visitedIndices[x, y])
					continue;

				if (cellModel.IsEmpty())
					continue;

				DropModel dropModel = cellModel.GetDropModel();
				MatchModel currentMatchModel = new MatchModel();

				// NOTE Rename this to visitQueue or something like that
				Queue<Vector2Int> queue = new();
				queue.Enqueue(new Vector2Int(x, y));

				visitedIndices[x, y] = true;

				while (queue.Count > 0) {
					Vector2Int cellIndex = queue.Dequeue();
					currentMatchModel.AddCellIndex(cellIndex);

					for (int i = 0; i < NeighborDirections.Length; i++) {
						Vector2Int neighborDirection = NeighborDirections[i];
						int neighborRow = cellIndex.x + neighborDirection.x;
						int neighborColumn = cellIndex.y + neighborDirection.y;
						Vector2Int neighborCellIndex = new(neighborRow, neighborColumn);

						if (!IsCellInsideBoard(neighborCellIndex, boardWidth, boardHeight))
							continue;

						if (visitedIndices[neighborRow, neighborColumn])
							continue;

						CellModel neighborCellModel = cellModels[neighborRow, neighborColumn];
						if (neighborCellModel.IsEmpty())
							continue;
						
						DropModel neighborDropModel = neighborCellModel.GetDropModel();
						if (dropModel.GetDropColor() != neighborDropModel.GetDropColor())
							continue;

						visitedIndices[neighborRow, neighborColumn] = true;
						queue.Enqueue(new Vector2Int(neighborRow, neighborColumn));
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
}
