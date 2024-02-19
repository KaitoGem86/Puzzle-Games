// This code is part of the SS-Scene library, released by Anh Pham (anhpt.csit@gmail.com).

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.Utils
{
    public class Screen
    {
        const float STANDARD_SCREEN_RATIO = 0.5625f;

        public static float GetCanvasScalerMatch()
        {
            float ratio = (float)UnityEngine.Screen.width / UnityEngine.Screen.height;
            return (ratio >= STANDARD_SCREEN_RATIO) ? 1 : 0;
        }

        public static float GetLogicScreenHeight()
        {
            float ratio = (float)UnityEngine.Screen.width / UnityEngine.Screen.height;
            return (ratio >= STANDARD_SCREEN_RATIO) ? 1136f : (640f / ratio);
        }

        public static int GetSdkInt()
        {
            using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
            {
                return version.GetStatic<int>("SDK_INT");
            }
        }

        public static void SetFullScreen()
        {
            #if !UNITY_EDITOR && UNITY_ANDROID
            int sdkInt = GetSdkInt();

            if (sdkInt >= 28)
            {
                using (AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                        {
                            using (AndroidJavaClass playerClass2 = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                            using (AndroidJavaObject activity2 = playerClass2.GetStatic<AndroidJavaObject>("currentActivity"))
                            using (AndroidJavaObject window = activity2.Call<AndroidJavaObject>("getWindow"))
                            using (AndroidJavaObject attributes = window.Call<AndroidJavaObject>("getAttributes"))
                            {
                                Debug.Log("layoutInDisplayCutoutMode: " + attributes.Get<int>("layoutInDisplayCutoutMode"));
                                attributes.Set<int>("layoutInDisplayCutoutMode", 1);
                                window.Call("setAttributes", attributes);
                            }
                        }));
                }
            }
            #endif
        }
    }
}