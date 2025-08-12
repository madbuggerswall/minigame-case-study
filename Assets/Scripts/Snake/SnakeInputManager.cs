using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Utilities.Contexts;
using Utilities.Input;
using Utilities.Input.Standalone;

public class SnakeInputManager : IInitializable {
	// Dependencies
	private InputManager inputManager;

	// Arrow Keys
	private readonly KeyControl upArrowKey = Keyboard.current.upArrowKey;
	private readonly KeyControl downArrowKey = Keyboard.current.downArrowKey;
	private readonly KeyControl leftArrowKey = Keyboard.current.leftArrowKey;
	private readonly KeyControl rightArrowKey = Keyboard.current.rightArrowKey;

	// WASD Keys
	private readonly KeyControl wKey = Keyboard.current.wKey;
	private readonly KeyControl aKey = Keyboard.current.aKey;
	private readonly KeyControl sKey = Keyboard.current.sKey;
	private readonly KeyControl dKey = Keyboard.current.dKey;

	public Action UpKeyPressEvent { get; set; } = delegate { };
	public Action DownKeyPressEvent { get; set; } = delegate { };
	public Action LeftKeyPressEvent { get; set; } = delegate { };
	public Action RightKeyPressEvent { get; set; } = delegate { };

	public void Initialize() {
		this.inputManager = SceneContext.GetInstance().Get<InputManager>();

		// Subscribe to InputHandler events
		StandaloneInputHandler inputHandler = inputManager.StandaloneInputHandler;
		inputHandler.KeyPressEvent += OnKeyPressed;
		inputHandler.KeyReleaseEvent += OnKeyReleased;
	}

	private void OnKeyPressed(KeyData keyData) {
		KeyControl keyControl = keyData.KeyControl;

		if (keyControl == upArrowKey || keyControl == wKey)
			UpKeyPressEvent.Invoke();
		else if (keyControl == downArrowKey || keyControl == sKey)
			DownKeyPressEvent.Invoke();
		else if (keyControl == leftArrowKey || keyControl == aKey)
			LeftKeyPressEvent.Invoke();
		else if (keyControl == rightArrowKey || keyControl == dKey)
			RightKeyPressEvent.Invoke();
	}

	private void OnKeyReleased(KeyData keyData) { }
}
