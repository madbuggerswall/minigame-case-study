using Utilities.Signals;

namespace Minigames.Loader.Signals {
	public class StartUnloadingMinigameSignal : Signal {
		public MinigameDefinition MinigameDefinition { get; }

		public StartUnloadingMinigameSignal(MinigameDefinition minigameDefinition) {
			MinigameDefinition = minigameDefinition;
		}
	}
}