using System.Collections.Generic;
using MatchThree.Model;
using MatchThree.PuzzleElements;
using UnityEngine;
using Utilities.Tweens.Easing;
using Utilities.Tweens.TransformTweens;

namespace MatchThree.ViewHelpers {
	public class FillViewHelper {
		private const float FillDuration = 0.6f;

		private readonly Dictionary<Transform, PositionTween> fillTweens = new();
		private readonly PuzzleLevelViewController viewController;
		private readonly PuzzleGrid puzzleGrid;

		public FillViewHelper(PuzzleLevelViewController viewController, PuzzleGrid puzzleGrid) {
			this.viewController = viewController;
			this.puzzleGrid = puzzleGrid;
		}

		public void MoveFilledElements(HashSet<PuzzleElement> filledElements) {
			Vector2Int gridSize = puzzleGrid.GetGridSize();
			Dictionary<int, int> filledElementByColumn = new();
			fillTweens.Clear();

			foreach (PuzzleElement filledElement in filledElements) {
				if (!puzzleGrid.TryGetPuzzleCell(filledElement, out PuzzleCell cell))
					return;
				
				Vector2Int cellIndex = puzzleGrid.GetCellIndex(cell);
				if (!filledElementByColumn.TryAdd(cellIndex.x, 1))
					filledElementByColumn[cellIndex.x]++;

				
				PuzzleCell topColumnCell = puzzleGrid.GetCells()[cellIndex.x, gridSize.y - 1];
				Vector3 startPosition = topColumnCell.GetWorldPosition();
				startPosition.y += puzzleGrid.GetCellDiameter() * filledElementByColumn[cellIndex.x];

				PuzzleElementBehaviour elementBehaviour = viewController.SpawnElementBehaviour(filledElement as ColorDrop, cell);
				elementBehaviour.transform.position = startPosition;
				PlayFillTween(elementBehaviour.transform, cell.GetWorldPosition());
			}
		}

		private void PlayFillTween(Transform elementTransform, Vector3 targetPosition) {
			if (fillTweens.TryGetValue(elementTransform, out PositionTween positionTween)) {
				positionTween.Stop();
				fillTweens.Remove(elementTransform);
			}

			positionTween = elementTransform.PlayPosition(targetPosition, FillDuration);
			positionTween.SetEase(Ease.Type.InCubic);
			positionTween.Play();
			positionTween.SetOnComplete(() => OnFillTweenComplete(elementTransform));

			fillTweens.Add(elementTransform, positionTween);
		}

		private void OnFillTweenComplete(Transform elementTransform) {
			if (!fillTweens.Remove(elementTransform))
				return;

			if (fillTweens.Count == 0)
				viewController.ViewReadyNotifier.OnFillTweensComplete();
		}
	}
}
