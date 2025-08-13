using UnityEngine;
using Utilities.Contexts;

namespace Minigames {
	public class MinigameDefinitionManager : MonoBehaviour, IInitializable {
		[SerializeField] private MinigameDefinition[] minigameDefinitions;

		public void Initialize() { }
		public MinigameDefinition[] GetMinigameDefinitions() => minigameDefinitions;
	}
}
