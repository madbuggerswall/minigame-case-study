using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = FileName, menuName = MenuName)]
public class MinigameDefinition : ScriptableObject {
	private const string FileName = "MinigameDefinition";
	private const string MenuName = "Scriptable Objects/" + FileName;
	
	[SerializeField] private string minigameName; 
	[SerializeField] private int sceneBuildIndex; 
	
	public string GetMinigameName() => minigameName;
	public int GetSceneBuildIndex() => sceneBuildIndex;
}
