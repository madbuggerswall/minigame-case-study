using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

public class FoodFactory : MonoBehaviour, IInitializable {
	[SerializeField] private Transform root;
	[SerializeField] private Food foodPrefab;

	// Dependencies
	private ObjectPool objectPool;

	public void Initialize() {
		this.objectPool = SnakeContext.GetInstance().Get<ObjectPool>();
	}

	public Food CreateFood(Vector2Int position) {
		Food food = objectPool.Spawn(foodPrefab, root);
		food.transform.position = new Vector3(position.x, position.y, 0);
		return food;
	}
}
