namespace Utilities.Tweens.Easing {
	public class InOutQuart : EaseFunction {
		public override float Evaluate(float time) {
			return time < 0.5
				? 8 * time * time * time * time
				: 1 - (-2 * time + 2) * (-2 * time + 2) * (-2 * time + 2) * (-2 * time + 2) / 2;
		}
	}
}