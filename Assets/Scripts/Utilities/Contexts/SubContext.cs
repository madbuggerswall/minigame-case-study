using System;
using UnityEngine;

namespace Utilities.Contexts {
	// Curiously recurring template pattern
	[DefaultExecutionOrder(-24)]
	public abstract class SubContext<T> : Context where T : SubContext<T> {
		private static T instance;

		private void Awake() {
			AssertSingleton();
			ResolveContext();
			InitializeContext();
			OnInitialized();
		}

		// TODO Might be a protected virtual
		private void OnDestroy() {
			if (instance == this)
				instance = null;
		}

		private void AssertSingleton() {
			if (instance is not null)
				Destroy(instance);

			instance = this as T;
		}

		public static T GetInstance() => instance;
	}
}
