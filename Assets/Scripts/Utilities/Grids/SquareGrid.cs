using UnityEngine;
using Utilities.Grids.NeighborHelpers;

namespace Utilities.Grids {
	public enum GridPlane { XY, XZ, YZ }

	public abstract class SquareGrid<T> : Grid<T> where T : SquareCell {
		public struct CellParams {
			public CellFactory<T> CellFactory { get; set; }
			public float CellDiameter { get; set; }
		}

		public struct GridParams {
			public Vector2Int GridSize { get; set; }
			public GridPlane GridPlane { get; set; }
		}

		protected readonly SquareGridNeighborHelper<T> neighborHelper;

		protected SquareGrid(GridParams gridParams, CellParams cellParams) {
			this.cellDiameter = cellParams.CellDiameter;
			this.gridSize = gridParams.GridSize;

			this.gridSizeInLength = GetFittingGridSize(gridSize);

			Vector3[] cellPositions = GenerateCellPositions(gridSize, gridParams.GridPlane);
			this.centerPoint = CalculateGridCenterPoint(cellPositions);
			this.cells = GenerateCells(cellParams.CellFactory, cellPositions);

			this.neighborHelper = new SquareGridNeighborHelper<T>(this, false);
		}

		private Vector3[] GenerateCellPositions(Vector2Int gridSizeInCells, GridPlane gridPlane) {
			Vector3[] cellPositions = new Vector3[gridSizeInCells.x * gridSizeInCells.y];
			Vector3 cellSpacing = new(cellDiameter, cellDiameter);
			Vector3 cellOffset = new(cellDiameter / 2, cellDiameter / 2);
			Vector2Int index = Vector2Int.zero;

			for (index.y = 0; index.y < gridSizeInCells.y; index.y++) {
				for (index.x = 0; index.x < gridSizeInCells.x; index.x++) {
					float posX = cellOffset.x + index.x * cellSpacing.x;
					float posY = cellOffset.y + index.y * cellSpacing.y;
					cellPositions[index.x + index.y * gridSizeInCells.x] = GetCellPosition(posX, posY, gridPlane);
				}
			}

			return cellPositions;
		}

		private Vector3 GetCellPosition(float posX, float posY, GridPlane gridPlane) {
			return gridPlane switch {
				GridPlane.XY => new Vector3(posX, posY),
				GridPlane.XZ => new Vector3(posX, 0, posY),
				GridPlane.YZ => new Vector3(0, posX, posY),
				_ => new Vector3(posX, posY)
			};
		}

		private Vector3 CalculateGridCenterPoint(Vector3[] cellPositions) {
			Vector3 positionSum = Vector3.zero;
			for (int i = 0; i < cellPositions.Length; i++)
				positionSum += cellPositions[i];

			return (positionSum / cellPositions.Length);
		}

		private Vector2 GetFittingGridSize(Vector2Int gridSizeInCells) {
			return new Vector2(gridSizeInCells.x * cellDiameter, gridSizeInCells.y * cellDiameter);
		}

		public T[] GetNeighbors(T cell) {
			return neighborHelper.GetCellNeighbors(cell);
		}
	}
}
