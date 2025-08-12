using UnityEngine;
using Utilities.Grids;

public class SnakeGrid : SquareGrid<SnakeCell> {
	public SnakeGrid(GridParams gridParams, CellParams cellParams) : base(gridParams, cellParams) { }
}
