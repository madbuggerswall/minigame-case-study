using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace MatchThree.View {
	public class CellViewFactory : MonoBehaviour, IInitializable {
		[SerializeField] private CellView cellViewPrefab;

		// Dependencies
		private ObjectPool objectPool;

		public void Initialize() {
			this.objectPool = SceneContext.GetInstance().Get<ObjectPool>();
		}

		public CellView CreateCellView(Vector2Int cellIndex, Transform parent) {
			CellView dropView = objectPool.Spawn(cellViewPrefab, parent);
			dropView.Initialize(cellIndex);
			return dropView;
		}
	}
}
