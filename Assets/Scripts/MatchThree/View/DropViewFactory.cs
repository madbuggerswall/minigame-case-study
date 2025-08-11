using MatchThree.Model;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace MatchThree.View {
	public class DropViewFactory : MonoBehaviour, IInitializable {
		[SerializeField] private DropView dropViewPrefab;

		// Dependencies
		private ObjectPool objectPool;

		public void Initialize() {
			this.objectPool = SceneContext.GetInstance().Get<ObjectPool>();
		}

		public DropView CreateDropView(DropModel dropModel, Transform parent) {
			DropView dropView = objectPool.Spawn(dropViewPrefab, parent);
			dropView.Initialize(dropModel);
			return dropView;
		}
	}
}
