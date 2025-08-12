using UnityEngine;
using Utilities.Contexts;

public class SnakeContext : SubContext<SnakeContext> {
	protected override void ResolveContext() {
		Resolve<SnakeInputManager>();
	}

	protected override void OnInitialized() { }
}
