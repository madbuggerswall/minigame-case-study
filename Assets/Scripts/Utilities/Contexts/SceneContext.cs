using UnityEngine;

namespace Utilities.Contexts {
	[DefaultExecutionOrder(-28)]
	public abstract class SceneContext : Context {
		private static SceneContext instance;

		private void Awake() {
			AssertSingleton();
			ResolveContext();
			InitializeContext();
			OnInitialized();
		}

		// Singleton Operations
		private void AssertSingleton() {
			if (instance is not null)
				Destroy(instance);

			instance = this;
		}

		public static SceneContext GetInstance() => instance;
	}
}

/*
   1.	Constants & Static Readonly Fields
   2.	Static Variables
   3.	Enums
   4.	Serialized Fields
   5.	Public Fields
   6.	Private Fields
   7.	Properties (instance first, then static if possible)
   8.	Events
   9.	Unity Lifecycle Methods (Awake, Start, etc.)
   10.	Overridden Methods (ToString, Equals, etc.)
   11.	Public Static Methods
   12.	Public Instance Methods
   13.	Private Static Methods
   14.	Private Instance Methods
   15.	Coroutines
   16.	Inner Classes / Structs
 */
