namespace MatchThree.Commands {
	public class SwapCommand : Command {
		private readonly PuzzleLevelManager levelManager;

		public SwapCommand(PuzzleLevelManager levelManager) {
			this.levelManager = levelManager;
		}

		public override void Execute() {
			// levelManager.Explode(this, link);
		}
	}
}
