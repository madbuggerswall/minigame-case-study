using UnityEngine;

namespace Utilities.Tweens.Easing {
	public class InOutCirc : EaseFunction {
		public override float Evaluate(float time) {
			return time < 0.5
				? (1 - Mathf.Sqrt(1 - (2 * time) * (2 * time))) / 2
				: (Mathf.Sqrt(1 - (-2 * time + 2) * (-2 * time + 2)) + 1) / 2;
		}
	}
}