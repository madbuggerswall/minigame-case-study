using Core.PuzzleElements.Behaviours;
using Core.PuzzleLevels;
using Core.PuzzleLevels.LevelView;
using Core.PuzzleLevels.Targets;
using Core.PuzzleLevels.Turns;
using Core.UI;
using MatchThree.Presenter;
using MatchThree.View;
using UnityEngine;
using Utilities.Contexts;

[DefaultExecutionOrder(-28)]
public class MatchThreeContext : SubContext<MatchThreeContext> {
	protected override void ResolveContext() {
		Resolve<MatchThreeInputManager>();

		Resolve<BoardViewFactory>();
		Resolve<CellViewFactory>();
		Resolve<DropViewFactory>();

		Resolve<MainPresenter>();
		Resolve<BoardPresenter>();
	}

	protected override void OnInitialized() { }
}

public class PuzzleContext : SubContext<PuzzleContext> {
	protected override void ResolveContext() {
		Resolve<PuzzleCellBehaviourFactory>();
		Resolve<PuzzleGridBehaviourFactory>();
		Resolve<PuzzleElementBehaviourFactory>();
		Resolve<PuzzleLevelInitializer>();
		Resolve<PuzzleLevelManager>();
		
		Resolve<TurnManager>();
		Resolve<TargetManager>();
		Resolve<PuzzleLevelViewController>();
		Resolve<PuzzleLevelUIController>();
	}
	protected override void OnInitialized() {
		throw new System.NotImplementedException();
	}
}

// TODO Each context should have its own object pool to handled undesired remains on the main pool.
