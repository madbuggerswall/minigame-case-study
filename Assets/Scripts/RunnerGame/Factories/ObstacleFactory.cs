using RunnerGame.Elements;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace RunnerGame.Factories {
	public class ObstacleFactory : MonoBehaviour, IInitializable {
		[SerializeField] private Transform root;
		[SerializeField] private Obstacle obstaclePrefab;

		// Dependencies
		private ObjectPool objectPool;

		public void Initialize() {
			objectPool = RunnerContext.GetInstance().Get<ObjectPool>();
		}

		public Obstacle CreateObstacle(Vector2Int gridPosition) {
			Obstacle obstacle = objectPool.Spawn(obstaclePrefab, root);
			obstacle.Initialize(gridPosition);
			return obstacle;
		}
	}
}
