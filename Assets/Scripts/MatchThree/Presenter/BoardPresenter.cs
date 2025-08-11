using MatchThree.Model;
using MatchThree.View;
using UnityEngine;
using Utilities.Contexts;

namespace MatchThree.Presenter {
	public class BoardPresenter : MonoBehaviour, IInitializable {
		private BoardModel boardModel;
		private BoardView boardView;

		// Dependencies
		private MainPresenter mainPresenter;
		private BoardViewFactory boardViewFactory;
		private CameraController cameraController;

		public void Initialize() {
			this.mainPresenter = MatchThreeContext.GetInstance().Get<MainPresenter>();
			this.boardViewFactory = MatchThreeContext.GetInstance().Get<BoardViewFactory>();
			this.cameraController = SceneContext.GetInstance().Get<CameraController>();

			this.boardModel = mainPresenter.GetBoardModel();
			this.boardView = boardViewFactory.CreateBoardView(boardModel, transform);

			int boardHeight = boardModel.GetBoardHeight();
			int boardWidth = boardModel.GetBoardWidth();
			Vector2 fittingRect = new Vector2(boardWidth, boardHeight);

			cameraController.PlayCameraPositionTween(boardView.transform.position);
			cameraController.PlayOrthoSizeTween(fittingRect);
		}
	}
}
