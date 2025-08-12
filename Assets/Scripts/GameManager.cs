using UnityEngine;

public static class GameManager {
	// Runtime methods
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
	private	static void OnBeforeSplashScreen() {
		Debug.Log("Before SplashScreen is shown and before the first scene is loaded.");
		// Setup
		Application.targetFrameRate = 60;
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void OnBeforeSceneLoad() {
		Debug.Log("First scene loading: Before Awake is called.");
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	private static void OnAfterSceneLoad() {
		Debug.Log("First scene loaded: After Awake is called.");
	}

	[RuntimeInitializeOnLoadMethod]
	private static void OnRuntimeInitialized() {
		Debug.Log("Runtime initialized: First scene loaded: After Awake is called.");
	}

	private static void SetupResolution() {
		const int defaultWidth = 600;
		// const int defaultHeight = 270;

		float ratio = (float) defaultWidth / Screen.width;
		int height = Mathf.RoundToInt(Screen.height * ratio) / 2 * 2;
		int scale = Mathf.FloorToInt((float) Screen.width / defaultWidth);
		Screen.SetResolution(defaultWidth * scale, height * scale, FullScreenMode.FullScreenWindow);

		LogDisplayInfo(height, scale);
	}

	private static void LogDisplayInfo(int height, int scale) {
		string displayInfo = "Resolution & DPI: " + Screen.currentResolution + " | " + Screen.dpi;
		displayInfo += "\nHeight: " + height;
		displayInfo += "\nScale: " + scale;
		Debug.Log(displayInfo);
	}
}