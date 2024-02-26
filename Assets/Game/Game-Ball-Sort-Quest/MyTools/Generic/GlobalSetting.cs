using UnityEngine;

namespace BallSortQuest
{
    public class GlobalSetting : MonoBehaviour
    {
        private void Awake()
        {
#if !UNITY_EDITOR
        Application.targetFrameRate = 60;
#endif
            DontDestroyOnLoad(gameObject);
        }
        public void Update()
        {

        }

        public static bool NetWorkRequirements()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }

        public static AudioClip GetSFX(string audioName)
        {
            // Debug.Log(Resources.Load<AudioClip>("SFX/" + audioName));
            return Resources.Load<AudioClip>("SFX/" + audioName);
        }
    }
}