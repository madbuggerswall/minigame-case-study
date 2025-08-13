using Snake.Factories;
using Utilities.Contexts;
using Utilities.Pooling;

namespace Snake {
	public class SnakeContext : SubContext<SnakeContext> {
		protected override void ResolveContext() {
			Resolve<ObjectPool>();
			Resolve<SnakeInputManager>();

			Resolve<FoodFactory>();
			Resolve<SnakeFactory>();
			Resolve<SnakeBodyFactory>();
			Resolve<SnakeGridFactory>();

			Resolve<FoodGenerator>();
			Resolve<SnakeLevelInitializer>();
			Resolve<SnakeLevelManager>();
		
			Resolve<SnakeUIController>();
			Resolve<ScoreManager>();
		}

		protected override void OnInitialized() { }
	}
}
