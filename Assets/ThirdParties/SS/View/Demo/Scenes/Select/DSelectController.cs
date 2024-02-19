using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class DSelectController : Controller
{
    public const string DSELECT_SCENE_NAME = "DSelect";

    public override string SceneName()
    {
        return DSELECT_SCENE_NAME;
    }

    public void OnGameButtonTap()
    {
        Manager.Load(DGameController.SCENE_NAME);
    }

    public override void OnActive(object data = null)
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
}