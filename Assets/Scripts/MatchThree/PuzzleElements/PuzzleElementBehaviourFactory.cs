using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace MatchThree.PuzzleElements {
	public class PuzzleElementBehaviourFactory : MonoBehaviour, IInitializable {
		[SerializeField] private Transform elementsParent;

		// Dependencies
		private ObjectPool objectPool;

		public void Initialize() {
			this.objectPool = PuzzleContext.GetInstance().Get<ObjectPool>();
		}

		public PuzzleElementBehaviour Create(PuzzleElement puzzleElement, PuzzleCell puzzleCell) {
			PuzzleElementDefinition elementDefinition = puzzleElement.GetDefinition();
			PuzzleElementBehaviour elementBehaviour = objectPool.Spawn(elementDefinition.GetPrefab(), elementsParent);
			elementBehaviour.Initialize(elementDefinition, puzzleCell);

			puzzleCell.SetPuzzleElement(puzzleElement);
			return elementBehaviour;
		}
	}
}
