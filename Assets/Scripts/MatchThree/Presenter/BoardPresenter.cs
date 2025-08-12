using System.Collections.Generic;
using MatchThree.Model;
using MatchThree.View;
using UnityEngine;
using Utilities;
using Utilities.Collections.Generic;
using Utilities.Contexts;

namespace MatchThree.Presenter {
	public class BoardPresenter : MonoBehaviour, IInitializable {
		private BoardModel boardModel;
		private BoardView boardView;

		private readonly Dictionary<DropModel, DropView> dropViews = new();
		private readonly Dictionary<CellModel, CellView> cellViews = new();

		// Dependencies
		private MainPresenter mainPresenter;

		private BoardViewFactory boardViewFactory;
		private CellViewFactory cellViewFactory;
		private DropViewFactory dropViewFactory;

		private CameraController cameraController;

		public void Initialize() {
			this.mainPresenter = MatchThreeContext.GetInstance().Get<MainPresenter>();
			this.boardViewFactory = MatchThreeContext.GetInstance().Get<BoardViewFactory>();
			this.cellViewFactory = MatchThreeContext.GetInstance().Get<CellViewFactory>();
			this.dropViewFactory = MatchThreeContext.GetInstance().Get<DropViewFactory>();
			this.cameraController = SceneContext.GetInstance().Get<CameraController>();

			this.boardModel = mainPresenter.GetBoardModel();
			this.boardView = boardViewFactory.CreateBoardView(boardModel, transform);
			this.boardView.transform.position = new Vector3(boardModel.GetWidth()-1, boardModel.GetHeight()-1, 0) / 2f;

			SpawnCellViews();
			SpawnDropViews();

			int boardHeight = boardModel.GetHeight();
			int boardWidth = boardModel.GetWidth();
			Vector2 fittingRect = new Vector2(boardWidth, boardHeight);

			cameraController.PlayCameraPositionTween(boardView.transform.position);
			cameraController.PlayOrthoSizeTween(fittingRect);
			
			
			MatchManager matchManager = new MatchManager(boardModel);
			List<MatchModel> matchModels = matchManager.FindMatches();
			CellModel[,] cellModels = boardModel.GetCellModels();

			for (int i = 0; i < matchModels.Count; i++) {
				MatchModel matchModel = matchModels[i];
				IReadOnlyList<Vector2Int> cellIndices = matchModel.GetCellIndices().AsReadOnly();
				for (int j = 0; j < cellIndices.Count; j++) {
					Vector2Int cellIndex = cellIndices[j];
					CellModel cellModel = cellModels[cellIndex.x, cellIndex.y];
					DropModel dropModel = cellModel.GetDropModel();
					
					dropViews[dropModel].transform.localScale = Vector3.one * .5f;
				}
			}
		}

		private void ClearInitialMatches() {
			
		}

		private void SpawnCellViews() {
			int boardHeight = boardModel.GetHeight();
			int boardWidth = boardModel.GetWidth();
			CellModel[,] cellModels = boardModel.GetCellModels();

			for (int y = 0; y < boardHeight; y++) {
				for (int x = 0; x < boardWidth; x++) {
					Vector2Int cellIndex = new Vector2Int(x, y);
					CellModel cellModel = cellModels[x, y];
					CellView cellView = cellViewFactory.CreateCellView(cellIndex, transform);
					cellView.transform.position = new Vector3(x, y, 0);

					cellViews.Add(cellModel, cellView);
				}
			}
		}

		private void SpawnDropViews() {
			int boardHeight = boardModel.GetHeight();
			int boardWidth = boardModel.GetWidth();
			CellModel[,] cellModels = boardModel.GetCellModels();

			for (int y = 0; y < boardHeight; y++) {
				for (int x = 0; x < boardWidth; x++) {
					CellModel cellModel = cellModels[x, y];
					if (cellModel.IsEmpty())
						continue;

					DropModel dropModel = cellModel.GetDropModel();
					DropView dropView = dropViewFactory.CreateDropView(dropModel, transform);
					dropView.transform.position = new Vector3(x, y, 0);

					dropViews.Add(dropModel, dropView);
				}
			}
		}
	}
}
