using UnityEngine;

namespace Utilities.Tweens.SpriteRendererTweens {
	public class ColorTween : Tween {
		private readonly SpriteRenderer tweener;
		private (Color initial, Color target) color;

		public ColorTween(SpriteRenderer tweener, Color targetColor, float duration) : base(duration) {
			this.tweener = tweener;
			this.color.initial = tweener.color;
			this.color.target = targetColor;
		}

		protected override void UpdateTween() {
			tweener.color = Color.Lerp(color.initial, color.target, progress);
		}

		protected override void SampleInitialState() {
			this.color.initial = tweener.color;
		}
	}
}
