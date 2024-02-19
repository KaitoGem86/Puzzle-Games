using UnityEngine;
using SS.View;
using System.Collections;

public class DMain : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);

        Manager.LoadingSceneName = DLoadingController.SCENE_NAME;
        Manager.Load(DTopController.SCENE_NAME);
    }
}
