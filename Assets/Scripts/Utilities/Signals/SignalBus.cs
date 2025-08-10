using System;
using System.Collections.Generic;
using Utilities.Contexts;

namespace Utilities.Signals {
	public class SignalBus : IInitializable {
		private readonly Dictionary<Type, CallbackSet<Signal>> callbackSetsBySignalType = new();

		public void Initialize() {
			callbackSetsBySignalType.Clear();
		}

		public void Fire<T>(T signal) where T : Signal {
			// Invoke callbacks if they exist
			if (callbackSetsBySignalType.TryGetValue(typeof(T), out CallbackSet<Signal> callbackSet)) {
				callbackSet.Invoke(signal);
			}
		}

		public void SubscribeTo<T>(Action<T> callback) where T : Signal {
			// Create a callback set if not present
			if (!callbackSetsBySignalType.TryGetValue(typeof(T), out CallbackSet<Signal> callbackSet)) {
				callbackSet = new CallbackSet<Signal>();
				callbackSetsBySignalType.Add(typeof(T), callbackSet);
			}

			// Add callback to the set
			callbackSet.Add(signal => callback(signal as T));
		}

		public void UnsubscribeFrom<T>(Action<T> callback) where T : Signal {
			if (callbackSetsBySignalType.TryGetValue(typeof(T), out CallbackSet<Signal> callbackSet)) {
				callbackSet.Remove((Signal signal) => callback((T) signal));
			}
		}

		public void ClearListeners<T>() where T : Signal {
			if (callbackSetsBySignalType.TryGetValue(typeof(T), out CallbackSet<Signal> callbackSet)) {
				callbackSet.Clear();
			}
		}
	}
}
