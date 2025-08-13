using RunnerGame.Factories;
using RunnerGame.Input;
using RunnerGame.Mechanics;
using RunnerGame.Persistence;
using RunnerGame.UI;
using Utilities.Contexts;
using Utilities.Pooling;

namespace RunnerGame {
	public class RunnerContext : SubContext<RunnerContext> {
		protected override void ResolveContext() {
			Resolve<ObjectPool>();
			Resolve<RunnerInputManager>();
			Resolve<RunnerSaveManager>();

			Resolve<ObstacleFactory>();
			Resolve<RunnerFactory>();
			Resolve<RunnerGridFactory>();

			Resolve<ObstacleGenerator>();
			Resolve<RunnerLevelInitializer>();
			Resolve<RunnerLevelManager>();

			Resolve<RunnerScoreManager>();
			Resolve<RunnerUIController>();
		}

		protected override void OnInitialized() { }
	}
}
