using UnityEngine;

namespace Utilities.Tweens.RectTransformTweens {
	public class AnchoredPositionTween : Tween {
		private readonly RectTransform tweener;
		private (Vector2 initial, Vector2 target) anchoredPosition;

		public AnchoredPositionTween(RectTransform tweener, Vector2 targetPosition, float duration) : base(duration) {
			this.tweener = tweener;
			anchoredPosition.initial = tweener.anchoredPosition;
			anchoredPosition.target = targetPosition;
		}

		protected override void UpdateTween() {
			tweener.anchoredPosition = Vector2.Lerp(anchoredPosition.initial, anchoredPosition.target, progress);
		}

		protected override void SampleInitialState() {
			anchoredPosition.initial = tweener.anchoredPosition;
		}
	}
}
