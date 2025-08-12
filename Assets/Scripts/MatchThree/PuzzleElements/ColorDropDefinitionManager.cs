using UnityEngine;
using Utilities.Contexts;

namespace MatchThree.PuzzleElements {
	public class ColorDropDefinitionManager : MonoBehaviour, IInitializable {
		[Header("Color Drop Definitions")]
		[SerializeField] private ColorDropDefinition[] colorDropDefinitions;

		public void Initialize() { }

		public ColorDropDefinition GetRandomColorChipDefinition() {
			int randomIndex = Random.Range(0, colorDropDefinitions.Length);
			ColorDropDefinition colorDropDefinition = colorDropDefinitions[randomIndex];
			return colorDropDefinition;
		}
	}
}
