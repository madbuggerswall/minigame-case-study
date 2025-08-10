using System.Collections.Generic;

namespace Utilities.Tweens.Easing {
	public static class Ease {
		public enum Type {
			Linear,
			InQuad,
			OutQuad,
			InOutQuad,
			InCubic,
			OutCubic,
			InOutCubic,
			InQuart,
			OutQuart,
			InOutQuart,
			InQuint,
			OutQuint,
			InOutQuint,
			InSine,
			OutSine,
			InOutSine,
			InExpo,
			OutExpo,
			InOutExpo,
			InCirc,
			OutCirc,
			InOutCirc
		}

		private static readonly Dictionary<Type, EaseFunction> EaseFunctions = new() {
			{ Type.Linear, new Linear() },
			{ Type.InQuad, new InQuad() },
			{ Type.OutQuad, new OutQuad() },
			{ Type.InOutQuad, new InOutQuad() },
			{ Type.InCubic, new InCubic() },
			{ Type.OutCubic, new OutCubic() },
			{ Type.InOutCubic, new InOutCubic() },
			{ Type.InQuart, new InQuart() },
			{ Type.OutQuart, new OutQuart() },
			{ Type.InOutQuart, new InOutQuart() },
			{ Type.InQuint, new InQuint() },
			{ Type.OutQuint, new OutQuint() },
			{ Type.InOutQuint, new InOutQuint() },
			{ Type.InSine, new InSine() },
			{ Type.OutSine, new OutSine() },
			{ Type.InOutSine, new InOutSine() },
			{ Type.InExpo, new InExpo() },
			{ Type.OutExpo, new OutExpo() },
			{ Type.InOutExpo, new InOutExpo() },
			{ Type.InCirc, new InCirc() },
			{ Type.OutCirc, new OutCirc() },
			{ Type.InOutCirc, new InOutCirc() },
		};

		public static EaseFunction Get(Type type) {
			return EaseFunctions.GetValueOrDefault(type, new Linear());
		}
	}
}
