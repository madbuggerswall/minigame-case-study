using MatchThree.PuzzleElements;
using UnityEngine;

namespace MatchThree {
	[System.Serializable]
	public class ElementPlacementDTO {
		[SerializeField] private PuzzleElementDefinition puzzleElementDefinition;
		[SerializeField] private int positionIndex;

		public PuzzleElementDefinition GetPuzzleElementDefinition() => puzzleElementDefinition;
		public int GetPositionIndex() => positionIndex;
	}
}
