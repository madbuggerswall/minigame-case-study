using Utilities.Contexts;
using Utilities.Signals;

public class MatchThreeContext : SubContext<MatchThreeContext> {
	protected override void ResolveContext() {
		Resolve<MatchThreeInputManager>();
	}

	protected override void OnInitialized() {
		SceneContext.GetInstance().Get<SignalBus>().Fire(new MatchThreeContextInitializedSignal());
	}
}

public class MatchThreeContextInitializedSignal : Signal { }
