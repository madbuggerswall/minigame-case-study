using UnityEngine.InputSystem.Controls;

namespace Utilities.Input.Standalone {
	public struct KeyData {
		public KeyControl KeyControl { get; }

		public KeyData(KeyControl keyControl) {
			KeyControl = keyControl;
		}
	}
}
