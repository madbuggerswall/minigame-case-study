using UnityEngine;

namespace MatchThree {
	[CreateAssetMenu(fileName = Filename, menuName = MenuName)]
	public class PuzzleLevelDefinition : ScriptableObject {
		private const string Filename = nameof(PuzzleLevelDefinition);
		private const string MenuName = "ScriptableObjects/Definitions/" + Filename;

		[SerializeField] private Vector2Int gridSize;
		[SerializeField] private int maxMoveCount;
		[SerializeField] private PuzzleElementTargetDTO[] elementTargets;
		[SerializeField] private ElementPlacementDTO[] elementPlacements;

		public Vector2Int GetGridSize() => gridSize;
		public int GetMaxMoveCount() => maxMoveCount;
		public PuzzleElementTargetDTO[] GetElementTargets() => elementTargets;
		public ElementPlacementDTO[] GetElementPlacements() => elementPlacements;
	}
}
