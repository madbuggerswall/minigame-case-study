using MatchThree.UI;
using Utilities.Contexts;

namespace MatchThree.Turns {
	public class TurnManager : IInitializable {
		private int maxTurnCount;
		private int currentTurnCount;

		// Dependencies
		private PuzzleLevelUIController uiController;
		private PuzzleLevelInitializer levelInitializer;

		public void Initialize() {
			this.uiController = PuzzleContext.GetInstance().Get<PuzzleLevelUIController>();
			this.levelInitializer = PuzzleContext.GetInstance().Get<PuzzleLevelInitializer>();

			this.maxTurnCount = levelInitializer.GetMaxMoveCount();
		}

		public void OnTurnMade() {
			currentTurnCount++;
			uiController.UpdateRemainingTurnsPanel(GetRemainingTurnCount());
		}

		public bool IsTurnsLeft() => maxTurnCount - currentTurnCount > 0;
		public int GetRemainingTurnCount() => maxTurnCount - currentTurnCount;
	}
}
