using Minigames;
using Minigames.Loader;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Input;
using Utilities.Pooling;
using Utilities.Signals;
using Utilities.Tweens;

[DefaultExecutionOrder(-32)]
public class MainContext : SceneContext {
	protected override void ResolveContext() {
		Resolve<SignalBus>();
		Resolve<TweenManager>();
		Resolve<ObjectPool>();
		Resolve<CameraController>();
		Resolve<InputManager>();

		Resolve<MinigameDefinitionManager>();
		Resolve<MinigameLoader>();
		
		Resolve<MainUIController>();
	}

	protected override void OnInitialized() {
		// throw new System.NotImplementedException();
	}
}
