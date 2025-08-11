using MatchThree.Presenter;
using MatchThree.View;
using UnityEngine;
using Utilities.Contexts;

[DefaultExecutionOrder(-28)]
public class MatchThreeContext : SubContext<MatchThreeContext> {
	protected override void ResolveContext() {
		Resolve<MatchThreeInputManager>();

		Resolve<BoardViewFactory>();
		Resolve<DropViewFactory>();
		
		Resolve<MainPresenter>();
		Resolve<BoardPresenter>();
	}

	protected override void OnInitialized() { }
}

// TODO Each context should have its own object pool to handled undesired remains on the main pool.
