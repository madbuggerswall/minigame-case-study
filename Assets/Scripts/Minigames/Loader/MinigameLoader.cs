using Minigames.Loader.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.Contexts;
using Utilities.Signals;

namespace Minigames.Loader {
	public class MinigameLoader : IInitializable {
		private MinigameDefinition activeMinigameDefinition;

		// Dependencies
		private SignalBus signalBus;

		public void Initialize() {
			signalBus = SceneContext.GetInstance().Get<SignalBus>();
		}

		public void LoadMinigame(MinigameDefinition minigameDefinition) {
			signalBus.Fire(new StartLoadingMinigameSignal(minigameDefinition));
			LoadSceneAsync(minigameDefinition);
		}

		public void UnloadMinigame(MinigameDefinition minigameDefinition) {
			signalBus.Fire(new StartUnloadingMinigameSignal(minigameDefinition));
			UnloadSceneAsync(minigameDefinition);
		}

		public void RestartMinigame(MinigameDefinition minigameDefinition) {
			signalBus.Fire(new StartRestartingMinigameSignal(minigameDefinition));
			RestartSceneAsync(minigameDefinition);
		}

		private void LoadSceneAsync(MinigameDefinition minigameDefinition) {
			int sceneIndex = minigameDefinition.GetSceneBuildIndex();

			AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
			if (loadAsync is null)
				return;

			loadAsync.completed += delegate {
				activeMinigameDefinition = minigameDefinition;
				signalBus.Fire(new MinigameLoadedSignal(minigameDefinition));
			};
		}

		private void UnloadSceneAsync(MinigameDefinition minigameDefinition) {
			int sceneIndex = minigameDefinition.GetSceneBuildIndex();

			AsyncOperation unloadAsync = SceneManager.UnloadSceneAsync(sceneIndex);
			if (unloadAsync is null)
				return;

			unloadAsync.completed += delegate {
				activeMinigameDefinition = null;
				signalBus.Fire(new MinigameUnloadedSignal(minigameDefinition));
			};
		}

		private void RestartSceneAsync(MinigameDefinition minigameDefinition) {
			int sceneIndex = minigameDefinition.GetSceneBuildIndex();

			AsyncOperation unloadAsync = SceneManager.UnloadSceneAsync(sceneIndex);
			if (unloadAsync is null)
				return;

			unloadAsync.completed += delegate {
				AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
				if (loadAsync is null)
					return;

				loadAsync.completed += delegate { signalBus.Fire(new MinigameRestartedSignal(minigameDefinition)); };
			};
		}

		public MinigameDefinition GetActiveMinigameDefinition() => activeMinigameDefinition;
	}
}