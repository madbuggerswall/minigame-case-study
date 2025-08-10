using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Contexts {
	[DefaultExecutionOrder(-32)]
	public abstract class Context : MonoBehaviour {
		protected abstract void ResolveContext();
		protected abstract void OnInitialized();
		protected internal abstract void Resolve<T>() where T : IInitializable, new();
	}

	public abstract class SceneContext : Context {
		private readonly Dictionary<Type, IInitializable> contextItems = new();
		private static SceneContext instance;

		private void Awake() {
			AssertSingleton();
			ResolveContext();
			InitializeContext();
			OnInitialized();
		}

		internal void InitializeContext() {
			//.NET Standard 2.1 preserves dictionary insertion order which is vital
			foreach ((Type _, IInitializable initializable) in contextItems)
				initializable.Initialize();
		}

		public T Get<T>() where T : class, IInitializable {
			if (contextItems.TryGetValue(typeof(T), out IInitializable contextItem))
				return contextItem as T;

			throw new Exception($"Context item {typeof(T)} cannot be found on current context");
		}

		protected internal override sealed void Resolve<T>() {
			if (typeof(MonoBehaviour).IsAssignableFrom(typeof(T))) {
				ResolveMonoBehaviour<T>();
			} else {
				ResolvePlainObject<T>();
			}
		}

		private void ResolveMonoBehaviour<T>() where T : IInitializable {
			T dependency = GetComponentInChildren<T>(true);

			if (dependency is null)
				throw new Exception("Dependency not found: " + typeof(T));

			// Try adding dependency to context dictionary
			if (contextItems.TryAdd(typeof(T), dependency))
				return;

			// This can be an exception as dependent systems would be broken already
			Debug.LogWarning($"Dependency {typeof(T)} is already added to context");
		}

		private void ResolvePlainObject<T>() where T : IInitializable, new() {
			T dependency = new T();
			// Try adding dependency to context dictionary
			if (contextItems.TryAdd(typeof(T), dependency))
				return;

			// This can be an exception as dependent systems would be broken already
			Debug.LogWarning($"Dependency {typeof(T)} is already added to context");
		}

		// Singleton Operations
		private void AssertSingleton() {
			if (instance is not null)
				Destroy(instance);

			instance = this;
		}

		public static SceneContext GetInstance() => instance;
	}

	public abstract class SubContext : Context {
		private SceneContext sceneContext;

		private void Awake() {
			this.sceneContext = SceneContext.GetInstance();

			ResolveContext();
			sceneContext.InitializeContext();
			OnInitialized();
		}

		protected internal override sealed void Resolve<T>() {
			sceneContext.Resolve<T>();
		}
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
