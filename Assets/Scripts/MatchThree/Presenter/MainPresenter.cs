using System.Collections.Generic;
using MatchThree.Model;
using MatchThree.View;
using UnityEngine;
using Utilities.Contexts;
using Utilities.Tweens.TransformTweens;

namespace MatchThree.Presenter {
	public class MainPresenter : MonoBehaviour, IInitializable {
		private BoardModel boardModel;
		private FillManager fillManager;

		// Dependencies
		private BoardPresenter boardPresenter;
		private MatchThreeInputManager inputManager;


		public void Initialize() {
			this.boardPresenter = MatchThreeContext.GetInstance().Get<BoardPresenter>();
			this.inputManager = MatchThreeContext.GetInstance().Get<MatchThreeInputManager>();

			Vector2Int boardSize = new Vector2Int(8, 8);
			this.boardModel = new BoardModel(boardSize);
			this.fillManager = new FillManager(boardModel);

			fillManager.FillEmptyCells();
		}

		public BoardModel GetBoardModel() { return boardModel; }
	}
}
