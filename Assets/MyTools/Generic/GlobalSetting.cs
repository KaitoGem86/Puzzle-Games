using UnityEngine;

public class GlobalSetting : MonoBehaviour
{
    private void Awake()
    {
#if !UNITY_EDITOR
        Application.targetFrameRate = 60;
#endif
        DontDestroyOnLoad(gameObject);
    }

    public static bool NetWorkRequirements()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}