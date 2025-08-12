using MatchThree.PuzzleElements;
using MatchThree.UI;
using Utilities.Contexts;

namespace MatchThree.Targets {
	public class TargetManager : IInitializable {
		private PuzzleElementTarget[] elementTargets;

		// Dependencies
		private PuzzleLevelInitializer levelInitializer;
		private PuzzleLevelUIController uiController;

		public void Initialize() {
			this.levelInitializer = PuzzleContext.GetInstance().Get<PuzzleLevelInitializer>();
			this.uiController = PuzzleContext.GetInstance().Get<PuzzleLevelUIController>();

			this.elementTargets = levelInitializer.GetElementTargets();
		}

		public void CheckForTargets(Match match) {
			CheckForElementTargets(match);
		}

		public void CheckForElementTargets(Match match) {
			// TODO
			// PuzzleElementDefinition linkDefinition = link.GetElementDefinition();
			//
			// for (int i = 0; i < elementTargets.Length; i++) {
			// 	PuzzleElementTarget target = elementTargets[i];
			// 	if (target.GetElementDefinition() != linkDefinition)
			// 		continue;
			//
			// 	target.IncreaseCurrentAmount(link.GetElements().Count);
			// 	uiController.UpdateElementTargetView(target);
			// }
		}

		public bool IsAllTargetsCompleted() {
			for (int i = 0; i < elementTargets.Length; i++)
				if (!elementTargets[i].IsTargetCompleted())
					return false;

			return true;
		}

		public PuzzleElementTarget[] GetElementTargets() => elementTargets;
	}
}
