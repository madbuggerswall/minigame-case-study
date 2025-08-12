namespace MatchThree.PuzzleElements {
	public abstract class PuzzleElement {
		private readonly PuzzleElementDefinition definition;

		protected PuzzleElement(PuzzleElementDefinition definition) {
			this.definition = definition;
		}

		// Getters
		public PuzzleElementDefinition GetDefinition() => definition;
	}
}
