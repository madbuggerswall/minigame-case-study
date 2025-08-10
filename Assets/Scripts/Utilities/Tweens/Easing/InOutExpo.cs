using UnityEngine;

namespace Utilities.Tweens.Easing {
	public class InOutExpo : EaseFunction {
		public override float Evaluate(float time) {
			if (Mathf.Approximately(time, 0))
				return 0;
			else if (Mathf.Approximately(time, 1))
				return 1;
			else
				return time < 0.5 ? Mathf.Pow(2, 20 * time - 10) / 2 : (2 - Mathf.Pow(2, -20 * time + 10)) / 2;
		}
	}
}