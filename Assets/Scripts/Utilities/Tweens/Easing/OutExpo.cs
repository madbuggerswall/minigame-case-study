using UnityEngine;

namespace Utilities.Tweens.Easing {
	public class OutExpo : EaseFunction {
		public override float Evaluate(float time) {
			return Mathf.Approximately(time, 1) ? 1 : 1 - Mathf.Pow(2, -10 * time);
		}
	}
}