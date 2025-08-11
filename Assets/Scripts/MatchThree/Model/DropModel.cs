namespace MatchThree.Model {
	public enum DropColor { Blue, Green, Red, Yellow }

	public class DropModel {
		private DropColor dropColor;

		public DropModel(DropColor dropColor) {
			this.dropColor = dropColor;
		}

		public DropColor GetDropColor() => dropColor;
	}
}
