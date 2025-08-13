using Utilities.Signals;

namespace Minigames.Loader.Signals {
	public class StartRestartingMinigameSignal : Signal {
		public MinigameDefinition MinigameDefinition { get; }

		public StartRestartingMinigameSignal(MinigameDefinition minigameDefinition) {
			MinigameDefinition = minigameDefinition;
		}
	}
}