using MatchThree.Model;
using UnityEngine;

namespace MatchThree.PuzzleElements {
	public abstract class PuzzleElementBehaviour : MonoBehaviour {
		public abstract void Initialize(PuzzleElementDefinition definition, PuzzleCell puzzleCell);

		public abstract void SetSortingOrder(int sortingOrder);
	}
}
