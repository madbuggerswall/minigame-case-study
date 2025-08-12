using UnityEngine.Events;

namespace MatchThree {
	public class ViewReadyNotifier {
		private bool isFallTweensComplete = true;
		private bool isFillTweensComplete = true;
		private bool isSwapTweensComplete = true;

		public UnityEvent OnViewReady { get; } = new UnityEvent();

		public void OnFallTweensComplete() {
			isFallTweensComplete = true;
			TryNotifyViewReady();
		}

		public void OnFillTweensComplete() {
			isFillTweensComplete = true;
			TryNotifyViewReady();
		}

		public void OnSwapTweensComplete() {
			isSwapTweensComplete = true;
			TryNotifyViewReady();
		}

		public void WaitForFallTweens() => isFallTweensComplete = false;

		public void WaitForFillTweens() => isFillTweensComplete = false;

		public void WaitForSwapTweens() => isSwapTweensComplete = false;
		

		private void TryNotifyViewReady() {
			if (!isFillTweensComplete || !isFallTweensComplete || !isSwapTweensComplete)
				return;

			OnViewReady.Invoke();
			OnViewReady.RemoveAllListeners();
		}
	}
}
