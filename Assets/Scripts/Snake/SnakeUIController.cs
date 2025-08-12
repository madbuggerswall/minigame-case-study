using Core.UI;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Contexts;

public class SnakeUIController : MonoBehaviour, IInitializable {
	[SerializeField] private LevelEndPanel levelSuccessPanel;
	[SerializeField] private LevelEndPanel levelFailPanel;
	[SerializeField] private ElementTargetsPanel elementTargetsPanel;
	[SerializeField] private ScorePanel scorePanel;
	[SerializeField] private Button restartButton;

	public void Initialize() { }
}
