using MatchThree.Model;
using MatchThree.PuzzleElements;
using MatchThree.Targets;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Grids;

namespace MatchThree {
	public class PuzzleLevelInitializer : MonoBehaviour, IInitializable {
		[SerializeField] private PuzzleLevelDefinition levelDefinition;

		// Dependencies
		private ColorDropDefinitionManager colorDropDefinitionManager;
		private CameraController cameraController;

		// Fields
		private PuzzleGrid puzzleGrid;
		private PuzzleElementTarget[] elementTargets;
		private int maxMoveCount;

		public void Initialize() {
			colorDropDefinitionManager = SceneContext.GetInstance().Get<ColorDropDefinitionManager>();
			cameraController = SceneContext.GetInstance().Get<CameraController>();

			// Puzzle Grid
			InitializeGrid();
			InitializeElements();
			InitializeElementTargets();
			InitializeMaxMoveCount();

			// Camera Controller
			cameraController.PlayCameraPositionTween(puzzleGrid.GetCenterPoint());
			cameraController.PlayOrthoSizeTween(puzzleGrid.GetGridSizeInLength());
		}


		private void InitializeGrid() {
			const float cellDiameter = 1f;

			Vector2Int gridSize = levelDefinition.GetGridSize();
			PuzzleCellFactory cellFactory = new PuzzleCellFactory();

			PuzzleGrid.GridParams gridParams = new() {
				GridSize = gridSize,
				GridPlane = GridPlane.XY
			};

			PuzzleGrid.CellParams cellParams = new() {
				CellFactory = cellFactory,
				CellDiameter = cellDiameter
			};

			puzzleGrid = new PuzzleGrid(gridParams, cellParams);
		}

		private void InitializeElements() {
			PuzzleCell[,] puzzleCells = puzzleGrid.GetCells();

			for (int y = 0; y < puzzleCells.GetLength(0); y++)
				for (int x = 0; x < puzzleCells.GetLength(1); x++) {
					PuzzleCell puzzleCell = puzzleCells[x, y];
					ColorDrop colorDrop = CreateRandomColorChip();
					puzzleCell.SetColorDrop(colorDrop);
				}
		}

		private void InitializeElementTargets() {
			PuzzleElementTargetDTO[] elementTargetDTOs = levelDefinition.GetElementTargets();
			elementTargets = new PuzzleElementTarget[elementTargetDTOs.Length];

			for (int i = 0; i < elementTargetDTOs.Length; i++)
				elementTargets[i] = new PuzzleElementTarget(elementTargetDTOs[i]);
		}

		private void InitializeMaxMoveCount() {
			maxMoveCount = levelDefinition.GetMaxMoveCount();
		}

		private ColorDrop CreateRandomColorChip() {
			ColorDropDefinition colorDropDefinition = colorDropDefinitionManager.GetRandomColorChipDefinition();
			ColorDrop colorDrop = new ColorDrop(colorDropDefinition);

			return colorDrop;
		}

		public PuzzleGrid GetPuzzleGrid() => puzzleGrid;
		public PuzzleElementTarget[] GetElementTargets() => elementTargets;
		public int GetMaxMoveCount() => maxMoveCount;
	}
}
