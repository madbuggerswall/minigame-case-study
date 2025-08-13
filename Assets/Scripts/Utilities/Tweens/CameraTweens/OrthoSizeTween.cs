using UnityEngine;

namespace Utilities.Tweens.CameraTweens {
	public class OrthoSizeTween : Tween {
		private readonly Camera tweener;
		private (float initial, float target) orthoSize;

		public OrthoSizeTween(Camera tweener, float targetOrthoSize, float duration) : base(duration) {
			this.tweener = tweener;
			orthoSize.initial = tweener.orthographicSize;
			orthoSize.target = targetOrthoSize;
		}

		protected override void UpdateTween() {
			tweener.orthographicSize = Mathf.Lerp(orthoSize.initial, orthoSize.target, progress);
		}

		protected override void SampleInitialState() {
			orthoSize.initial = tweener.orthographicSize;
		}
	}
}
