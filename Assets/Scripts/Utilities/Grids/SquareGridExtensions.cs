using UnityEngine;

namespace Utilities.Grids {
	// Not so much an extension class, more a container for unused methods.
	public static class SquareGridExtensions {
		private static Vector2Int GetFittingGridSizeInCells(Vector2 gridSize, float cellDiameter) {
			int widthInCells = Mathf.CeilToInt(gridSize.x / cellDiameter);
			int heightInCells = Mathf.CeilToInt(gridSize.y / cellDiameter);
			return new Vector2Int(widthInCells, heightInCells);
		}

		private static Vector2[,] GenerateIndexedCellPositions(Vector2Int gridSizeInCells, float cellDiameter) {
			Vector2[,] cellPositions = new Vector2[gridSizeInCells.x, gridSizeInCells.y];
			Vector2 cellSpacing = new(cellDiameter, cellDiameter);
			Vector2 cellOffset = new(cellDiameter / 2, cellDiameter / 2);
			Vector2Int index = Vector2Int.zero;

			for (index.y = 0; index.y < gridSizeInCells.y; index.y++) {
				for (index.x = 0; index.x < gridSizeInCells.x; index.x++) {
					float cellPosX = cellOffset.x + index.x * cellSpacing.x;
					float cellPosY = cellOffset.y + index.y * cellSpacing.y;
					cellPositions[index.x, index.y] = new Vector2(cellPosX, cellPosY);
				}
			}

			return cellPositions;
		}
	}
}
