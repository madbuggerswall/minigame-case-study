using System;
using UnityEngine;

namespace Utilities.Grids.SpatialHelpers {
	public enum AxialDirection { East, NorthEast, NorthWest, West, SouthWest, SouthEast }

	public struct CubeCoord {
		public int q;
		public int r;
		public int s;

		private static readonly CubeCoord[] DirectionVectors = {
			new(1, 0, -1), // East
			new(1, -1, 0), // North East
			new(0, -1, 1), // North West
			new(-1, 0, 1), // West
			new(-1, 1, 0), // South West
			new(0, 1, -1), // South East
		};

		public CubeCoord(int q, int r, int s) {
			this.q = q;
			this.r = r;
			this.s = s;
		}

		public CubeCoord GetNeighbor(int neighborIndex) {
			return this + GetDirection(neighborIndex);
		}

		public static CubeCoord Round(float fractionalQ, float fractionalR, float fractionalS) {
			// Round each cube component
			int roundedQ = (int) Math.Round(fractionalQ, MidpointRounding.AwayFromZero);
			int roundedR = (int) Math.Round(fractionalR, MidpointRounding.AwayFromZero);
			int roundedS = (int) Math.Round(fractionalS, MidpointRounding.AwayFromZero);

			// Calculate differences from the original fractional values
			float deltaQ = Mathf.Abs(roundedQ - fractionalQ);
			float deltaR = Mathf.Abs(roundedR - fractionalR);
			float deltaS = Mathf.Abs(roundedS - fractionalS);

			if (deltaQ > deltaR && deltaQ > deltaS)
				roundedQ = -roundedR - roundedS;
			else if (deltaR > deltaS)
				roundedR = -roundedQ - roundedS;
			else
				roundedS = -roundedQ - roundedR;

			return new CubeCoord(roundedQ, roundedR, roundedS);
		}

		public static CubeCoord[] Line(CubeCoord start, CubeCoord end) {
			int distance = Distance(start, end);
			if (distance == 0)
				return new[] { start };

			CubeCoord[] cubeCoords = new CubeCoord[distance + 1];
			for (int i = 0; i < distance + 1; i++) {
				(float q, float r, float s) floatingCubeCoord = Lerp(start, end, 1f / distance * i);
				cubeCoords[i] = Round(floatingCubeCoord.q, floatingCubeCoord.r, floatingCubeCoord.s);
			}

			return cubeCoords;
		}

		public static (float, float, float) Lerp(CubeCoord start, CubeCoord end, float t) {
			return (Mathf.Lerp(start.q, end.q, t), Mathf.Lerp(start.r, end.r, t), Mathf.Lerp(start.s, end.s, t));
		}

		public static CubeCoord[] Range(CubeCoord center, int range) {
			int coordCount = 1 + 3 * range * (range + 1);
			CubeCoord[] cubeCoords = new CubeCoord[coordCount];
			int i = 0;

			for (int q = -range; q <= range; q++)
				for (int r = Mathf.Max(-range, -q - range); r <= Mathf.Min(range, -q + range); r++)
					cubeCoords[i++] = new CubeCoord(q, r, -q - r) + center;

			return cubeCoords;
		}

		public static int Distance(CubeCoord lhs, CubeCoord rhs) {
			CubeCoord vec = lhs - rhs;
			return (Mathf.Abs(vec.q) + Mathf.Abs(vec.r) + Mathf.Abs(vec.s)) / 2;
		}

		public static CubeCoord GetDirection(int directionIndex) {
			return DirectionVectors[directionIndex];
		}

		public static CubeCoord GetDirection(AxialDirection axialDirection) {
			return DirectionVectors[(int) axialDirection];
		}

		// Operator overloads
		public static CubeCoord operator +(CubeCoord lhs, CubeCoord rhs) {
			return new CubeCoord(lhs.q + rhs.q, lhs.r + rhs.r, lhs.s + rhs.s);
		}

		public static CubeCoord operator -(CubeCoord lhs, CubeCoord rhs) {
			return new CubeCoord(lhs.q - rhs.q, lhs.r - rhs.r, lhs.s - rhs.s);
		}


		// Optional: equality support
		public override bool Equals(object obj) {
			return obj is CubeCoord other && q == other.q && r == other.r && s == other.s;
		}

		public override int GetHashCode() => new Vector3Int(q, r, s).GetHashCode();

		public static bool operator ==(CubeCoord lhs, CubeCoord rhs) {
			return lhs.q == rhs.q && lhs.r == rhs.r && lhs.s == rhs.s;
		}

		public static bool operator !=(CubeCoord lhs, CubeCoord rhs) => !(lhs == rhs);
	}
}
