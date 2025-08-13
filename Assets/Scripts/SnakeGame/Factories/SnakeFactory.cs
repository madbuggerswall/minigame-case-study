using SnakeGame.Elements;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace SnakeGame.Factories {
	public class SnakeFactory : MonoBehaviour, IInitializable {
		[SerializeField] private Transform root;
		[SerializeField] private Snake snakePrefab;

		// Dependencies
		private ObjectPool objectPool;

		public void Initialize() {
			objectPool = SnakeContext.GetInstance().Get<ObjectPool>();
		}

		public Snake CreateSnake(Vector2Int position) {
			Snake snake = objectPool.Spawn(snakePrefab, root);
			snake.transform.position = new Vector3(position.x, position.y, 0);
			return snake;
		}
	}
}
