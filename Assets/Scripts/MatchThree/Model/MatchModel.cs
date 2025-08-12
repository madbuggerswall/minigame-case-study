using System.Collections.Generic;
using UnityEngine;
using Utilities.Collections.Generic;

// TODO Match validation ignores 2x2 matches
namespace MatchThree.Model {
	public class MatchModel {
		private const int MinMatchLength = 3;

		private readonly UniqueList<Vector2Int> cellIndices = new();
		private Dictionary<int, List<Vector2Int>> columns = new(); // Share same x
		private Dictionary<int, List<Vector2Int>> rows = new();    // Share same y

		private int longestColumnIndex;
		private int longestRowIndex;

		public MatchModel() { }

		public void AddCellIndex(Vector2Int cellIndex) {
			// cellIndices.Add(cellIndex);

			if (columns.TryAdd(cellIndex.x, new List<Vector2Int> { cellIndex })) {
				longestColumnIndex = cellIndex.x;
			} else {
				columns[cellIndex.x].Add(cellIndex);
			}

			if (rows.TryAdd(cellIndex.y, new List<Vector2Int> { cellIndex })) {
				longestRowIndex = cellIndex.y;
			} else {
				rows[cellIndex.y].Add(cellIndex);
			}

			if (columns[cellIndex.x].Count > columns[longestColumnIndex].Count)
				longestColumnIndex = cellIndex.x;

			if (rows[cellIndex.y].Count > rows[longestRowIndex].Count)
				longestRowIndex = cellIndex.y;
		}

		public void EvaluateAxes() {
			List<Vector2Int> longestColumn = columns[longestColumnIndex];
			List<Vector2Int> longestRow = rows[longestRowIndex];

			if (longestColumn.Count >= 3)
				for (int i = 0; i < longestColumn.Count; i++)
					cellIndices.Add(longestColumn[i]);

			if (longestRow.Count >= 3)
				for (int i = 0; i < longestRow.Count; i++)
					cellIndices.Add(longestRow[i]);
		}

		public bool IsValid() {
			return cellIndices.Count >= 3;
		}

		public UniqueList<Vector2Int> GetCellIndices() => cellIndices;
	}
}
