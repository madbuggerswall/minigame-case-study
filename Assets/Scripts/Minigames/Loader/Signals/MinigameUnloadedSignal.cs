using Utilities.Signals;

namespace Minigames.Loader.Signals {
	public class MinigameUnloadedSignal : Signal {
		public MinigameDefinition MinigameDefinition { get; }

		public MinigameUnloadedSignal(MinigameDefinition minigameDefinition) {
			MinigameDefinition = minigameDefinition;
		}
	}
}