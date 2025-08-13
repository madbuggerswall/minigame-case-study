using Utilities.Signals;

namespace Minigames.Loader.Signals {
	public class MinigameRestartedSignal : Signal {
		public MinigameDefinition MinigameDefinition { get; }

		public MinigameRestartedSignal(MinigameDefinition minigameDefinition) {
			MinigameDefinition = minigameDefinition;
		}
	}
}