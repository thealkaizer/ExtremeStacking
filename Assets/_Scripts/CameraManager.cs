using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour {

	public float zoomedOutY, zoomedInY;
	public float zoomedOutOrthographicSize, zoomedInOrthographicSize;
	public float zoomInDuration, zoomOutDuration;

	public void ZoomIn() {
		Camera.main.DOOrthoSize(zoomedInOrthographicSize, zoomInDuration).SetEase(Ease.InQuad);
		Camera.main.transform.DOMoveY(zoomedInY, zoomInDuration).SetEase(Ease.InQuad);
	}

	public void ZoomOut() {
		Camera.main.DOOrthoSize(zoomedOutOrthographicSize, zoomOutDuration).SetEase(Ease.InQuad);
		Camera.main.transform.DOMoveY(zoomedOutY, zoomOutDuration).SetEase(Ease.InQuad);
	}
}
