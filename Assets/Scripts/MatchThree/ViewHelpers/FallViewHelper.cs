using System.Collections.Generic;
using MatchThree.PuzzleElements;
using UnityEngine;
using Utilities.Tweens.Easing;
using Utilities.Tweens.TransformTweens;

namespace MatchThree.ViewHelpers {
	public class FallViewHelper {
		private const float FallDuration = 2f;

		private readonly Dictionary<Transform, PositionTween> fallTweens = new();
		private readonly PuzzleLevelViewController viewController;
		private readonly PuzzleGrid puzzleGrid;

		public FallViewHelper(PuzzleLevelViewController viewController, PuzzleGrid puzzleGrid) {
			this.viewController = viewController;
			this.puzzleGrid = puzzleGrid;
		}

		public void MoveFallenElements(List<PuzzleElement> fallenElements) {
			fallTweens.Clear();

			for (int i = 0; i < fallenElements.Count; i++) {
				PuzzleElement fallenElement = fallenElements[i];
				if (!puzzleGrid.TryGetPuzzleCell(fallenElement, out PuzzleCell cell))
					return;

				PuzzleElementBehaviour elementBehaviour = viewController.GetPuzzleElementBehaviour(fallenElement);
				PlayFallTween(elementBehaviour.transform, cell.GetWorldPosition());
			}
		}

		private void PlayFallTween(Transform elementTransform, Vector3 targetPosition) {
			if (fallTweens.TryGetValue(elementTransform, out PositionTween positionTween)) {
				positionTween.Stop();
				fallTweens.Remove(elementTransform);
			}

			positionTween = elementTransform.PlayPosition(targetPosition, FallDuration);
			positionTween.SetEase(Ease.Type.InCubic);
			positionTween.SetOnComplete(() => OnFallTweenComplete(elementTransform));
			positionTween.Play();

			fallTweens.Add(elementTransform, positionTween);
		}

		private void OnFallTweenComplete(Transform elementTransform) {
			if (!fallTweens.Remove(elementTransform))
				return;

			if (fallTweens.Count == 0)
				viewController.ViewReadyNotifier.OnFallTweensComplete();
		}
	}
}
