namespace Utilities.Tweens.Easing {
	public class InQuad : EaseFunction {
		public override float Evaluate(float time) {
			return time * time;
		}
	}
}