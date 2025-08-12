using UnityEngine;

namespace Utilities.Grids {
	public enum GridPlane { XY, XZ, YZ }

	public abstract class Grid<T> where T : Cell {
		protected Vector2 gridSizeInLength;
		protected Vector2Int gridSize;
		protected Vector3 centerPoint;
		protected float cellDiameter;

		// Getters
		public Vector2 GetGridSizeInLength() => gridSizeInLength;
		public Vector2Int GetGridSize() => gridSize;

		public float GetCellDiameter() => cellDiameter;
		public Vector3 GetCenterPoint() => centerPoint;

		// Static
		protected static Vector3 GetCellPosition(float posX, float posY, GridPlane gridPlane) {
			return gridPlane switch {
				GridPlane.XY => new Vector3(posX, posY),
				GridPlane.XZ => new Vector3(posX, 0, posY),
				GridPlane.YZ => new Vector3(0, posX, posY),
				_ => new Vector3(posX, posY)
			};
		}
	}
}
