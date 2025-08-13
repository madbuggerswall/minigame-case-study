using RunnerGame.Elements;
using RunnerGame.Factories;
using UnityEngine;
using Utilities.Contexts;

namespace RunnerGame.Mechanics {
	public class RunnerLevelInitializer : MonoBehaviour, IInitializable {
		[SerializeField] private Vector2Int gridSize;

		private RunnerGrid runnerGrid;
		private Runner runner;

		// Dependencies
		private RunnerGridFactory runnerGridFactory;
		private RunnerFactory runnerFactory;
		private ObstacleFactory obstacleFactory;

		private CameraController cameraController;

		public void Initialize() {
			runnerGridFactory = RunnerContext.GetInstance().Get<RunnerGridFactory>();
			runnerFactory = RunnerContext.GetInstance().Get<RunnerFactory>();
			cameraController = SceneContext.GetInstance().Get<CameraController>();
			
			runnerGrid = runnerGridFactory.CreateRunnerGrid(Vector2Int.zero, gridSize);
			runner = runnerFactory.CreateRunner(Vector2Int.zero);

			cameraController.PlayCameraPositionTween(Vector3.zero);
			cameraController.PlayOrthoSizeTween(gridSize);
		}

		public Runner GetRunner() => runner;
		public RunnerGrid GetRunnerGrid() => runnerGrid;
	}
}
