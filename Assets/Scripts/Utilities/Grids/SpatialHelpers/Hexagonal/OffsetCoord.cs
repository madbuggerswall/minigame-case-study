namespace Utilities.Grids.SpatialHelpers.Hexagonal {
	public struct OffsetOddRCoord {
		public int column;
		public int row;

		private static readonly OffsetOddRCoord[,] DirectionVectors = {
			// Even rows (row % 2 == 0)
			{ new(1, 0), new(0, -1), new(-1, -1), new(-1, 0), new(-1, 1), new(0, 1) },
			// Odd rows (row % 2 == 1)
			{ new(1, 0), new(1, -1), new(0, -1), new(-1, 0), new(0, 1), new(1, 1) }
		};

		public OffsetOddRCoord(int column, int row) {
			this.column = column;
			this.row = row;
		}

		public OffsetOddRCoord GetNeighbor(int neighborIndex) {
			int parity = this.row & 1;
			OffsetOddRCoord diff = DirectionVectors[parity, neighborIndex];
			return new OffsetOddRCoord(this.column + diff.column, this.row + diff.row);
		}

		public static int Distance(OffsetOddRCoord lhs, OffsetOddRCoord rhs) {
			return AxialCoord.Distance(lhs.ToAxial(), rhs.ToAxial());
		}
	}
}
