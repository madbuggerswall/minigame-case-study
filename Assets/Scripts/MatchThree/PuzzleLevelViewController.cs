using System.Collections.Generic;
using MatchThree.PuzzleElements;
using MatchThree.ViewHelpers;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Pooling;

namespace MatchThree {
	public class PuzzleLevelViewController : IInitializable {
		private readonly Dictionary<PuzzleElement, PuzzleElementBehaviour> elementBehaviours = new();
		private readonly Dictionary<PuzzleGrid, PuzzleGridBehaviour> gridBehaviours = new();
		private readonly Dictionary<PuzzleCell, PuzzleCellBehaviour> cellBehaviours = new();

		// Dependencies
		private PuzzleElementBehaviourFactory elementBehaviourFactory;
		private PuzzleGridBehaviourFactory gridBehaviourFactory;
		private PuzzleCellBehaviourFactory cellBehaviourFactory;
		private PuzzleLevelManager levelManager;
		private ObjectPool objectPool;

		public FallViewHelper FallViewHelper { get; private set; }
		public FillViewHelper FillViewHelper { get; private set; }

		public ViewReadyNotifier ViewReadyNotifier { get; private set; }

		public void Initialize() {
			this.elementBehaviourFactory = PuzzleContext.GetInstance().Get<PuzzleElementBehaviourFactory>();
			this.gridBehaviourFactory = PuzzleContext.GetInstance().Get<PuzzleGridBehaviourFactory>();
			this.cellBehaviourFactory = PuzzleContext.GetInstance().Get<PuzzleCellBehaviourFactory>();
			this.levelManager = PuzzleContext.GetInstance().Get<PuzzleLevelManager>();
			this.objectPool = PuzzleContext.GetInstance().Get<ObjectPool>();

			PuzzleGrid puzzleGrid = levelManager.GetPuzzleGrid();
			SpawnGridBehaviour(puzzleGrid);
			SpawnCellBehaviours(puzzleGrid);
			SpawnElementBehaviours(puzzleGrid);

			FallViewHelper = new FallViewHelper(this, puzzleGrid);
			FillViewHelper = new FillViewHelper(this, puzzleGrid);

			ViewReadyNotifier = new ViewReadyNotifier();
		}

		// Initializer methods
		private void SpawnGridBehaviour(PuzzleGrid puzzleGrid) {
			PuzzleGridBehaviour gridBehaviour = gridBehaviourFactory.Create(puzzleGrid);
			gridBehaviours.Add(puzzleGrid, gridBehaviour);
		}

		private void SpawnCellBehaviours(PuzzleGrid puzzleGrid) {
			if (!gridBehaviours.TryGetValue(puzzleGrid, out PuzzleGridBehaviour gridBehaviour))
				return;

			Transform cellsParent = gridBehaviour.GetCellsParent();
			PuzzleCell[,] puzzleCells = puzzleGrid.GetCells();

			for (int y = 0; y < puzzleCells.GetLength(1); y++) {
				for (int x = 0; x < puzzleCells.GetLength(0); x++) {
					PuzzleCell cell = puzzleCells[x, y];
					PuzzleCellBehaviour cellBehaviour = cellBehaviourFactory.Create(cell, cellsParent);
					cellBehaviours.Add(cell, cellBehaviour);
				}
			}
		}

		private void SpawnElementBehaviours(PuzzleGrid puzzleGrid) {
			PuzzleCell[,] puzzleCells = puzzleGrid.GetCells();
			for (int y = 0; y < puzzleCells.GetLength(1); y++) {
				for (int x = 0; x < puzzleCells.GetLength(0); x++) {
					PuzzleCell cell = puzzleCells[x, y];
					if (cell.IsEmpty())
						continue;

					ColorDrop colorDrop = cell.GetColorDrop();
					PuzzleElementBehaviour elementBehaviour = elementBehaviourFactory.Create(colorDrop, cell);
					elementBehaviour.SetSortingOrder(y);
					elementBehaviours.Add(colorDrop, elementBehaviour);
				}
			}
		}

		public PuzzleElementBehaviour SpawnElementBehaviour(ColorDrop colorDrop, PuzzleCell puzzleCell) {
			PuzzleGrid puzzleGrid = levelManager.GetPuzzleGrid();
			// TODO int cellIndex = puzzleGrid.GetCellIndex(puzzleCell);

			PuzzleElementBehaviour elementBehaviour = elementBehaviourFactory.Create(colorDrop, puzzleCell);
			// TODO elementBehaviour.SetSortingOrder(cellIndex);
			elementBehaviours.Add(colorDrop, elementBehaviour);

			return elementBehaviour;
		}

		public void DespawnElementBehaviour(PuzzleElement puzzleElement) {
			PuzzleElementBehaviour elementBehaviour = GetPuzzleElementBehaviour(puzzleElement);
			objectPool.Despawn(elementBehaviour);
			elementBehaviours.Remove(puzzleElement);
		}


		// Getters
		public PuzzleGridBehaviour GetPuzzleGridBehaviour(PuzzleGrid puzzleGrid) {
			return gridBehaviours.GetValueOrDefault(puzzleGrid);
		}

		public PuzzleCellBehaviour GetPuzzleCellBehaviour(PuzzleCell puzzleCell) {
			return cellBehaviours.GetValueOrDefault(puzzleCell);
		}

		public PuzzleElementBehaviour GetPuzzleElementBehaviour(PuzzleElement element) {
			return elementBehaviours.GetValueOrDefault(element);
		}
	}
}
