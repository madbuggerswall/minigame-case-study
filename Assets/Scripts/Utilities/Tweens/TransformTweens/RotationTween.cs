using UnityEngine;

namespace Utilities.Tweens.TransformTweens {
	public class RotationTween : Tween {
		private readonly Transform tweener;
		private (Quaternion initial, Quaternion target) rotation;

		public RotationTween(Transform tweener, Quaternion targetRotation, float duration) : base(duration) {
			this.tweener = tweener;
			this.rotation.initial = tweener.rotation;
			this.rotation.target = targetRotation;
		}

		protected override void UpdateTween() {
			tweener.rotation = Quaternion.Lerp(rotation.initial, rotation.target, progress);
		}

		protected override void SampleInitialState() {
			this.rotation.initial = tweener.rotation;
		}
	}
}
