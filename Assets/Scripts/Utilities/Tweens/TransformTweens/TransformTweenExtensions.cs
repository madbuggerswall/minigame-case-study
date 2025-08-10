using UnityEngine;

namespace Utilities.Tweens.TransformTweens {
	public static class TransformTweenExtensions {
		public static PositionTween PlayPosition(this Transform tweener, Vector3 targetPosition, float duration) {
			return new PositionTween(tweener, targetPosition, duration);
		}

		public static PositionXTween PlayPositionX(this Transform tweener, float targetX, float duration) {
			return new PositionXTween(tweener, targetX, duration);
		}

		public static PositionYTween PlayPositionY(this Transform tweener, float targetY, float duration) {
			return new PositionYTween(tweener, targetY, duration);
		}

		public static PositionZTween PlayPositionZ(this Transform tweener, float targetZ, float duration) {
			return new PositionZTween(tweener, targetZ, duration);
		}

		public static PositionXYZTween PlayPositionXYZ(this Transform tweener, Vector3 targetPosition, float duration) {
			return new PositionXYZTween(tweener, targetPosition, duration);
		}

		public static ScaleTween PlayScale(this Transform tweener, Vector3 targetScale, float duration) {
			return new ScaleTween(tweener, targetScale, duration);
		}

		public static RotationTween PlayRotation(this Transform tweener, Quaternion targetRotation, float duration) {
			return new RotationTween(tweener, targetRotation, duration);
		}

		public static RotateAroundTween PlayRotateAround(
			this Transform tweener,
			Vector3 axis,
			Vector3 pivot,
			float targetAngle,
			float duration
		) {
			return new RotateAroundTween(tweener, axis, pivot, targetAngle, duration);
		}
	}
}
