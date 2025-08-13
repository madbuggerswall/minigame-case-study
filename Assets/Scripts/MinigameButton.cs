using Minigames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameButton : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI buttonText;
	[SerializeField] private Button button;
	
	public void Initialize(MinigameDefinition minigameDefinition) {
		buttonText.text = minigameDefinition.GetMinigameName();
	}

	public Button GetButton() => button;
}
