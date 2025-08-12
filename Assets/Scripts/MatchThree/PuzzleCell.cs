using MatchThree.PuzzleElements;
using UnityEngine;
using Utilities.Grids;

namespace MatchThree {
	public class PuzzleCell : SquareCell {
		// TODO Replace this with puzzle element
		private PuzzleElement puzzleElement;

		public PuzzleCell(Vector3 worldPosition, float diameter) : base(worldPosition, diameter) { }


		public void SetPuzzleElement(PuzzleElement puzzleElement) => this.puzzleElement = puzzleElement;
		public bool IsEmpty() => puzzleElement is null;
		public PuzzleElement GetPuzzleElement() => puzzleElement;
	}
}
