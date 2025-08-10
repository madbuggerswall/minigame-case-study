using System.Collections.Generic;
using UnityEngine;
using Utilities.Contexts;

namespace Utilities.Tweens {
	public class TweenManager : MonoBehaviour, IInitializable {
		private List<Tween> tweens;
		private List<RigidbodyTween> rigidbodyTweens;

		public void Initialize() {
			tweens = new List<Tween>();
			rigidbodyTweens = new List<RigidbodyTween>();
		}

		private void Update() {
			UpdateAllTweens();
		}

		private void FixedUpdate() {
			UpdateAllRigidbodyTweens();
		}

		internal void AddTween(Tween tween) {
			tweens.Add(tween);
		}

		internal void AddTween(RigidbodyTween tween) {
			rigidbodyTweens.Add(tween);
		}

		private void UpdateAllTweens() {
			for (int i = tweens.Count - 1; i >= 0; i--) {
				Tween tween = tweens[i];

				// Remove completed tween efficiently by swapping with last and popping
				if (!tween.IsCompleted()) {
					tween.UpdateProgress(Time.deltaTime);
				} else {
					int lastIndex = tweens.Count - 1;
					tweens[i] = tweens[lastIndex];
					tweens.RemoveAt(lastIndex);
				}
			}
		}

		private void UpdateAllRigidbodyTweens() {
			for (int i = rigidbodyTweens.Count - 1; i >= 0; i--) {
				RigidbodyTween tween = rigidbodyTweens[i];

				// Remove completed tween efficiently by swapping with last and popping
				if (!tween.IsCompleted()) {
					tween.UpdateProgress(Time.deltaTime);
				} else {
					int lastIndex = rigidbodyTweens.Count - 1;
					rigidbodyTweens[i] = rigidbodyTweens[lastIndex];
					rigidbodyTweens.RemoveAt(lastIndex);
				}
			}
		}
	}
}
