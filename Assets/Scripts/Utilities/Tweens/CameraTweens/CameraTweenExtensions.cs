using UnityEngine;

namespace Utilities.Tweens.CameraTweens {
	public static class CameraTweenExtensions {
		public static OrthoSizeTween PlayOrthoSize(this Camera camera, float targetOrthoSize, float duration) {
			return new OrthoSizeTween(camera, targetOrthoSize, duration);
		}
	}
}
