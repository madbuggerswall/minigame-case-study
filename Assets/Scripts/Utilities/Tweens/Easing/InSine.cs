using UnityEngine;

namespace Utilities.Tweens.Easing {
	public class InSine : EaseFunction {
		public override float Evaluate(float time) {
			return 1 - Mathf.Cos(time * Mathf.PI / 2);
		}
	}
}