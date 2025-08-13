using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Grids.SpatialHelpers.Hexagonal {
	// NOTE Also do this for SquareCells
	// IDEA Rename to AxialCellMapper<T> or AxialGridHelper<T>
	// https://www.redblobgames.com/grids/hexagons/
	public class AxialCoordinateHash<T> where T : CircleCell {
		private readonly Dictionary<AxialCoord, T> cellsByAxialCoord;
		private readonly Dictionary<T, AxialCoord> axialCoordsByCell;

		private readonly float cellDiameter;
		private readonly T[] cells;

		public AxialCoordinateHash(CircleGrid<T> grid) {
			cellDiameter = grid.GetCellDiameter();
			cells = grid.GetCells();
			
			cellsByAxialCoord = MapCellsByAxialCoord();
			axialCoordsByCell = MapAxialCoordsByCell(cellsByAxialCoord);
		}

		private Dictionary<AxialCoord, T> MapCellsByAxialCoord() {
			Dictionary<AxialCoord, T> cellsByAxialCoordinates = new();

			foreach (T cell in cells) {
				AxialCoord axial = AxialCoord.WorldToAxial(cell.GetWorldPosition().GetXY(), cellDiameter);
				if (!cellsByAxialCoordinates.TryAdd(axial, cell))
					Debug.Log("Oh no");
			}

			return cellsByAxialCoordinates;
		}

		private Dictionary<T, AxialCoord> MapAxialCoordsByCell(Dictionary<AxialCoord, T> map) {
			Dictionary<T, AxialCoord> invertedAxialMap = new();
			foreach ((AxialCoord axialCoordinate, T cell) in map)
				invertedAxialMap.TryAdd(cell, axialCoordinate);

			return invertedAxialMap;
		}

		public bool TryGetCell(Vector3 worldPosition, out T circleCell) {
			AxialCoord centerAxial = AxialCoord.WorldToAxial(worldPosition, cellDiameter);
			return cellsByAxialCoord.TryGetValue(centerAxial, out circleCell);
		}

		public AxialCoord GetAxialCoordinates(T cell) {
			return axialCoordsByCell.GetValueOrDefault(cell);
		}

		public T GetCell(AxialCoord axialCoord) {
			return cellsByAxialCoord.GetValueOrDefault(axialCoord);
		}
	}
}
