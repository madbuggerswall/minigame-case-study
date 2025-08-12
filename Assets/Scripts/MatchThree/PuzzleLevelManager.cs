using System.Collections.Generic;
using MatchThree.MechanicsHelpers;
using MatchThree.Model;
using MatchThree.PuzzleElements;
using MatchThree.Targets;
using MatchThree.Turns;
using MatchThree.UI;
using Utilities.Contexts;

namespace MatchThree {
	public class PuzzleLevelManager : IInitializable {
		// Dependencies
		private PuzzleLevelInitializer levelInitializer;
		private PuzzleLevelViewController viewController;
		private PuzzleLevelUIController uiController;
		private ColorDropDefinitionManager colorDropDefinitionManager;
		private TurnManager turnManager;
		private TargetManager targetManager;

		// Helpers
		private FallHelper fallHelper;
		private FillHelper fillHelper;
		// TODO SwapHelper

		// Fields
		private PuzzleGrid puzzleGrid;

		public void Initialize() {
			levelInitializer = MatchThreeContext.GetInstance().Get<PuzzleLevelInitializer>();
			colorDropDefinitionManager = MatchThreeContext.GetInstance().Get<ColorDropDefinitionManager>();
			viewController = MatchThreeContext.GetInstance().Get<PuzzleLevelViewController>();
			uiController = MatchThreeContext.GetInstance().Get<PuzzleLevelUIController>();
			turnManager = MatchThreeContext.GetInstance().Get<TurnManager>();
			targetManager = MatchThreeContext.GetInstance().Get<TargetManager>();

			puzzleGrid = levelInitializer.GetPuzzleGrid();

			fallHelper = new FallHelper(this);
			fillHelper = new FillHelper(this);

			// TODO This will be local events handled via Actions
			
			// SignalBus.GetInstance().ClearListeners<ElementExplodedSignal>();
			// SignalBus.GetInstance().ClearListeners<ContextInitializedSignal>();
			// SignalBus.GetInstance().SubscribeTo<ElementExplodedSignal>(OnElementExploded);
			// SignalBus.GetInstance().SubscribeTo<ContextInitializedSignal>(OnContextInitialized);
		}

		// TODO OnElementsMatch
		// TODO OnContextInitialized -> check for initial matches
		
		
		// private void OnElementExploded(ElementExplodedSignal signal) {
		// 	PuzzleElement puzzleElement = signal.PuzzleElement;
		// 	viewController.DespawnElementBehaviour(puzzleElement);
		// }
		//
		// private void OnContextInitialized(ContextInitializedSignal signal) {
		// 	if (!shuffleHelper.IsShuffleNeeded())
		// 		return;
		//
		// 	shuffleHelper.Shuffle();
		// 	viewController.ShuffleViewHelper.MoveShuffledElements();
		// }


		// public void Explode(ExplodeLinkCommand command, Link link) {
		// 	if (!link.IsValid(puzzleGrid)) {
		// 		command.InvokeCompletionHandlers();
		// 		return;
		// 	}
		//
		// 	link.Explode(puzzleGrid);
		// 	turnManager.OnTurnMade();
		// 	targetManager.CheckForTargets(link);
		//
		// 	fallHelper.ApplyFall();
		// 	fillHelper.ApplyFill();
		//
		// 	HashSet<PuzzleElement> fallenElements = fallHelper.GetFallenElements();
		// 	HashSet<PuzzleElement> filledElements = fillHelper.GetFilledElements();
		//
		// 	viewController.FallViewHelper.MoveFallenElements(fallenElements);
		// 	viewController.ViewReadyNotifier.WaitForFallTweens();
		//
		// 	viewController.FillViewHelper.MoveFilledElements(filledElements);
		// 	viewController.ViewReadyNotifier.WaitForFillTweens();
		//
		// 	// if (shuffleHelper.IsShuffleNeeded()) {
		// 	// 	viewController.ViewReadyNotifier.OnReadyForShuffle.AddListener(OnReadyForShuffle);
		// 	// 	viewController.ViewReadyNotifier.WaitShuffleForTweens();
		// 	// }
		//
		// 	viewController.ViewReadyNotifier.OnViewReady.AddListener(command.InvokeCompletionHandlers);
		// 	viewController.ViewReadyNotifier.OnViewReady.AddListener(CheckLevelSuccess);
		// 	viewController.ViewReadyNotifier.OnViewReady.AddListener(CheckLevelFail);
		// }

		private void CheckLevelSuccess() {
			if (!targetManager.IsAllTargetsCompleted())
				return;

			uiController.ShowLevelSuccessPanel();
		}

		private void CheckLevelFail() {
			if (turnManager.IsTurnsLeft() || targetManager.IsAllTargetsCompleted())
				return;

			uiController.ShowLevelFailPanel();
		}

		// private void OnReadyForShuffle() {
		//
		// }

		public ColorDrop CreateRandomColorChip() {
			ColorDropDefinition definition = colorDropDefinitionManager.GetRandomColorChipDefinition();
			return new ColorDrop(definition);
		}

		// Getters
		public PuzzleGrid GetPuzzleGrid() => puzzleGrid;
	}
}
