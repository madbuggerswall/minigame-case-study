using UnityEngine;

namespace MatchThree.PuzzleElements {
	public abstract class PuzzleElementDefinition : ScriptableObject {
		[SerializeField] private PuzzleElementBehaviour prefab;

		public abstract PuzzleElement CreateElement();
		public abstract Sprite GetSprite();

		public PuzzleElementBehaviour GetPrefab() => prefab;
	}
}
