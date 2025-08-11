using MatchThree.View;
using UnityEngine;
using Utilities.Contexts;

[DefaultExecutionOrder(-28)]
public class MatchThreeContext : SubContext<MatchThreeContext> {
	protected override void ResolveContext() {
		Resolve<MatchThreeInputManager>();
	}

	protected override void OnInitialized() { }
}
