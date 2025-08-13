using Utilities.Signals;

namespace Minigames.Loader.Signals {
	public class StartLoadingMinigameSignal : Signal {
		public MinigameDefinition MinigameDefinition { get; }

		public StartLoadingMinigameSignal(MinigameDefinition minigameDefinition) {
			MinigameDefinition = minigameDefinition;
		}
	}
}