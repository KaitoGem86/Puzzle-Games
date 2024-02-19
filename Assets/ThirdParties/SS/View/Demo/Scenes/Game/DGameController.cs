using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class DGameController : Controller
{
    public const string SCENE_NAME = "DGame";

    public override string SceneName()
    {
        return SCENE_NAME;
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

    public void OnButtonTap()
    {
        StartCoroutine(LoadingToTop());
    }

    IEnumerator LoadingToTop()
    {
        Manager.LoadingAnimation(true);

        yield return new WaitForSeconds(1f);

        Manager.LoadingAnimation(false);

        Manager.Load(DTopController.SCENE_NAME);
    }
}