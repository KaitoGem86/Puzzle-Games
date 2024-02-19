using System.Collections.Generic;
using PopupSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager: MonoBehaviour {

	#region Instance

	private static InputManager instance;

	public static InputManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<InputManager>();
			}
			return instance;
		}
	}

	public static bool Exist => instance != null;
	#endregion
	
	#region Inspector Variables
	public Camera myCamera;
	public GraphicRaycaster[] gr;
	//if the distance between finger's touch point and finger's current point is bigger than this value, the touch is considered as a drag
	public float dragThreshold;
	#endregion

	#region Member Variables
	public enum INPUT_STATE {
		FREE,
		// no input
		TOUCHING,
		DRAG
	}
	public INPUT_STATE inputState;
	float dragThresholdSqr;
	Vector3 mouseDownPos;
	#endregion

	#region Events
	public delegate void InputEvent2 (Vector3 position);
	public event InputEvent2 OnStartDrag;
	public event InputEvent2 OnTouch;
	public event InputEvent2 OnTap;
	public event InputEvent2 OnFinishDrag;
	public event InputEvent2 OnDrag;
	#endregion
	
	void Awake ()
	{
		instance = this;
		dragThresholdSqr = dragThreshold * dragThreshold;
	}

	void Update () {
		if (!PopupManager.Instance.hasPopupShowing ) {
			if (inputState == INPUT_STATE.FREE) {
				if (Input.GetMouseButtonDown(0)) {
					PointerEventData ped = new PointerEventData(null);
					ped.position = Input.mousePosition;
					List<RaycastResult> results = new List<RaycastResult>();
					for (int i = 0; gr!=null && i < gr.Length; i++) {
						gr[i].Raycast(ped, results);
						if (results.Count > 0)
							break;
					}
					if (results.Count > 0) {
						mouseDownPos = myCamera.ScreenToWorldPoint(Input.mousePosition);
						inputState = INPUT_STATE.TOUCHING;
                        OnTouch?.Invoke(myCamera.ScreenToWorldPoint(Input.mousePosition));
                    }
				}
			} else if (inputState == INPUT_STATE.TOUCHING) {
				Vector3 curPos = myCamera.ScreenToWorldPoint(Input.mousePosition);
				if (Vector3.SqrMagnitude(curPos - mouseDownPos) > dragThresholdSqr) {
					inputState = INPUT_STATE.DRAG;
                    OnStartDrag?.Invoke(myCamera.ScreenToWorldPoint(Input.mousePosition));
                }
			} else if (inputState == INPUT_STATE.DRAG)
			{
				OnDrag?.Invoke((myCamera.ScreenToWorldPoint(Input.mousePosition)));
			}

			if (Input.GetMouseButtonUp(0)) {
				if (inputState == INPUT_STATE.TOUCHING) {
                    OnTap?.Invoke(myCamera.ScreenToWorldPoint(Input.mousePosition));
                } else if (inputState == INPUT_STATE.DRAG) {
                    OnFinishDrag?.Invoke(myCamera.ScreenToWorldPoint(Input.mousePosition));
                }
				inputState = INPUT_STATE.FREE;
			}
		}
	}

	void OnDestroy () {
		OnTouch = null;
		OnStartDrag = null;
		OnFinishDrag = null;
		OnTap = null;
		OnDrag = null;
	}
}