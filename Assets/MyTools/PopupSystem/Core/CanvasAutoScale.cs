using UnityEngine;
using UnityEngine.UI;

public class CanvasAutoScale : MonoBehaviour {
	public CanvasScaler scaler;
	public Canvas canvas;
	void Reset () {
		scaler = GetComponent<CanvasScaler> ();
		canvas = GetComponent<Canvas> ();
	}

	private void Awake () {
		canvas.renderMode = RenderMode.ScreenSpaceCamera;
		canvas.worldCamera = Camera.main;
		canvas.sortingLayerName = "UI";
		if (Screen.height * 1f / Screen.width > 2f)
			scaler.matchWidthOrHeight = 0;
	}
}
