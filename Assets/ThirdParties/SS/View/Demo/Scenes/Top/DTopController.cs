using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class DTopController : Controller
{
    public const string SCENE_NAME = "DTop";

    public override string SceneName()
    {
        return SCENE_NAME;
    }

    public void OnButtonTap()
    {
        Manager.Add(DPopupController.SCENE_NAME, "Popup1");
        Manager.Add(DPopupController.SCENE_NAME, "Popup2");
    }

    public void OnSelectTap()
    {
        Manager.Add(DSelectController.DSELECT_SCENE_NAME);
    }

    public override void OnActive(object data)
    {
        Debug.Log(SceneName() + " OnActive");
    }

    public override void OnShown()
    {
        Debug.Log(SceneName() + " OnShown");
    }

    public override void OnHidden()
    {
        Debug.Log(SceneName() + " OnHidden");
    }

    public override void OnReFocus()
    {
        Debug.Log(SceneName() + " OnReFocus");
    }
}
