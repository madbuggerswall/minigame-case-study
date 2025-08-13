using RunnerGame.Elements;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace RunnerGame.Factories {
	public class RunnerFactory : MonoBehaviour, IInitializable {
		[SerializeField] private Transform root;
		[SerializeField] private Runner runnerPrefab;

		// Dependencies
		private ObjectPool objectPool;

		public void Initialize() {
			objectPool = RunnerContext.GetInstance().Get<ObjectPool>();
		}

		public Runner CreateRunner(Vector2Int position) {
			Runner runner = objectPool.Spawn(runnerPrefab, root);
			runner.transform.position = new Vector3(position.x, position.y, 0);
			return runner;
		}
	}
}
