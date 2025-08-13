using UnityEngine;
using Utilities.Tweens.Easing;

namespace Utilities.Tweens.RigidbodyTweens {
	/// <summary>
	/// A Rigidbody tween that allows independent easing functions for each position axis (X, Y, Z).
	/// This class is useful when you want to animate a Rigidbody's position with different motion profiles
	/// on each axis simultaneously.
	/// </summary>
	/// <remarks>
	/// Using <c>SetEase</c> may produce unintended results.
	/// Use <c>SetEasePosX</c>, <c>SetEasePosY</c>, or <c>SetEasePosZ</c> instead.
	/// </remarks>
	public class PositionXYZTween : RigidbodyTween {
		private EaseFunction easeFunctionPosX = new Linear();
		private EaseFunction easeFunctionPosY = new Linear();
		private EaseFunction easeFunctionPosZ = new Linear();

		private (Vector3 initial, Vector3 target) position;

		public PositionXYZTween(Rigidbody tweener, Vector3 targetPosition, float duration) : base(tweener, duration) {
			position.initial = tweener.position;
			position.target = targetPosition;
		}

		protected override void UpdateTween() {
			// progress will be eased if SetEase is used to assign a function other than Linear
			// which may result in overlapping easing with axis-specific functions.
			float easedX = Mathf.Lerp(position.initial.x, position.target.x, easeFunctionPosX.Evaluate(progress));
			float easedY = Mathf.Lerp(position.initial.y, position.target.y, easeFunctionPosY.Evaluate(progress));
			float easedZ = Mathf.Lerp(position.initial.z, position.target.z, easeFunctionPosZ.Evaluate(progress));

			tweener.MovePosition(new Vector3(easedX, easedY, easedZ));
		}

		protected override void SampleInitialState() {
			position.initial = tweener.position;
		}

		public void SetEaseX(Ease.Type easeType) => easeFunctionPosX = Ease.Get(easeType);
		public void SetEaseY(Ease.Type easeType) => easeFunctionPosY = Ease.Get(easeType);
		public void SetEaseZ(Ease.Type easeType) => easeFunctionPosZ = Ease.Get(easeType);

		public void SetEaseX(AnimationCurve animationCurve) => easeFunctionPosX = new Curve(animationCurve);
		public void SetEaseY(AnimationCurve animationCurve) => easeFunctionPosY = new Curve(animationCurve);
		public void SetEaseZ(AnimationCurve animationCurve) => easeFunctionPosZ = new Curve(animationCurve);
	}
}