//using System;
//using Firebase.Analytics;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GlobalEventManager : SingletonMonoBehaviour<GlobalEventManager>
//{

//    public Action<string, Parameter[]> EvtSendEvent;
//    //  public Action<string> EvtUpdateUserProperties;

//    #region Tracking Ads
//    public static int interAdCount
//    {
//        get => PlayerPrefs.GetInt("INTER_AD_COUNT", 0);
//        set => PlayerPrefs.SetInt("INTER_AD_COUNT", value);
//    }

//    public static int rewardedAdCount
//    {
//        get => PlayerPrefs.GetInt("REWARDED_AD_COUNT", 0);
//        set => PlayerPrefs.SetInt("REWARED_AD_COUNT", value);
//    }

//    #region tracking Banner
//    public int bannerAdCount
//    {
//        get => PlayerPrefs.GetInt("BANNER_AD_COUNT", 0);
//        set => PlayerPrefs.SetInt("BANNER_AD_COUNT", value);
//    }

//    public void AdBannerTimes()
//    {
//        bannerAdCount += 1;
//        Parameter[] parameter = new Parameter[]
//        {
//             new Parameter("amount", bannerAdCount.ToString()),
//        };
//        FirebaseAnalytics.LogEvent("ads_banner_times", parameter);
//    }
//    #endregion

//    public void AdIntertitialTimes()
//    {
//        interAdCount += 1;
//        Parameter[] parameter = new Parameter[]
//        {
//             new Parameter("amount", interAdCount.ToString()),
//        };
//        FirebaseAnalytics.LogEvent("ads_interstitial_times", parameter);
//    }

//    public void AdRewardedTimes()
//    {
//        rewardedAdCount += 1;
//        Parameter[] parameter = new Parameter[]
//        {
//             new Parameter("amount", rewardedAdCount.ToString()),
//        };
//        FirebaseAnalytics.LogEvent("ads_rewarded_times", parameter);
//    }

//    public void OnShowInterstitial()
//    {
//        FirebaseAnalytics.LogEvent("ads_interstitial_show");
//    }

//    public void OnCloseInterstitial()
//    {
//        FirebaseAnalytics.LogEvent("ads_interstitial_closed");
//    }

//    public void OnShowRewarded()
//    {
//        FirebaseAnalytics.LogEvent("ads_rewarded_show");
//    }


//    public void OnRewardedComplete()
//    {
//        FirebaseAnalytics.LogEvent("ads_rewarded_completed");
//    }
//    #endregion

//    #region Tracking GamePlay
//    public void OnLevelPlay(int _level)
//    //{
//        Parameter[] parameter = new Parameter[]
//       {
//             new Parameter("level", _level.ToString()),
//       };
//        EvtSendEvent?.Invoke($"level_play", parameter);
//    }

//    public void OnLevelComplete(int _level)
//    {
//        Parameter[] parameter = new Parameter[]
//      {
//             new Parameter("level", _level.ToString()),
//      };
//        EvtSendEvent?.Invoke($"level_Win", null);
//    }

//    public void OnLevelReplay(int _level)
//    {
//        Parameter[] parameter = new Parameter[]
//      {
//             new Parameter("level", _level.ToString()),
//      };
//        EvtSendEvent?.Invoke($"level_lose", null);
//    }

//    public void ChapterOnComplete(int _chapterIndex)
//    {
//        EvtSendEvent?.Invoke($"chapter_{_chapterIndex}_completed", null);
//    }
//    #endregion
//}
