using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.UI {
	public class LevelEndPanel : MonoBehaviour {
		[SerializeField] private Button restartButton;

		private void Awake() {
			restartButton.onClick.AddListener(OnRestartButtonClick);
		}

		private void OnRestartButtonClick() {
			Scene activeScene = SceneManager.GetActiveScene();

			AsyncOperation unloadAsync = SceneManager.UnloadSceneAsync(1);
			if (unloadAsync != null)
				unloadAsync.completed += LoadSceneAsync;
		}

		private void LoadSceneAsync(AsyncOperation obj) {
			AsyncOperation loadAsync = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
			loadAsync.completed += delegate { UnityEngine.Debug.Log("Loaded"); };
		}
	}
}
