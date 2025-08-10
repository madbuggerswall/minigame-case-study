using UnityEngine;

namespace Utilities.Tweens.Easing {
	public class InExpo : EaseFunction {
		public override float Evaluate(float time) {
			return Mathf.Approximately(time, 0) ? 0 : Mathf.Pow(2, 10 * time - 10);
		}
	}
}