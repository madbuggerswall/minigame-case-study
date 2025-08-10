namespace Utilities.Tweens.Easing {
	public class InOutQuad : EaseFunction {
		public override float Evaluate(float time) {
			return time < 0.5f ? 2f * time * time : 1 - (-2 * time + 2) * (-2 * time + 2) / 2;
		}
	}
}