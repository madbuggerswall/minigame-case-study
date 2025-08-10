using UnityEngine;
using static UnityEngine.Mathf;

namespace Utilities.Grids.SpatialHelpers {
	public struct AxialCoord {
		public int q;
		public int r;

		private static readonly AxialCoord[] DirectionVectors = {
			new(1, 0),  // East
			new(1, -1), // North East
			new(0, -1), // North West
			new(-1, 0), // West
			new(-1, 1), // South West
			new(0, 1)   // South East
		};

		public AxialCoord(int q, int r) {
			this.q = q;
			this.r = r;
		}

		public AxialCoord GetNeighbor(AxialCoord center, int neighborIndex) {
			return center + DirectionVectors[neighborIndex];
		}

		// No matter which way you write it, axial hex distance is derived from the Manhattan distance on cubes.
		public static int Distance(AxialCoord lhs, AxialCoord rhs) {
			CubeCoord lhsCube = lhs.ToCubeCoord();
			CubeCoord rhsCube = rhs.ToCubeCoord();
			return CubeCoord.Distance(lhsCube, rhsCube);
		}

		public static Vector2 AxialToWorld(AxialCoord axialCoord, float cellRadius) {
			float size = 2f / Sqrt(3f) * cellRadius;
			float x = Sqrt(3) * axialCoord.q + Sqrt(3) / 2 * axialCoord.r;
			float y = 3f / 2f * axialCoord.r;
			return new Vector2(x * size, -y * size);
		}

		public static AxialCoord WorldToAxial(Vector2 worldPosition, float cellDiameter) {
			float cellRadius = cellDiameter / 2;

			// Hexagon (pointy-top)
			float size = 2f / Sqrt(3f) * cellRadius;
			float x = worldPosition.x / size;
			float y = -worldPosition.y / size;

			// Cartesian to hex
			float fractionalQ = Sqrt(3f) / 3f * x - 1f / 3f * y;
			float fractionalR = 2f / 3f * y;
			float fractionalS = -fractionalQ - fractionalR;

			CubeCoord cubeCoord = CubeCoord.Round(fractionalQ, fractionalR, fractionalS);
			return cubeCoord.ToAxialCoord();
		}

		public override string ToString() => $"({q}, {r})";

		// Operator overloads
		public static AxialCoord operator +(AxialCoord lhs, AxialCoord rhs) => new(lhs.q + rhs.q, lhs.r + rhs.r);
		public static AxialCoord operator -(AxialCoord lhs, AxialCoord rhs) => new(lhs.q - rhs.q, lhs.r - rhs.r);


		// Optional: equality support
		public override bool Equals(object obj) => obj is AxialCoord other && q == other.q && r == other.r;
		public override int GetHashCode() => new Vector2Int(q, r).GetHashCode();

		public static bool operator ==(AxialCoord lhs, AxialCoord rhs) => lhs.q == rhs.q && lhs.r == rhs.r;
		public static bool operator !=(AxialCoord lhs, AxialCoord rhs) => !(lhs == rhs);
	}
}
