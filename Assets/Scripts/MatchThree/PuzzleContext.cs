using MatchThree.PuzzleElements;
using MatchThree.Targets;
using MatchThree.Turns;
using MatchThree.UI;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;
using Utilities.Signals;

namespace MatchThree {
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
			SceneContext.GetInstance().Get<SignalBus>().Fire(new PuzzleContextInitializedSignal());
		}
	}
	
	public class PuzzleContextInitializedSignal : Signal {}
}
