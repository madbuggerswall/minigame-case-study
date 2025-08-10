using UnityEngine;

namespace Utilities.Grids.SpatialHelpers {
	public static class AxialConversionExtensions {
		// Axial
		public static Vector2Int ToVector2Int(this AxialCoord axialCoord) {
			return new Vector2Int(axialCoord.q, axialCoord.r);
		}

		public static CubeCoord ToCubeCoord(this AxialCoord axialCoord) {
			int cubeQ = axialCoord.q;
			int cubeR = axialCoord.r;
			int cubeS = -axialCoord.q - axialCoord.r;
			return new CubeCoord(cubeQ, cubeR, cubeS);
		}

		public static DoubleWidthCoord ToDoubleWidth(this AxialCoord axialCoord) {
			int column = 2 * axialCoord.q + axialCoord.r;
			int row = axialCoord.r;
			return new DoubleWidthCoord(column, row);
		}

		public static OffsetOddRCoord ToOddR(this AxialCoord axialCoord) {
			int parity = axialCoord.r & 1; // 0 if even, 1 if odd
			int column = axialCoord.q + (axialCoord.r - parity) / 2;
			int row = axialCoord.r;
			return new OffsetOddRCoord(column, row);
		}

		// Cube
		public static Vector3Int ToVector3Int(this CubeCoord cubeCoord) {
			return new Vector3Int(cubeCoord.q, cubeCoord.r, -cubeCoord.s);
		}

		public static Vector3 ToVector3(this CubeCoord cubeCoord) {
			return new Vector3(cubeCoord.q, cubeCoord.r, -cubeCoord.s);
		}

		public static AxialCoord ToAxialCoord(this CubeCoord cubeCoord) {
			int axialQ = cubeCoord.q;
			int axialR = cubeCoord.r;
			return new AxialCoord(axialQ, axialR);
		}

		// Offset Coords
		public static AxialCoord ToAxial(this OffsetOddRCoord offsetOddRCoord) {
			int parity = offsetOddRCoord.row & 1;
			int q = offsetOddRCoord.column - (offsetOddRCoord.row - parity) / 2;
			int r = offsetOddRCoord.row;
			return new AxialCoord(q, r);
		}

		// DoubleWidth
		public static AxialCoord ToAxial(this DoubleWidthCoord doubledCoord) {
			int q = (doubledCoord.column - doubledCoord.row) / 2;
			int r = doubledCoord.row;
			return new AxialCoord(q, r);
		}
	}
}
