using System;
using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager
{

   public static Action<string, Parameter[]> EvtSendEvent;
   //  public Action<string> EvtUpdateUserProperties;

   #region Tracking Ads
       #region Tracking Ads
    public static void OnShowInterstitial()
    {
        EvtSendEvent?.Invoke("inter_show", null);
    }

    public static void OnCloseInterstitial()
    {
        EvtSendEvent?.Invoke("inter_close", null);
    }

    public static void OnShowRewarded(int level)
    {
        Parameter[] parameter = new Parameter[]
     {
             new Parameter("level", level.ToString()),
     };
        EvtSendEvent?.Invoke("reward_show", parameter);
    }

    public static void OnRewardedComplete(int level)
    {
        Parameter[] parameter = new Parameter[]
  {
             new Parameter("level", level.ToString()),
  };
        EvtSendEvent?.Invoke("reward_complete", parameter);
    }
    #endregion

    #region Tracking GamePlay
    public static int FirstPlayLevelTracking
    {
        get => PlayerPrefs.GetInt("first_play_level", 0);
        set => PlayerPrefs.SetInt("first_play_level", value);
    }

    public static void OnLevelPlay(int level)
    {
        if (FirstPlayLevelTracking == level) return;
        Parameter[] parameter = new Parameter[]
       {
             new Parameter("level", level.ToString()),
       };
        FirstPlayLevelTracking = level;
        EvtSendEvent?.Invoke($"level_play", parameter);
    }

    public static void OnLevelComplete(int level)
    {
        Parameter[] parameter = new Parameter[]
      {
             new Parameter("level", level.ToString()),
      };
        EvtSendEvent?.Invoke($"level_Win", parameter);
    }

    public static void OnLevelReplay(int level)
    {
        if (FirstPlayLevelTracking == level) return;
        Parameter[] parameter = new Parameter[]
      {
             new Parameter("level", level.ToString()),
      };
        EvtSendEvent?.Invoke($"level_replay", parameter);
    }
    #endregion
   #endregion

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
// }
}