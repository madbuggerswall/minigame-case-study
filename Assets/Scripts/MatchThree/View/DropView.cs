using MatchThree.Model;
using UnityEngine;

namespace MatchThree.View {
	public class DropView : MonoBehaviour {
		[SerializeField] private SpriteRenderer spriteRenderer;

		public void Initialize(DropModel dropModel) {
			DropColor dropColor = dropModel.GetDropColor();
			spriteRenderer.color = GetDropColor(dropColor);
		}

		private static Color GetDropColor(DropColor dropColor) {
			return dropColor switch {
				DropColor.Blue => Color.blue,
				DropColor.Red => Color.red,
				DropColor.Green => Color.magenta,
				DropColor.Yellow => Color.yellow,
				_ => Color.blue
			};
		}
	}
}
