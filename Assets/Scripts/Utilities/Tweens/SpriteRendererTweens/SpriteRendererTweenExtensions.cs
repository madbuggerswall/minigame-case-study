using UnityEngine;

namespace Utilities.Tweens.SpriteRendererTweens {
	public static class SpriteRendererTweenExtensions {
		public static ColorTween PlayColor(this SpriteRenderer tweener, Color targetColor, float duration) {
			return new ColorTween(tweener, targetColor, duration);
		}

		public static ColorAlphaTween PlayColorAlpha(this SpriteRenderer tweener, float targetAlpha, float duration) {
			return new ColorAlphaTween(tweener, targetAlpha, duration);
		}
	}
}
