using UnityEngine;

namespace Utilities.Tweens.Easing {
	public class InCirc : EaseFunction {
		public override float Evaluate(float time) {
			return 1f - Mathf.Sqrt(1 - time * time);
		}
	}
}