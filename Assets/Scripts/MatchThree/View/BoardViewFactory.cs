using MatchThree.Model;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace MatchThree.View {
	public class BoardViewFactory : MonoBehaviour, IInitializable {
		[SerializeField] private BoardView boardViewPrefab;

		// Dependencies
		private ObjectPool objectPool;

		public void Initialize() {
			this.objectPool = SceneContext.GetInstance().Get<ObjectPool>();
		}

		public BoardView CreateBoardView(BoardModel boardModel, Transform parent) {
			BoardView boardView = objectPool.Spawn(boardViewPrefab, parent);
			boardView.Initialize(boardModel);
			return boardView;
		}
	}
}
