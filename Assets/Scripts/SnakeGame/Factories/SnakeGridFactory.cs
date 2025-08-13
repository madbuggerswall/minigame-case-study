using SnakeGame.Elements;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace SnakeGame.Factories {
	public class SnakeGridFactory : MonoBehaviour, IInitializable {
		[SerializeField] private Transform root;
		[SerializeField] private SnakeGrid snakeGridPrefab;

		// Dependencies
		private ObjectPool objectPool;

		public void Initialize() {
			this.objectPool = SnakeContext.GetInstance().Get<ObjectPool>();
		}

		public SnakeGrid CreateSnakeGrid(Vector2Int gridPosition, Vector2Int gridSize) {
			SnakeGrid snakeGrid = objectPool.Spawn(snakeGridPrefab, root);
			snakeGrid.Initialize(gridPosition, gridSize);
			return snakeGrid;
		}
	}
}
