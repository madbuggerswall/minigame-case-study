using System.Collections.Generic;
using MatchThree.MechanicsHelpers;
using MatchThree.PuzzleElements;
using MatchThree.Targets;
using MatchThree.Turns;
using MatchThree.UI;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Signals;

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
		private MatchFinder matchFinder;

		public void Initialize() {
			levelInitializer = PuzzleContext.GetInstance().Get<PuzzleLevelInitializer>();
			colorDropDefinitionManager = PuzzleContext.GetInstance().Get<ColorDropDefinitionManager>();
			viewController = PuzzleContext.GetInstance().Get<PuzzleLevelViewController>();
			uiController = PuzzleContext.GetInstance().Get<PuzzleLevelUIController>();
			turnManager = PuzzleContext.GetInstance().Get<TurnManager>();
			targetManager = PuzzleContext.GetInstance().Get<TargetManager>();

			puzzleGrid = levelInitializer.GetPuzzleGrid();
			matchFinder = new MatchFinder(puzzleGrid);

			fallHelper = new FallHelper(this);
			fillHelper = new FillHelper(this);

			SignalBus signalBus = SceneContext.GetInstance().Get<SignalBus>();
			signalBus.SubscribeTo<PuzzleContextInitializedSignal>(OnPuzzleContextInitialized);

			// TODO This will be local events handled via Actions
			// SignalBus.GetInstance().ClearListeners<ElementExplodedSignal>();
			// SignalBus.GetInstance().ClearListeners<ContextInitializedSignal>();
			// SignalBus.GetInstance().SubscribeTo<ElementExplodedSignal>(OnElementExploded);
			// SignalBus.GetInstance().SubscribeTo<ContextInitializedSignal>(OnContextInitialized);
		}

		private void OnPuzzleContextInitialized(PuzzleContextInitializedSignal signal) {
			CheckForMatches();
		}

		private void CheckForMatches() {
			List<Match> matches = matchFinder.FindMatches();
			if (matches.Count == 0)
				return;

			List<Vector2Int> matchedCellIndices = new();
			for (int i = 0; i < matches.Count; i++)
				matchedCellIndices.AddRange(matches[i].GetCellIndices().AsReadOnly());

			OnMatchesFound(matchedCellIndices);
		}

		private void OnMatchesFound(List<Vector2Int> matchedCellIndices) {
			BlastMatchedCells(matchedCellIndices);

			fallHelper.ApplyFall();
			fillHelper.ApplyFill();

			viewController.FallViewHelper.MoveFallenElements(fallHelper.GetFallenElements());
			viewController.ViewReadyNotifier.WaitForFallTweens();

			viewController.FillViewHelper.MoveFilledElements(fillHelper.GetFilledElements());
			viewController.ViewReadyNotifier.WaitForFillTweens();

			viewController.ViewReadyNotifier.OnViewReady.AddListener(CheckForMatches);
		}

		private void BlastMatchedCells(List<Vector2Int> matchedCellIndices) {
			PuzzleCell[,] cells = puzzleGrid.GetCells();
			for (int i = 0; i < matchedCellIndices.Count; i++) {
				Vector2Int matchedCellIndex = matchedCellIndices[i];
				PuzzleCell blastedCell = cells[matchedCellIndex.x, matchedCellIndex.y];
				PuzzleElement matchedElement = blastedCell.GetPuzzleElement();

				blastedCell.SetPuzzleElement(null);
				viewController.DespawnElementBehaviour(matchedElement);
			}
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
