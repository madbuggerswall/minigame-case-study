using UnityEngine;
using Utilities.Grids;

namespace MatchThree.Model {
	public class BoardModel {
		private CellModel[,] cellModels;

		public BoardModel(Vector2Int gridSize) {
			cellModels = new CellModel[gridSize.x, gridSize.y];

			for (int i = 0; i < cellModels.GetLength(0); i++)
				for (int j = 0; j < cellModels.GetLength(1); j++)
					cellModels[i, j] = new CellModel();
		}

		public CellModel[,] GetCellModels() => cellModels;
	}
}
