using UnityEngine;

namespace Utilities.Tweens.RectTransformTweens {
	public class EulerAnglesTween : Tween {
		private readonly RectTransform tweener;
		private (Vector3 initial, Vector3 target) eulerAngles;

		public EulerAnglesTween(RectTransform tweener, Vector3 targetAngle, float duration) : base(duration) {
			this.tweener = tweener;
			this.eulerAngles.initial = tweener.eulerAngles;
			this.eulerAngles.target = targetAngle;
		}

		protected override void UpdateTween() {
			tweener.eulerAngles = Vector3.Lerp(eulerAngles.initial, eulerAngles.target, progress);
		}

		protected override void SampleInitialState() {
			this.eulerAngles.initial = tweener.eulerAngles;
		}
	}
}
