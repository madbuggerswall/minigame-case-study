using UnityEngine;

namespace Utilities.Grids {
	public abstract class SquareGrid<T> : Grid<T> where T : SquareCell {
		protected readonly T[,] cells;

		public struct CellParams {
			public CellFactory<T> CellFactory { get; set; }
			public float CellDiameter { get; set; }
		}

		public struct GridParams {
			public Vector2Int GridSize { get; set; }
			public GridPlane GridPlane { get; set; }
		}

		protected SquareGrid(GridParams gridParams, CellParams cellParams) {
			this.cellDiameter = cellParams.CellDiameter;
			this.gridSize = gridParams.GridSize;

			this.gridSizeInLength = GetFittingGridSize(gridSize, cellDiameter);
			Vector3[,] cellPositions = GenerateCellPositions(gridSize, gridParams.GridPlane);
			this.centerPoint = CalculateGridCenterPoint(cellPositions);
			this.cells = GenerateCells(cellParams.CellFactory, cellPositions);
		}

		private Vector3[,] GenerateCellPositions(Vector2Int gridSizeInCells, GridPlane gridPlane) {
			Vector3[,] cellPositions = new Vector3[gridSizeInCells.x, gridSizeInCells.y];
			Vector3 cellSpacing = new(cellDiameter, cellDiameter);
			Vector3 cellOffset = new(cellDiameter / 2, cellDiameter / 2);

			for (int y = 0; y < gridSizeInCells.y; y++) {
				for (int x = 0; x < gridSizeInCells.x; x++) {
					float posX = cellOffset.x + x * cellSpacing.x;
					float posY = cellOffset.y + y * cellSpacing.y;
					cellPositions[x, y] = GetCellPosition(posX, posY, gridPlane);
				}
			}

			return cellPositions;
		}

		private T[,] GenerateCells(CellFactory<T> cellFactory, Vector3[,] cellPositions) {
			T[,] cells = new T[gridSize.x, gridSize.y];

			for (int y = 0; y < gridSize.y; y++)
				for (int x = 0; x < gridSize.x; x++)
					cells[x, y] = cellFactory.Create(cellPositions[x, y], cellDiameter);

			return cells;
		}

		public T[,] GetCells() => cells;

		private static Vector3 CalculateGridCenterPoint(Vector3[,] cellPositions) {
			Vector3 positionSum = Vector3.zero;
			for (int i = 0; i < cellPositions.GetLength(0); i++)
				for (int j = 0; j < cellPositions.GetLength(1); j++)
					positionSum += cellPositions[i, j];

			return (positionSum / cellPositions.Length);
		}

		private static Vector2 GetFittingGridSize(Vector2Int gridSizeInCells, float cellDiameter) {
			return new Vector2(gridSizeInCells.x * cellDiameter, gridSizeInCells.y * cellDiameter);
		}
	}
}
