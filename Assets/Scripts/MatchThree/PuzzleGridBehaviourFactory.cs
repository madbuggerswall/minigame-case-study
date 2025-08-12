using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace MatchThree {
	public class PuzzleGridBehaviourFactory : MonoBehaviour, IInitializable {
		[SerializeField] private PuzzleGridBehaviour puzzleGridBehaviourPrefab;
		[SerializeField] private Transform puzzleGridRoot;

		// Dependencies
		private ObjectPool objectPool;

		public void Initialize() {
			this.objectPool = PuzzleContext.GetInstance().Get<ObjectPool>();
		}

		public PuzzleGridBehaviour Create(PuzzleGrid puzzleGrid) {
			PuzzleGridBehaviour puzzleGridBehaviour = objectPool.Spawn(puzzleGridBehaviourPrefab, puzzleGridRoot);
			puzzleGridBehaviour.Initialize(puzzleGrid);
			return puzzleGridBehaviour;
		}
	}
}
