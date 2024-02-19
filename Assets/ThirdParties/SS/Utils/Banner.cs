// This code is part of the SS-Scene library, released by Anh Pham (anhpt.csit@gmail.com).

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace SS.Utils
{
    public class Banner
    {
#if UNITY_IOS
        [DllImport("__Internal")] private static extern float getSmartBannerHeight();

        [DllImport("__Internal")] private static extern float getSmartBannerHeightRatio();
#endif

        public static bool isSmartBanner
        {
            get;
            protected set;
        }

        public static float GetSafeInsetBottom()
        {
            return UnityEngine.Screen.safeArea.y;
        }

        public static float GetBannerHeight()
        {
#if UNITY_EDITOR
            isSmartBanner = true;
            return 100;
#elif UNITY_IOS
            isSmartBanner = true;
            return getSmartBannerHeight();
#else
            using (AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity"))
            using (AndroidJavaClass adSizeClass = new AndroidJavaClass("com.google.android.gms.ads.AdSize"))
            {
                if (adSizeClass != null)
                {
                    using (AndroidJavaObject smartBanner = adSizeClass.GetStatic<AndroidJavaObject>("SMART_BANNER"))
                    {
                        int smartBannerHeight = smartBanner.Call<int>("getHeightInPixels", activity);

                        float logicHeight = SS.Utils.Screen.GetLogicScreenHeight();
                        float requireHeight = 1036 + logicHeight / UnityEngine.Screen.height * smartBannerHeight;

                        if (requireHeight <= logicHeight)
                        {
                            int sdkInt = GetSdkInt();
                            if (sdkInt < 28)
                            {
                                isSmartBanner = false;
                            }
                            else
                            {
                                isSmartBanner = true;
                            }
                            return smartBannerHeight;
                        }
                        else
                        {
                            isSmartBanner = false;
                            using (AndroidJavaObject normalBanner = adSizeClass.GetStatic<AndroidJavaObject>("BANNER"))
                            {
                                int bannerHeight = normalBanner.Call<int>("getHeightInPixels", activity);
                                return bannerHeight;
                            }
                        }
                    }
                }
            }
            isSmartBanner = false;
            return 100;
#endif
        }

        public static float GetBannerHeightRatio()
        {
#if UNITY_EDITOR
            return GetBannerHeight() / UnityEngine.Screen.height;
#elif UNITY_IOS
            return getSmartBannerHeightRatio();
#else
            return GetBannerHeight() / UnityEngine.Screen.height;
#endif
        }

        public static int GetSdkInt()
        {
            using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
            {
                return version.GetStatic<int>("SDK_INT");
            }
        }
    }
}