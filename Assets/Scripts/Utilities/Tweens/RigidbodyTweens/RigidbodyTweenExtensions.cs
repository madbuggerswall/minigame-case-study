using UnityEngine;

namespace Utilities.Tweens.RigidbodyTweens {
	public static class RigidbodyTweenExtensions {
		public static PositionTween PlayPosition(this Rigidbody tweener, Vector3 targetPosition, float duration) {
			return new PositionTween(tweener, targetPosition, duration);
		}

		public static PositionXYZTween PlayPositionXYZ(this Rigidbody tweener, Vector3 targetPosition, float duration) {
			return new PositionXYZTween(tweener, targetPosition, duration);
		}
	}
}
