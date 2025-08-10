using UnityEngine;

namespace Utilities.Tweens.Easing {
	public class Curve : EaseFunction {
		private readonly AnimationCurve curve;

		public Curve(AnimationCurve curve) {
			this.curve = curve;
		}

		public override float Evaluate(float time) {
			return curve.Evaluate(time);
		}
	}
}
