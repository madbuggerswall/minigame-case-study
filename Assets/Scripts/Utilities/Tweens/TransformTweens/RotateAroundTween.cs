using UnityEngine;

namespace Utilities.Tweens.TransformTweens {
	public class RotateAroundTween : Tween {
		private readonly Transform tweener;
		private readonly Vector3 axis;
		private readonly Vector3 pivot;
		private readonly (float initial, float target) angle;

		private readonly Vector3 initialDirection;

		public RotateAroundTween(Transform tweener, Vector3 axis, Vector3 pivot, float targetAngle, float duration)
			: base(duration) {
			this.tweener = tweener;
			this.axis = axis.normalized;
			this.pivot = pivot;
			angle.target = targetAngle;

			// Store initial direction from pivot to object
			initialDirection = tweener.position - pivot;
		}

		protected override void UpdateTween() {
			// Current direction from pivot to tweener
			Vector3 currentDirection = tweener.position - pivot;

			// Signed angle between initial and current directions
			float currentAngle = Vector3.SignedAngle(initialDirection, currentDirection, axis);

			// Target angle at this progress
			float targetAngle = Mathf.Lerp(angle.initial, angle.target, progress);

			// Rotate by the delta
			float deltaAngle = targetAngle - currentAngle;
			tweener.RotateAround(pivot, axis, deltaAngle);
		}

		protected override void SampleInitialState() {
			throw new System.NotImplementedException();
		}
	}
}
