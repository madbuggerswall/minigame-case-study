using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Utilities.Contexts;
using Utilities.Input;
using Utilities.Input.Standalone;

public class SnakeInputManager : IInitializable {
	public Action UpKeyPressEvent { get; set; } = delegate { };
	public Action DownKeyPressEvent { get; set; } = delegate { };
	public Action LeftKeyPressEvent { get; set; } = delegate { };
	public Action RightKeyPressEvent { get; set; } = delegate { };

	public Action OneKeyPressEvent { get; set; } = delegate { };
	public Action OneKeyReleaseEvent { get; set; } = delegate { };

	// Dependencies
	private InputManager inputManager;

	public void Initialize() {
		this.inputManager = SceneContext.GetInstance().Get<InputManager>();

		// Subscribe to InputHandler events
		StandaloneInputHandler inputHandler = inputManager.StandaloneInputHandler;
		inputHandler.KeyPressEvent += OnKeyPressed;
		inputHandler.KeyReleaseEvent += OnKeyReleased;
	}

	private void OnKeyPressed(KeyData keyData) {
		KeyControl keyControl = keyData.KeyControl;

		if (keyControl == Keyboard.current.upArrowKey || keyControl == Keyboard.current.wKey)
			UpKeyPressEvent.Invoke();
		else if (keyControl == Keyboard.current.downArrowKey || keyControl == Keyboard.current.sKey)
			DownKeyPressEvent.Invoke();
		else if (keyControl == Keyboard.current.leftArrowKey || keyControl == Keyboard.current.aKey)
			LeftKeyPressEvent.Invoke();
		else if (keyControl == Keyboard.current.rightArrowKey || keyControl == Keyboard.current.dKey)
			RightKeyPressEvent.Invoke();

		if (keyControl == Keyboard.current.digit1Key)
			OneKeyPressEvent.Invoke();
	}

	private void OnKeyReleased(KeyData keyData) {
		KeyControl keyControl = keyData.KeyControl;
		
		if (keyControl == Keyboard.current.digit1Key)
			OneKeyReleaseEvent.Invoke();
	}
}
