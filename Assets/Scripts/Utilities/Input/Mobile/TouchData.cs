using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Utilities.Input.Mobile {
	public struct TouchData {
		public Touch Touch { get; private set; }
		public Vector2 PressPosition { get; private set; }

		public TouchData(Touch touch, Vector2 pressPosition) {
			Touch = touch;
			PressPosition = pressPosition;
		}
	}
}