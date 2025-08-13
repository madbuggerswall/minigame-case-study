using System;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Tweens.Easing;

// TODO Needs a tween pool
namespace Utilities.Tweens {
	public abstract class Tween {
		private Action onCompleteCallback;
		private EaseFunction easeFunction;

		protected float progress;
		private float elapsed;
		private readonly float duration;

		// Dependencies
		protected readonly TweenManager tweenManager;

		private Tween() {
			elapsed = 0;
			progress = 0;
			duration = 1;
			easeFunction = Ease.Get(Ease.Type.Linear);

			// NOTE Handle this differently
			tweenManager = SceneContext.GetInstance().Get<TweenManager>();
		}

		protected Tween(float duration) : this() {
			this.duration = duration;
		}

		public virtual void Play() {
			Rewind();
			tweenManager.AddTween(this);
		}

		protected void Rewind() {
			progress = 0;
			elapsed = 0;
			SampleInitialState();
		}

		public void Stop(bool invokeCallback = false) {
			progress = 1;
			if (invokeCallback)
				onCompleteCallback?.Invoke();
		}


		public void SetDelay(float delay) {
			throw new NotImplementedException();
		}

		public void SetEase(Ease.Type easeType) {
			easeFunction = Ease.Get(easeType);
		}

		public void SetEase(AnimationCurve animationCurve) {
			easeFunction = new Curve(animationCurve);
		}

		public void SetRepeat() {
			throw new NotImplementedException();
		}

		public void SetOnComplete(Action callback) {
			onCompleteCallback = callback;
		}

		public void InsertCallback() {
			throw new NotImplementedException();
		}


		// Tween operations
		internal void UpdateProgress(float deltaTime) {
			elapsed += deltaTime;

			// IDEA Rename progress to easedTime
			// IDEA Make normalizedTime a member field
			float normalizedTime = Mathf.Clamp01(elapsed / duration);
			progress = easeFunction.Evaluate(normalizedTime);

			UpdateTween();

			// IDEA Callback can be called from TweenManager
			if (progress >= 1)
				onCompleteCallback?.Invoke();
		}

		internal bool IsCompleted() { return progress >= 1; }
		internal bool IsPlaying() { return progress is > 0 and < 1; }

		protected abstract void UpdateTween();
		protected abstract void SampleInitialState();
	}

	public abstract class RigidbodyTween : Tween {
		protected Rigidbody tweener;

		public RigidbodyTween(Rigidbody tweener, float duration) : base(duration) {
			this.tweener = tweener;
		}

		public override void Play() {
			Rewind();
			tweenManager.AddTween(this);
		}
	}
}
