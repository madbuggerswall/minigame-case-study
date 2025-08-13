using Snake.Factories;
using Utilities.Contexts;
using Utilities.Pooling;

namespace Snake {
	public class SnakeContext : SubContext<SnakeContext> {
		protected override void ResolveContext() {
			Resolve<ObjectPool>();
			Resolve<SnakeInputManager>();
			Resolve<SnakeSaveManager>();

			Resolve<FoodFactory>();
			Resolve<SnakeFactory>();
			Resolve<SnakeBodyFactory>();
			Resolve<SnakeGridFactory>();

			Resolve<FoodGenerator>();
			Resolve<SnakeLevelInitializer>();
			Resolve<SnakeLevelManager>();

			Resolve<ScoreManager>();
			Resolve<SnakeUIController>();
		}

		protected override void OnInitialized() { }
	}
}
