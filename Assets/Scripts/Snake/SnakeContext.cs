using Utilities.Contexts;
using Utilities.Pooling;

public class SnakeContext : SubContext<SnakeContext> {
	protected override void ResolveContext() {
		Resolve<ObjectPool>();
		Resolve<SnakeInputManager>();

		Resolve<FoodFactory>();
		Resolve<SnakeFactory>();
		Resolve<SnakeBodyFactory>();
		Resolve<SnakeGridFactory>();

		Resolve<SnakeLevelInitializer>();
		
		Resolve<SnakeUIController>();
	}

	protected override void OnInitialized() { }
}
