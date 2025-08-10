using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Grids.NeighborHelpers {
	public class CircleGridNeighborHelper<T> where T : CircleCell {
		private readonly int gridWidth;
		private readonly int gridHeight;
		private readonly Dictionary<T, T[]> neighborsByCell;
		
		public CircleGridNeighborHelper(CircleGrid<T> circleGrid) {
			this.gridWidth = circleGrid.GetGridSize().x;
			this.gridHeight = circleGrid.GetGridSize().y;

			this.neighborsByCell = MapNeighborsByCell(circleGrid.GetCells());
		}

		public T[] GetCellNeighbors(T cell) {
			if (neighborsByCell.TryGetValue(cell, out T[] neighbors))
				return neighbors;

			return new T[] { };
		}

		private Dictionary<T, T[]> MapNeighborsByCell(T[] circleCells) {
			Dictionary<T, T[]> neighborsByCell = new();

			for (int i = 0; i < circleCells.Length; i++)
				neighborsByCell.Add(circleCells[i], GetCellNeighbors(circleCells, i));

			return neighborsByCell;
		}

		private Dictionary<CircleCell, int> MapCellsByIndex(CircleCell[] circleCells) {
			Dictionary<CircleCell, int> circleCellsByIndex = new();

			for (int i = 0; i < circleCells.Length; i++)
				circleCellsByIndex.Add(circleCells[i], i);

			return circleCellsByIndex;
		}

		private T[] GetCellNeighbors(T[] cells, int cellIndex) {
			int[] neighborIndices = GetCellNeighborIndices(cellIndex);
			T[] neighborCells = new T[neighborIndices.Length];

			for (int i = 0; i < neighborIndices.Length; i++)
				neighborCells[i] = cells[neighborIndices[i]];

			return neighborCells;
		}

		private int[] GetCellNeighborIndices(int cellIndex) {
			if (IsCornerCell(cellIndex))
				return GetCornerCellNeighborIndices(cellIndex);
			else if (IsEdgeCell(cellIndex))
				return GetEdgeCellNeighborIndices(cellIndex);
			else
				return GetCenterCellNeighborIndices(cellIndex);
		}

		private int[] GetCornerCellNeighborIndices(int cellIndex) {
			if (IsBottomLeftCorner(cellIndex))
				return GetSelectedCellNeighborIndices(cellIndex, 0, 1);
			else if (IsBottomRightCorner(cellIndex))
				return GetSelectedCellNeighborIndices(cellIndex, 2, 3);
			else if (IsTopLeftCorner(cellIndex))
				return GetSelectedCellNeighborIndices(cellIndex, 5, 0);
			else if (IsTopRightCorner(cellIndex))
				return GetSelectedCellNeighborIndices(cellIndex, 3, 4);
			else return new int[] { };
		}

		private int[] GetEdgeCellNeighborIndices(int cellIndex) {
			if (IsBottomEdge(cellIndex))
				return GetSelectedCellNeighborIndices(cellIndex, 0, 1, 2, 3);
			else if (IsTopEdge(cellIndex))
				return GetSelectedCellNeighborIndices(cellIndex, 0, 3, 4, 5);
			else if (IsLeftOuterEdge(cellIndex))
				return GetSelectedCellNeighborIndices(cellIndex, 0, 1, 5);
			else if (IsLeftInnerEdge(cellIndex))
				return GetSelectedCellNeighborIndices(cellIndex, 0, 1, 2, 4, 5);
			else if (IsRightOuterEdge(cellIndex))
				return GetSelectedCellNeighborIndices(cellIndex, 2, 3, 4);
			else if (IsRightInnerEdge(cellIndex))
				return GetSelectedCellNeighborIndices(cellIndex, 1, 2, 3, 4, 5);
			else return new int[] { };
		}

		private int[] GetCenterCellNeighborIndices(int cellIndex) {
			return new[] {
				cellIndex + 1,
				cellIndex + gridWidth + 1,
				cellIndex + gridWidth,
				cellIndex - 1,
				cellIndex - gridWidth - 1,
				cellIndex - gridWidth
			};
		}

		private int[] GetSelectedCellNeighborIndices(int cellIndex, params int[] selectedIndices) {
			int[] cellNeighborIndices = GetCenterCellNeighborIndices(cellIndex);
			int[] selectedCellNeighborIndices = new int[selectedIndices.Length];

			for (int i = 0; i < selectedIndices.Length; i++)
				selectedCellNeighborIndices[i] = cellNeighborIndices[selectedIndices[i]];

			return selectedCellNeighborIndices;
		}

		private bool IsCornerCell(int cellIndex) {
			return IsBottomLeftCorner(cellIndex)
			    || IsBottomRightCorner(cellIndex)
			    || IsTopLeftCorner(cellIndex)
			    || IsTopRightCorner(cellIndex);
		}

		private bool IsEdgeCell(int cellIndex) {
			bool isLeftEdge = IsLeftOuterEdge(cellIndex) || IsLeftInnerEdge(cellIndex);
			bool isRightEdge = IsRightOuterEdge(cellIndex) || IsRightInnerEdge(cellIndex);

			return IsBottomEdge(cellIndex) || IsTopEdge(cellIndex) || isLeftEdge || isRightEdge;
		}

		private int GetTopRightCorner() => gridWidth * gridHeight + GetEvenRowCount(gridHeight) - 1;
		private int GetTopLeftCorner() => gridWidth * (gridHeight - 1) + GetEvenRowCount(gridHeight - 1);
		private int GetBottomRightCorner() => gridWidth;
		private int GetBottomLeftCorner() => 0;

		private bool IsTopRightCorner(int cellIndex) => cellIndex == GetTopRightCorner();
		private bool IsTopLeftCorner(int cellIndex) => cellIndex == GetTopLeftCorner();
		private bool IsBottomRightCorner(int cellIndex) => cellIndex == GetBottomRightCorner();
		private bool IsBottomLeftCorner(int cellIndex) => cellIndex == GetBottomLeftCorner();

		private bool IsTopEdge(int cellIndex) => InRange(cellIndex, GetTopLeftCorner(), GetTopRightCorner());
		private bool IsBottomEdge(int cellIndex) => InRange(cellIndex, GetBottomLeftCorner(), GetBottomRightCorner());

		private bool IsRightInnerEdge(int cellIndex) => (cellIndex - 2 * gridWidth) % (2 * gridWidth + 1) == 0;
		private bool IsRightOuterEdge(int cellIndex) => (cellIndex - gridWidth) % (2 * gridWidth + 1) == 0;
		private bool IsLeftInnerEdge(int cellIndex) => (cellIndex - (gridWidth + 1)) % (2 * gridWidth + 1) == 0;
		private bool IsLeftOuterEdge(int cellIndex) => cellIndex % (2 * gridWidth + 1) == 0;

		private bool InRange(int value, int min, int max) => value > min && value < max;
		private int GetEvenRowCount(int height) => Mathf.CeilToInt(height / 2f);
	}
}
