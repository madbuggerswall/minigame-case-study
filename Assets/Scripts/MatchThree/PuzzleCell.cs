using MatchThree.PuzzleElements;
using UnityEngine;
using Utilities.Grids;

namespace MatchThree {
	public class PuzzleCell : SquareCell {
		// TODO Replace this with puzzle element
		private ColorDrop colorDrop;

		public PuzzleCell(Vector3 worldPosition, float diameter) : base(worldPosition, diameter) { }


		public void SetColorDrop(ColorDrop colorDrop) => this.colorDrop = colorDrop;
		public bool IsEmpty() => colorDrop is null;
		public ColorDrop GetColorDrop() => colorDrop;
	}
}
