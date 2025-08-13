using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace Snake.Factories {
	public class SnakeFactory : MonoBehaviour, IInitializable {
		[SerializeField] private Transform root;
		[SerializeField] private Elements.Snake snakePrefab;

		// Dependencies
		private ObjectPool objectPool;

		public void Initialize() {
			this.objectPool = SnakeContext.GetInstance().Get<ObjectPool>();
		}

		public Elements.Snake CreateSnake(Vector2Int position) {
			Elements.Snake snake = objectPool.Spawn(snakePrefab, root);
			snake.transform.position = new Vector3(position.x, position.y, 0);
			return snake;
		}
	}
}
