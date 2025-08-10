using UnityEngine;

namespace Utilities.Grids {
	public abstract class CellFactory<T> where T : Cell {
		public abstract T Create(Vector3 cellPosition, float diameter);
	}
}
