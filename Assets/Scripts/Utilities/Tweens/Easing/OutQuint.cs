namespace Utilities.Tweens.Easing {
	public class OutQuint : EaseFunction {
		public override float Evaluate(float time) {
			return 1 - (1 - time) * (1 - time) * (1 - time) * (1 - time) * (1 - time);
		}
	}
}