using SnakeGame.Elements;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace SnakeGame.Factories {
	public class SnakeBodyFactory : MonoBehaviour, IInitializable {
		[SerializeField] private Transform root;
		[SerializeField] private SnakeBody snakeBodyPrefab;

		// Dependencies
		private ObjectPool objectPool;

		public void Initialize() {
			this.objectPool = SnakeContext.GetInstance().Get<ObjectPool>();
		}

		public SnakeBody CreateSnake(Vector2Int position) {
			SnakeBody snakeBody = objectPool.Spawn(snakeBodyPrefab, root);
			snakeBody.transform.position = new Vector3(position.x, position.y, 0);
			return snakeBody;
		}
	}
}
