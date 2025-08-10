using UnityEngine;

namespace Utilities.Tweens.RectTransformTweens {
	public static class RectTransformTweenExtensions {
		public static AnchoredPositionTween PlayAnchoredPosition(
			this RectTransform tweener,
			Vector2 targetPosition,
			float duration
		) {
			return new AnchoredPositionTween(tweener, targetPosition, duration);
		}

		public static ScaleTween PlayScale(this RectTransform tweener, Vector3 targetScale, float duration) {
			return new ScaleTween(tweener, targetScale, duration);
		}

		public static EulerAnglesTween PlayEuler(this RectTransform tweener, Vector3 targetEuler, float duration) {
			return new EulerAnglesTween(tweener, targetEuler, duration);
		}
	}
}