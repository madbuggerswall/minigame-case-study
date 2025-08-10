using UnityEngine;
using Utilities.Tweens.Easing;

namespace Utilities.Tweens.TransformTweens {
	public class PositionTween : Tween {
		private readonly Transform tweener;
		private (Vector3 initial, Vector3 target) position;

		public PositionTween(Transform tweener, Vector3 targetPosition, float duration) : base(duration) {
			this.tweener = tweener;
			this.position.initial = tweener.position;
			this.position.target = targetPosition;
		}

		protected override void UpdateTween() {
			tweener.position = Vector3.Lerp(position.initial, position.target, progress);
		}

		protected override void SampleInitialState() {
			this.position.initial = tweener.position;
		}
	}

	public class PositionXTween : Tween {
		private readonly Transform tweener;
		private (float initial, float target) positionX;

		public PositionXTween(Transform tweener, float targetX, float duration) : base(duration) {
			this.tweener = tweener;
			this.positionX.initial = tweener.position.x;
			this.positionX.target = targetX;
		}

		protected override void UpdateTween() {
			float posX = Mathf.Lerp(positionX.initial, positionX.target, progress);
			tweener.position = new Vector3(posX, tweener.position.y, tweener.position.z);
		}

		protected override void SampleInitialState() {
			this.positionX.initial = tweener.position.x;
		}
	}

	public class PositionYTween : Tween {
		private readonly Transform tweener;
		private (float initial, float target) positionY;

		public PositionYTween(Transform tweener, float targetY, float duration) : base(duration) {
			this.tweener = tweener;
			this.positionY.initial = tweener.position.y;
			this.positionY.target = targetY;
		}

		protected override void UpdateTween() {
			float posY = Mathf.Lerp(positionY.initial, positionY.target, progress);
			tweener.position = new Vector3(tweener.position.x, posY, tweener.position.z);
		}

		protected override void SampleInitialState() {
			this.positionY.initial = tweener.position.y;
		}
	}

	public class PositionZTween : Tween {
		private readonly Transform tweener;
		private (float initial, float target) positionZ;

		public PositionZTween(Transform tweener, float targetZ, float duration) : base(duration) {
			this.tweener = tweener;
			this.positionZ.initial = tweener.position.z;
			this.positionZ.target = targetZ;
		}

		protected override void UpdateTween() {
			float posZ = Mathf.Lerp(positionZ.initial, positionZ.target, progress);
			tweener.position = new Vector3(tweener.position.x, tweener.position.y, posZ);
		}

		protected override void SampleInitialState() {
			this.positionZ.initial = tweener.position.z;
		}
	}

	public class PositionXYZTween : Tween {
		private readonly Transform tweener;

		private EaseFunction easeFunctionPosX = new Linear();
		private EaseFunction easeFunctionPosY = new Linear();
		private EaseFunction easeFunctionPosZ = new Linear();

		private (Vector3 initial, Vector3 target) position;

		public PositionXYZTween(Transform tweener, Vector3 targetPosition, float duration) : base(duration) {
			this.tweener = tweener;
			this.position.initial = tweener.position;
			this.position.target = targetPosition;
		}

		protected override void UpdateTween() {
			// progress will be eased if SetEase is used to assign a function other than Linear
			// which may result in overlapping easing with axis-specific functions.
			float easedX = Mathf.Lerp(position.initial.x, position.target.x, easeFunctionPosX.Evaluate(progress));
			float easedY = Mathf.Lerp(position.initial.y, position.target.y, easeFunctionPosY.Evaluate(progress));
			float easedZ = Mathf.Lerp(position.initial.z, position.target.z, easeFunctionPosZ.Evaluate(progress));

			tweener.position = new Vector3(easedX, easedY, easedZ);
		}

		protected override void SampleInitialState() {
			this.position.initial = tweener.position;
		}

		public void SetEaseX(Ease.Type easeType) => this.easeFunctionPosX = Ease.Get(easeType);
		public void SetEaseY(Ease.Type easeType) => this.easeFunctionPosY = Ease.Get(easeType);
		public void SetEaseZ(Ease.Type easeType) => this.easeFunctionPosZ = Ease.Get(easeType);

		public void SetEaseX(AnimationCurve animationCurve) => this.easeFunctionPosX = new Curve(animationCurve);
		public void SetEaseY(AnimationCurve animationCurve) => this.easeFunctionPosY = new Curve(animationCurve);
		public void SetEaseZ(AnimationCurve animationCurve) => this.easeFunctionPosZ = new Curve(animationCurve);
	}
}
