using UnityEngine;

namespace Utilities.Grids.SpatialHelpers.Rectangular {
	// TODO Rename this class
	public class GridCellHelper {
		public static Vector2Int WorldToCell(Vector3 worldPos, float cellDiameter, GridPlane gridPlane) {
			return gridPlane switch {
				GridPlane.XY => WorldToCellXY(worldPos, cellDiameter),
				GridPlane.XZ => WorldToCellXZ(worldPos, cellDiameter),
				GridPlane.YZ => WorldToCellYZ(worldPos, cellDiameter),
				_ => WorldToCellXY(worldPos, cellDiameter)
			};
		}

		private static Vector2Int WorldToCellXY(Vector3 worldPos, float cellDiameter) {
			int x = Mathf.FloorToInt(worldPos.x / cellDiameter);
			int y = Mathf.FloorToInt(worldPos.y / cellDiameter);
			return new Vector2Int(x, y);
		}

		private static Vector2Int WorldToCellXZ(Vector3 worldPos, float cellDiameter) {
			int x = Mathf.FloorToInt(worldPos.x / cellDiameter);
			int y = Mathf.FloorToInt(worldPos.z / cellDiameter);
			return new Vector2Int(x, y);
		}

		private static Vector2Int WorldToCellYZ(Vector3 worldPos, float cellDiameter) {
			int x = Mathf.FloorToInt(worldPos.y / cellDiameter);
			int y = Mathf.FloorToInt(worldPos.z / cellDiameter);
			return new Vector2Int(x, y);
		}

		public static Vector3 CellToWorld(Vector2Int cell, float cellDiameter, GridPlane gridPlane) {
			float x = (cell.x + 0.5f) * cellDiameter;
			float y = (cell.y + 0.5f) * cellDiameter;

			return gridPlane switch {
				GridPlane.XY => new Vector3(x, y, 0),
				GridPlane.XZ => new Vector3(x, 0, y),
				GridPlane.YZ => new Vector3(0, x, y),
				_ => new Vector3(x, y, 0)
			};
		}
	}
}
