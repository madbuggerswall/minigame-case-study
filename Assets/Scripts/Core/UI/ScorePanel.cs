using TMPro;
using UnityEngine;

namespace Core.UI {
	public class ScorePanel : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI scoreText;

		public void UpdateScore(int score) {
			scoreText.text = score.ToString();
		}
	}
}
