namespace MatchThree.PuzzleElements {
	public class ColorDrop : PuzzleElement {
		public ColorDrop(ColorDropDefinition definition) : base(definition) { }

		// public override void Explode(PuzzleGrid puzzleGrid) {
		// 	if (!puzzleGrid.TryGetPuzzleCell(this, out PuzzleCell puzzleCell))
		// 		return;
		//
		// 	puzzleCell.SetCellEmpty();
		// 	// SignalBus.GetInstance().Fire(new ElementExplodedSignal(this));
		//
		// 	InvokeAdjacentElements(puzzleGrid);
		// }
		//
		// public override void OnAdjacentExplode(PuzzleGrid puzzleGrid) { }
		//
		// public override void Fall(PuzzleGrid puzzleGrid) {
		// 	if (!puzzleGrid.TryGetPuzzleCell(this, out PuzzleCell currentCell))
		// 		return;
		//
		// 	if (!GetFallTarget(puzzleGrid, currentCell, out PuzzleCell targetCell))
		// 		return;
		//
		// 	currentCell.SetCellEmpty();
		// 	targetCell.SetPuzzleElement(this);
		// }
		//
		// // Helper methods
		// private void InvokeAdjacentElements(PuzzleGrid puzzleGrid) {
		// 	if (!puzzleGrid.TryGetPuzzleCell(this, out PuzzleCell puzzleCell))
		// 		return;
		//
		// 	PuzzleCell[] neighbors = puzzleGrid.GetNeighbors(puzzleCell);
		// 	foreach (PuzzleCell cell in neighbors)
		// 		if (cell.TryGetPuzzleElement(out PuzzleElement neighborElement))
		// 			neighborElement.OnAdjacentExplode(puzzleGrid);
		// }
	}
}
