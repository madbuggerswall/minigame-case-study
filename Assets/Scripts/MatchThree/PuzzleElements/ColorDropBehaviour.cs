using System;
using UnityEngine;

namespace MatchThree.PuzzleElements {
	public class ColorDropBehaviour : PuzzleElementBehaviour {
		[SerializeField] private SpriteRenderer spriteRenderer;

		private ColorDrop colorDrop;

		public override void Initialize(PuzzleElementDefinition definition, PuzzleCell puzzleCell) {
			if (definition is not ColorDropDefinition colorCubeDefinition)
				throw new Exception("Invalid PuzzleElementDefinition!");

			Initialize(colorCubeDefinition, puzzleCell);
		}

		private void Initialize(ColorDropDefinition definition, PuzzleCell puzzleCell) {
			spriteRenderer.sprite = definition.GetSprite();
			transform.position = puzzleCell.GetWorldPosition();
		}

		// Setters
		public override void SetSortingOrder(int sortingOrder) => spriteRenderer.sortingOrder = sortingOrder;
		public void SetSprite(Sprite sprite) => spriteRenderer.sprite = sprite;
	}
}
