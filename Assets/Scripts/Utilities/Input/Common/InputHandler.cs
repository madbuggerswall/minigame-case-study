using System;
using UnityEngine;

namespace Utilities.Input.Common {
	public abstract class InputHandler {
		public Vector2 PointerPosition { get; protected set; }

		public Action<PointerData> PressEvent { get; set; } = delegate { };
		public Action<PointerData> DragEvent { get; set; } = delegate { };
		public Action<PointerData> ReleaseEvent { get; set; } = delegate { };

		public abstract void HandleInput();
	}
}
