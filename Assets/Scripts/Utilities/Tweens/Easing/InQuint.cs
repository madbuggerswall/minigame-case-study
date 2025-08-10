namespace Utilities.Tweens.Easing {
	public class InQuint : EaseFunction {
		public override float Evaluate(float time) {
			return time * time * time * time * time;
		}
	}
}