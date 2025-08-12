using MatchThree.Model;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace MatchThree.PuzzleElements {
	public class PuzzleElementBehaviourFactory : MonoBehaviour, IInitializable {
		[SerializeField] private Transform elementsParent;

		// Dependencies
		private ObjectPool objectPool;

		public void Initialize() {
			this.objectPool = SceneContext.GetInstance().Get<ObjectPool>();
		}

		public PuzzleElementBehaviour Create(ColorDrop colorDrop, PuzzleCell puzzleCell) {
			PuzzleElementDefinition elementDefinition = colorDrop.GetDefinition();
			PuzzleElementBehaviour elementBehaviour = objectPool.Spawn(elementDefinition.GetPrefab(), elementsParent);
			elementBehaviour.Initialize(elementDefinition, puzzleCell);

			puzzleCell.SetColorDrop(colorDrop);
			return elementBehaviour;
		}
	}
}
