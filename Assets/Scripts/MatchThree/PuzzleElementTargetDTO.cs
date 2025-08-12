using MatchThree.PuzzleElements;
using MatchThree.Targets;
using UnityEngine;

namespace MatchThree {
	[System.Serializable]
	public class PuzzleElementTargetDTO : TargetDTO {
		[SerializeField] private PuzzleElementDefinition puzzleElementDefinition;
		[SerializeField] private int targetAmount;

		public PuzzleElementDefinition GetTargetDefinition() => puzzleElementDefinition;
		public int GetTargetAmount() => targetAmount;

		public override Target CreateTarget() => new PuzzleElementTarget(this);
	}
}
