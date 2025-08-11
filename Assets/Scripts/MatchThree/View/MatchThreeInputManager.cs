using Utilities.Contexts;
using Utilities.Input;
using Utilities.Input.Common;

namespace MatchThree.View {
	public class MatchThreeInputManager : IInitializable {
		private InputManager inputManager;

		public void Initialize() {
			this.inputManager = SceneContext.GetInstance().Get<InputManager>();

			InputHandler inputHandler = inputManager.CommonInputHandler;
			inputHandler.PressEvent += OnPress;
			inputHandler.DragEvent += OnDrag;
			inputHandler.ReleaseEvent += OnRelease;
		}

		private void OnPress(PointerData pressData) { }
		private void OnDrag(PointerData dragData) { }
		private void OnRelease(PointerData releaseData) { }
	}
}
