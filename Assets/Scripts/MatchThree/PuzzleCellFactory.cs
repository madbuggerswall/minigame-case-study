using UnityEngine;
using Utilities.Grids;

namespace MatchThree {
	public class PuzzleCellFactory : CellFactory<PuzzleCell> {
		public override PuzzleCell Create(Vector3 cellPosition, float diameter) {
			return new PuzzleCell(cellPosition, diameter);
		}
	}
}
