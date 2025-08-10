using UnityEngine;

namespace Utilities.Tweens.Easing {
	public class OutSine : EaseFunction {
		public override float Evaluate(float time) {
			return Mathf.Sin(time * Mathf.PI / 2);
		}
	}
}