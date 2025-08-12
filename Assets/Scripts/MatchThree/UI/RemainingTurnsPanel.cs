using TMPro;
using UnityEngine;

namespace MatchThree.UI {
	public class RemainingTurnsPanel : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI remainingMovesText;

		public void UpdateRemainingTurns(int remainingMoves) {
			remainingMovesText.text = remainingMoves.ToString();
		}
	}
}
