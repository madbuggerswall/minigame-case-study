#### **How** to Run

- Run the **MainScene** and select a mini-game to play.
- Both games can be played with **WASD** or arrow keys.
- The game is designed for **portrait mode** (810×1800).

---

#### Architecture

- **MainScene** contains a `SceneContext` for global dependencies (e.g., `SignalBus`, `TweenManager`).
- The `MinigameLoader` class loads the selected mini-game scene additively and asynchronously on top of the main scene.
- Each loaded mini-game scene has its own `SubContext`, which keeps dependencies in a limited scope rather than using a monolithic service locator.
- When needed, `SubContext` elements or other mini-game classes can still access `MainContext` dependencies.

---

#### Utilities

The **Utilities** namespace contains generic tools used throughout the game.
It’s a subset of my personal framework/library, developed over time.
Mini-games rely on utilities such as `ObjectPool`, `SubContext`, `TweenManager`, `InputManager`, and `SignalBus` from this namespace.

> Example: `RunnerInputManager` listens to `InputManager.StandaloneInputHandler` events to map custom controls.

---

#### Mini-game Structure

- Mini-games have basic game loops with no explicit success states. Fail states simply restart the game or return the player to the main scene.
- Both mini-games follow the same core structure:
  1. Their respective `SubContext` initializes all mini-game dependencies in sequence.
  2. `LevelInitializers` spawn game elements using Factory classes.
  3. `LevelManagers` control gameplay, triggering fail states or score increases.
  4. `Snake` / `Runner` classes handle their own controls.
  5. `UIControllers` update score and high score by listening to relevant events.
- When a scene is unloaded, `SaveManagers` save the high score to their respective files in the persistent data path.

---

#### **Known Issues & Future Improvements**

Some mini-game classes currently result in code duplication:

- `RunnerSaveManager` & `SnakeSaveManager`
- `RunnerUIController` & `SnakeUIController`
- `RunnerInputManager` & `SnakeInputManager`
- `RunnerGrid` & `SnakeGrid`
- `RunnerScoreManager` & `SnakeScoreManager`

I intentionally kept these classes separate, as different mini-games can require very different `UIControllers`, `InputManagers`, and so on. However, some of them (e.g., `RunnerGrid` and `SnakeGrid`) could share an abstract base class, as they are virtually identical.
