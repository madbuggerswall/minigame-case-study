using UnityEngine;

namespace MatchThree.PuzzleElements {
	[CreateAssetMenu(fileName = Filename, menuName = MenuName)]
	public class ColorDropDefinition : PuzzleElementDefinition {
		private const string Filename = nameof(ColorDropDefinition);
		private const string MenuName = "ScriptableObjects/Definitions/" + Filename;

		[SerializeField] private Sprite sprite;

		public override Sprite GetSprite() => sprite;
		public override PuzzleElement CreateElement() => new ColorDrop(this);
	}
}
