using UnityEngine;
using Utilities.Grids;

namespace MatchThree.Model {
	public class CellModel  {
		private DropModel dropModel;

		public void SetDropModel(DropModel dropModel) => this.dropModel = dropModel;
		public bool IsEmpty() => dropModel is null;
		public DropModel GetDropModel() => dropModel;
	}
}
