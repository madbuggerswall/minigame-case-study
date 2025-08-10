using System.Collections.Generic;
using UnityEngine;

namespace Utilities {
	// TODO Might be redundant
	public static class Yielders {
		private static readonly Dictionary<float, WaitForSeconds> WaitTimes = new();
		private static readonly Dictionary<float, WaitForSecondsRealtime> WaitTimesReal = new();

		public static WaitForEndOfFrame WaitForEndOfFrame { get; } = new();
		public static WaitForFixedUpdate WaitForFixedUpdate { get; } = new();

		public static WaitForSeconds WaitForSeconds(float seconds) {
			if (!WaitTimes.ContainsKey(seconds))
				WaitTimes.Add(seconds, new WaitForSeconds(seconds));

			return WaitTimes[seconds];
		}

		public static WaitForSecondsRealtime WaitForSecondsRealtime(float seconds) {
			if (!WaitTimesReal.ContainsKey(seconds))
				WaitTimesReal.Add(seconds, new WaitForSecondsRealtime(seconds));

			return WaitTimesReal[seconds];
		}
	}
}
