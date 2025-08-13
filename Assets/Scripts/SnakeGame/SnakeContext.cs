using SnakeGame.Elements;
using SnakeGame.Factories;
using SnakeGame.Input;
using SnakeGame.Level;
using SnakeGame.Persistence;
using SnakeGame.UI;
using Utilities.Contexts;
using Utilities.Pooling;

namespace SnakeGame {
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

			Resolve<SnakeScoreManager>();
			Resolve<SnakeUIController>();
		}

		protected override void OnInitialized() { }
	}
}
