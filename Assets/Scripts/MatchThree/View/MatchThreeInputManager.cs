using UnityEngine;
using Utilities.Contexts;
using Utilities.Input;
using Utilities.Input.Common;

// This should be a vanilla class
namespace MatchThree.View {
	public class MatchThreeInputManager : MonoBehaviour, IInitializable {
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
