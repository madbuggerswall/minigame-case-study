using UnityEngine;

namespace Utilities.Grids.SpatialHelpers.Hexagonal {
	public struct DoubleWidthCoord {
		public int column;
		public int row;

		private static readonly DoubleWidthCoord[] DirectionVectors = {
			new(2, 0),   // East
			new(1, -1),  // North East
			new(-1, -1), // North West
			new(-2, 0),  // West
			new(-1, 1),  // South West
			new(1, 1)    // South East
		};

		public DoubleWidthCoord(int column, int row) {
			this.column = column;
			this.row = row;
		}

		public DoubleWidthCoord GetNeighbor(int directionIndex) {
			return this + GetDirection(directionIndex);
		}

		public static DoubleWidthCoord GetDirection(int directionIndex) {
			return DirectionVectors[directionIndex];
		}

		public static int Distance(DoubleWidthCoord lhs, DoubleWidthCoord rhs) {
			int columnDiff = Mathf.Abs(lhs.column - rhs.column);
			int rowDiff = Mathf.Abs(lhs.row - rhs.row);
			return rowDiff + Mathf.Max(0, (columnDiff - rowDiff) / 2);
		}


		// Operator overloads
		public static DoubleWidthCoord operator +(DoubleWidthCoord lhs, DoubleWidthCoord rhs) {
			return new DoubleWidthCoord(lhs.column + rhs.column, lhs.row + rhs.row);
		}

		public static DoubleWidthCoord operator -(DoubleWidthCoord lhs, DoubleWidthCoord rhs) {
			return new DoubleWidthCoord(lhs.column - rhs.column, lhs.row - rhs.row);
		}
	}
}
