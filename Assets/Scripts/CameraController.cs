using UnityEngine;
using Utilities;
using Utilities.Contexts;
using Utilities.Tweens.CameraTweens;
using Utilities.Tweens.Easing;
using Utilities.Tweens.TransformTweens;

public class CameraController : MonoBehaviour, IInitializable {
	[SerializeField] private new Camera camera;
	[SerializeField] private Ease.Type easeType = Ease.Type.InOutQuad;

	public void Initialize() { }

	// Tween 
	public void PlayCameraPositionTween(Vector3 targetPoint, float cameraDistance = 12f) {
		Vector3 cameraPosition = GetCenteredCameraPosition(targetPoint, cameraDistance);
		PositionTween positionTween = camera.transform.PlayPosition(cameraPosition, 1f);
		positionTween.SetEase(easeType);
		positionTween.Play();
	}

	public void PlayOrthoSizeTween(Vector2 fittingRect, float margin = 1f) {
		float targetOrthoSize = GetFittingOrthographicSize(fittingRect, margin);

		OrthoSizeTween orthoSizeTween = camera.PlayOrthoSize(targetOrthoSize, 1f);
		orthoSizeTween.SetEase(easeType);
		orthoSizeTween.Play();
	}

	// Instant
	public void CenterCameraToGrid(Vector3 targetPoint, float cameraDistance = 12f) {
		Vector3 cameraPos = GetCenteredCameraPosition(targetPoint, cameraDistance);
		camera.transform.position = cameraPos;
	}


	public void FitProjectionSizeToGrid(Vector2 boardSizeInLength, float margin = 1f) {
		camera.orthographicSize = GetFittingOrthographicSize(boardSizeInLength, margin);
	}

	// Calculate
	private Vector3 GetCenteredCameraPosition(Vector3 targetPoint, float cameraDistance) {
		Vector3 cameraPos = targetPoint - Vector3.forward * cameraDistance;
		return cameraPos;
	}

	private float GetFittingOrthographicSize(Vector2 fittingRect, float margin) {
		float fittingWidth = fittingRect.x + 2 * margin;
		float fittingHeight = fittingRect.y + 2 * margin;

		// Redundant viewport calculations are left for future usages if needed
		float aspectRatio = camera.aspect;
		Vector2 viewportFittingWidth = new(fittingWidth, fittingWidth / aspectRatio);
		Vector2 viewportFittingHeight = new(fittingHeight * aspectRatio, fittingHeight);

		// Greater ortho size ensures grid is inbound horizontally and vertically
		float fittingOrthoSize = Mathf.Max(viewportFittingWidth.y / 2, viewportFittingHeight.y / 2);
		return fittingOrthoSize;
	}

	// Helper
	public Vector3 ScreenPositionToWorldSpace(Vector2 screenPosition) {
		return camera.ScreenToWorldPoint(screenPosition.WithZ(camera.nearClipPlane));
	}

	// Getter
	public Camera GetCamera() => camera;
}
