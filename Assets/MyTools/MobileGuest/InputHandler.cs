using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    #region  Inspector Variabler
    #endregion

    #region Member Variables
    #endregion

    #region Unity Methods

    private void Start()
    {
        InputManager.Instance.OnTouch += HandleTouch;
        InputManager.Instance.OnTap += HandleTap;
        InputManager.Instance.OnStartDrag += HandleStartDrag;
        InputManager.Instance.OnFinishDrag += HandleEndDrag;
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnTouch -= HandleTouch;
        InputManager.Instance.OnTap -= HandleTap;
        InputManager.Instance.OnStartDrag -= HandleStartDrag;
        InputManager.Instance.OnFinishDrag -= HandleEndDrag;
    }

    #endregion

    #region Private Methods

    private void HandleTouch(Vector3 pos)
    {
        //Debug.Log($"Touch {pos}");
    }

    private void HandleTap(Vector3 pos)
    {
        //Debug.Log($"Tap {pos}");
    }

    private void HandleStartDrag(Vector3 pos)
    {
        //Debug.Log($"Start Drag {pos}");
    }

    private void HandleDrag(Vector3 pos)
    {
        //Debug.Log($"Drag {pos}");
    }

    private void HandleEndDrag(Vector3 pos)
    {
        //Debug.Log($"End Drag {pos}");
    }
    #endregion
}