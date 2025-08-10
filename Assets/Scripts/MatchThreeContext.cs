using MatchThree.View;
using Utilities.Contexts;

public class MatchThreeContext : SubContext<MatchThreeContext> {
	protected override void ResolveContext() {
		Resolve<MatchThreeInputManager>();
	}

	protected override void OnInitialized() { }
}
