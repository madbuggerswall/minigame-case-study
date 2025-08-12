using Core.Contexts;
using Core.DataTransfer.Definitions.PuzzleElements;
using Core.PuzzleLevels.Links;
using Core.UI;
using MatchThree.Model;
using Utilities.Contexts;

namespace Core.PuzzleLevels.Targets {
	public class TargetManager : IInitializable {
		private PuzzleElementTarget[] elementTargets;

		// Dependencies
		private PuzzleLevelInitializer levelInitializer;
		private PuzzleLevelUIController uiController;

		public void Initialize() {
			this.levelInitializer = SceneContext.GetInstance().Get<PuzzleLevelInitializer>();
			this.uiController = SceneContext.GetInstance().Get<PuzzleLevelUIController>();

			this.elementTargets = levelInitializer.GetElementTargets();
		}

		public void CheckForTargets(MatchModel matchModel) {
			CheckForElementTargets(matchModel);
		}

		public void CheckForElementTargets(MatchModel matchModel) {
			PuzzleElementDefinition linkDefinition = link.GetElementDefinition();

			for (int i = 0; i < elementTargets.Length; i++) {
				PuzzleElementTarget target = elementTargets[i];
				if (target.GetElementDefinition() != linkDefinition)
					continue;

				target.IncreaseCurrentAmount(link.GetElements().Count);
				uiController.UpdateElementTargetView(target);
			}
		}

		public bool IsAllTargetsCompleted() {
			for (int i = 0; i < elementTargets.Length; i++)
				if (!elementTargets[i].IsTargetCompleted())
					return false;

			return scoreTarget.IsTargetCompleted();
		}

		public PuzzleElementTarget[] GetElementTargets() => elementTargets;
	}
}
