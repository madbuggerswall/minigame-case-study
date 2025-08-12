using UnityEngine;
using Utilities.Grids;

public class SnakeCellFactory : CellFactory<SnakeCell> {
	public override SnakeCell Create(Vector3 cellPosition, float diameter) {
		return new SnakeCell(cellPosition, diameter);
	}
}
