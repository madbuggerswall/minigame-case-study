using TMPro;
using UnityEngine;

namespace Core.UI {
	public class ScoreTargetPanel : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI remainingScoreText;

		public void UpdateRemainingScore(int targetScore, int currentScore) {
			int remainingScore = Mathf.Max(targetScore - currentScore, 0);
			remainingScoreText.text = remainingScore.ToString();
		}
	}
}
