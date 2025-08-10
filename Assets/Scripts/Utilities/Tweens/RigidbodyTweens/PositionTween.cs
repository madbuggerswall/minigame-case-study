using UnityEngine;

namespace Utilities.Tweens.RigidbodyTweens {
	public class PositionTween : RigidbodyTween {
		private (Vector3 initial, Vector3 target) position;

		public PositionTween(Rigidbody tweener, Vector3 targetPosition, float duration) : base(tweener, duration) {
			this.position.initial = tweener.position;
			this.position.target = targetPosition;
		}

		protected override void UpdateTween() {
			tweener.MovePosition(Vector3.Lerp(position.initial, position.target, progress));
		}

		protected override void SampleInitialState() {
			this.position.initial = tweener.position;
		}
	}
}
