using UnityEngine.InputSystem.Controls;

namespace Utilities.Input.Standalone {
	public struct MouseData {
		public ButtonControl ButtonControl { get; private set; }

		public MouseData(ButtonControl buttonControl) {
			this.ButtonControl = buttonControl;
		}
	}
}
