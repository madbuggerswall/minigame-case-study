using Utilities.Signals;

namespace Minigames.Loader.Signals {
	public class MinigameLoadedSignal : Signal {
		public MinigameDefinition MinigameDefinition { get; }

		public MinigameLoadedSignal(MinigameDefinition minigameDefinition) {
			MinigameDefinition = minigameDefinition;
		}
	}
}