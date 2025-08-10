namespace Utilities.Tweens.Easing {
	public class InOutQuint : EaseFunction {
		public override float Evaluate(float time) {
			return time < 0.5
				? 16 * time * time * time * time * time
				: 1 - (-2 * time + 2) * (-2 * time + 2) * (-2 * time + 2) * (-2 * time + 2) * (-2 * time + 2) / 2;
		}
	}
}