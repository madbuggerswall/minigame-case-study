using RunnerGame.Elements;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace RunnerGame.Factories {
	public class RunnerGridFactory : MonoBehaviour, IInitializable {
		[SerializeField] private Transform root;
		[SerializeField] private RunnerGrid runnerGridPrefab;

		// Dependencies
		private ObjectPool objectPool;

		public void Initialize() {
			objectPool = RunnerContext.GetInstance().Get<ObjectPool>();
		}

		public RunnerGrid CreateRunnerGrid(Vector2Int gridPosition, Vector2Int gridSize) {
			RunnerGrid runnerGrid = objectPool.Spawn(runnerGridPrefab, root);
			runnerGrid.Initialize(gridPosition, gridSize);
			return runnerGrid;
		}
	}
}
