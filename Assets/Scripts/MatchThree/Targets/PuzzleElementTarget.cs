using MatchThree.PuzzleElements;

namespace MatchThree.Targets {
	public class PuzzleElementTarget : Target {
		private readonly PuzzleElementDefinition elementDefinition;
		private readonly int targetAmount;
		private int currentAmount;

		public PuzzleElementTarget(PuzzleElementTargetDTO targetDTO) {
			this.elementDefinition = targetDTO.GetTargetDefinition();
			this.targetAmount = targetDTO.GetTargetAmount();
			this.currentAmount = 0;
		}

		public override bool IsTargetCompleted() => currentAmount >= targetAmount;
		public int IncreaseCurrentAmount(int amount) => currentAmount += amount;

		// Getters
		public int GetTargetAmount() => targetAmount;
		public int GetCurrentAmount() => currentAmount;
		public PuzzleElementDefinition GetElementDefinition() => elementDefinition;
	}
}
