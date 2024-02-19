using UnityEngine;
using UnityEngine.UI;

public class CameraAuto : MonoBehaviour {

	public Camera cameraGameplay;

	public float defaultOrthoSize = 5;
    public float referHeight = 1024;
    public float referWidth = 512;    
    public float ratio = 0.5f, ratio1 =0.5f;

    public static Rect gameView;
    void Reset () {
		cameraGameplay = (Camera)GetComponent (typeof (Camera));
	}

	void Awake () {
        if(cameraGameplay==null)
            cameraGameplay = (Camera)GetComponent(typeof(Camera));
        float unitWidth = referWidth * defaultOrthoSize / referHeight;
        ratio = referWidth / referHeight;
        ratio1 = (float)Screen.width/ (float)Screen.height;
        gameView.xMin = -unitWidth;
		gameView.xMax = unitWidth;
		gameView.yMin = -cameraGameplay.orthographicSize;
		gameView.yMax = cameraGameplay.orthographicSize;
        if (ratio1 < 0.5f)
        {
            float h = ratio1 / ratio;
            float y = (1 - h) / 2;
            cameraGameplay.rect =new Rect(0,y,1,h);
        }
	}
}
