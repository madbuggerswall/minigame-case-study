using System.Collections.Generic;
using MatchThree.Targets;
using UnityEngine;
using UnityEngine.UI;

namespace MatchThree.UI {
	public class ElementTargetsPanel : MonoBehaviour {
		[SerializeField] private PuzzleElementTargetView targetViewPrefab;
		[SerializeField] private HorizontalLayoutGroup layoutGroup;

		private readonly Dictionary<PuzzleElementTarget, PuzzleElementTargetView> targetViewsByTarget = new();

		public void Initialize(PuzzleElementTarget[] targets) {
			for (int i = 0; i < targets.Length; i++) {
				PuzzleElementTarget target = targets[i];
				PuzzleElementTargetView targetView = Instantiate(targetViewPrefab, layoutGroup.transform);
				targetView.Initialize(target);
				targetViewsByTarget.Add(target, targetView);
			}
		}

		public void UpdateTargetView(PuzzleElementTarget target) {
			if (targetViewsByTarget.TryGetValue(target, out PuzzleElementTargetView targetView))
				targetView.UpdateRemainingAmount(target);
		}
	}
}
