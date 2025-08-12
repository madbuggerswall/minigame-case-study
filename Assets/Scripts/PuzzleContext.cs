using Core.PuzzleGrids;
using MatchThree;
using MatchThree.PuzzleElements;
using MatchThree.Targets;
using MatchThree.Turns;
using MatchThree.UI;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

public class PuzzleContext : SubContext<PuzzleContext> {
	protected override void ResolveContext() {
		Resolve<ObjectPool>();
		Resolve<ColorDropDefinitionManager>();
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
		Debug.Log("Puzzle Context Initialized");
		// throw new System.NotImplementedException();
	}
}
