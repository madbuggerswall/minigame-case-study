using UnityEngine;

namespace Utilities.Tweens.RectTransformTweens {
	public class ScaleTween : Tween {
		private readonly RectTransform tweener;
		private (Vector3 initial, Vector3 target) localScale;

		public ScaleTween(RectTransform tweener, Vector3 targetScale, float duration) : base(duration) {
			this.tweener = tweener;
			localScale.initial = tweener.localScale;
			localScale.target = targetScale;
		}

		protected override void UpdateTween() {
			tweener.localScale = Vector3.Lerp(localScale.initial, localScale.target, progress);
		}

		protected override void SampleInitialState() {
			localScale.initial = tweener.localScale;
		}
	}
}
