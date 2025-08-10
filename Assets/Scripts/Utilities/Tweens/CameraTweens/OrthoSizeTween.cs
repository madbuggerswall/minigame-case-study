using UnityEngine;

namespace Utilities.Tweens.CameraTweens {
	public class OrthoSizeTween : Tween {
		private readonly Camera tweener;
		private (float initial, float target) orthoSize;

		public OrthoSizeTween(Camera tweener, float targetOrthoSize, float duration) : base(duration) {
			this.tweener = tweener;
			this.orthoSize.initial = tweener.orthographicSize;
			this.orthoSize.target = targetOrthoSize;
		}

		protected override void UpdateTween() {
			tweener.orthographicSize = Mathf.Lerp(orthoSize.initial, orthoSize.target, progress);
		}

		protected override void SampleInitialState() {
			this.orthoSize.initial = tweener.orthographicSize;
		}
	}
}
