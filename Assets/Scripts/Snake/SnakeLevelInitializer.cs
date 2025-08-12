using UnityEngine;
using Utilities.Contexts;
using Utilities.Grids;

public class SnakeLevelInitializer : MonoBehaviour, IInitializable {
	[SerializeField] private Vector2Int gridSize;
	
	private SnakeGrid snakeGrid;
	
	public void Initialize() {
		
	}

	private void InitializeGrid() {
		SnakeGrid.GridParams gridParams = new SnakeGrid.GridParams() {
			GridSize = gridSize,
			GridPlane = GridPlane.XY
		};

		SnakeGrid.CellParams cellParams = new SnakeGrid.CellParams() {
			CellFactory = new SnakeCellFactory(),
			CellDiameter = 1f
		};
		
		this.snakeGrid = new SnakeGrid(gridParams, cellParams);
	}
}