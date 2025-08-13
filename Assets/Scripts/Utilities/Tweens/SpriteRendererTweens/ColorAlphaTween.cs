using UnityEngine;

namespace Utilities.Tweens.SpriteRendererTweens {
	public class ColorAlphaTween : Tween {
		private readonly SpriteRenderer tweener;
		private (float initial, float target) alpha;

		public ColorAlphaTween(SpriteRenderer tweener, float targetAlpha, float duration) : base(duration) {
			this.tweener = tweener;
			alpha.initial = tweener.color.a;
			alpha.target = targetAlpha;
		}

		protected override void UpdateTween() {
			Color tweenerColor = tweener.color;
			tweenerColor.a = Mathf.Lerp(alpha.initial, alpha.target, progress);
			tweener.color = tweenerColor;
		}

		protected override void SampleInitialState() {
			alpha.initial = tweener.color.a;
		}
	}
}
