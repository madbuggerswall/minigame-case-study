namespace Utilities.Tweens.Easing {
	public class InOutCubic : EaseFunction {
		public override float Evaluate(float time) {
			return time < 0.5 ? 4 * time * time * time : 1 - (-2 * time + 2) * (-2 * time + 2) * (-2 * time + 2) / 2;
		}
	}
}