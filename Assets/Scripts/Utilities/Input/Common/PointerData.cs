using UnityEngine;

namespace Utilities.Input.Common {
	public struct PointerData {
		public Vector2 PointerPosition { get; private set; }

		public PointerData(Vector2 pointerPosition) {
			this.PointerPosition = pointerPosition;
		}
	}
}
