using UnityEngine;

namespace Utilities.Tweens.RigidbodyTweens {
	public class PositionTween : RigidbodyTween {
		private (Vector3 initial, Vector3 target) position;

		public PositionTween(Rigidbody tweener, Vector3 targetPosition, float duration) : base(tweener, duration) {
			position.initial = tweener.position;
			position.target = targetPosition;
		}

		protected override void UpdateTween() {
			tweener.MovePosition(Vector3.Lerp(position.initial, position.target, progress));
		}

		protected override void SampleInitialState() {
			position.initial = tweener.position;
		}
	}
}
