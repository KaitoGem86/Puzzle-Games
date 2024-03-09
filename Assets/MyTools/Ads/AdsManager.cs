using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AdsManager : SingletonMonoBehaviour<AdsManager>
{
    [Header("---SDK ID---")]
    public string sdkID;
    [Header("---AD UNIT ID---")]
    public string bannerID; // Retrieve the ID from your account
    public string interstitialID;
    public string rewardID;
    int retryAttempt;

    public Action actionClose, actionClaim;

    private Action actionCloseBanner;
    private float adCloseInterDelayTime = 0f;

    private DateTime? lastTime = null;
    bool timeShowAds => lastTime == null ? true : (DateTime.Now - lastTime)?.TotalSeconds > 45 /* inter ad tần suất (s)*/;
    public bool isShowBanner;

    public override void Awake()
    {
        base.Awake();
        InitAds();
    }
    public int GetBannerHeight()
    {
        if (MaxSdkUtils.IsTablet())
        {
            return Mathf.RoundToInt(90 * Screen.dpi / 160);
        }
        else
        {
            if (Screen.height <= 400 * Mathf.RoundToInt(Screen.dpi / 160))
            {
                return 32 * Mathf.RoundToInt(Screen.dpi / 160);
            }
            else if (Screen.height <= 720 * Mathf.RoundToInt(Screen.dpi / 160))
            {
                return 42 * Mathf.RoundToInt(Screen.dpi / 160);
            }
            else
            {
                return 50 * Mathf.RoundToInt(Screen.dpi / 160);
            }
        }
    }

    private void InitAds()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads   
        };

        MaxSdk.SetSdkKey(sdkID);
        MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
        //   Debug.Log("Ads ");
        InitializeBannerAds();
        InitializeInterstitialAds();
        InitializeRewardedAds();
        InitializeAd_impression();

        // Load the first rewarded ad
    }
    #region InitBanner
    public void InitializeBannerAds()
    {
        // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
        // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
        MaxSdk.CreateBanner(bannerID, MaxSdkBase.BannerPosition.BottomCenter);

        // Set background or background color for banners to be fully functional
        //    MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, Color.black);

        MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
        MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdLoadFailedEvent;
        MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;
        MaxSdkCallbacks.Banner.OnAdExpandedEvent += OnBannerAdExpandedEvent;
        MaxSdkCallbacks.Banner.OnAdCollapsedEvent += OnBannerAdCollapsedEvent;

        var height = MaxSdk.GetBannerLayout(bannerID);
        //  height.position.y;
    }


    private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //GlobalEventManager.Instance.OnAds_Banner_times();
    }

    private void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo) { }

    private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {

    }

    private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnBannerAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnBannerAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //MaxSdk.HideBanner(adUnitId);
    }

    public void ShowBanner()
    {
        if (!GlobalSetting.NetWorkRequirements() || PlayerData.UserData.HighestLevel + 1 < AppConfig.Instance.BannerAdLevel) return;
        //MaxSdk.CreateBanner(bannerID, MaxSdkBase.BannerPosition.BottomCenter);
        MaxSdk.ShowBanner(bannerID);
        isShowBanner = true;
        // GlobalEventManager.Instance.AdBannerTimes();
    }

    #endregion

    #region InitInter
    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(interstitialID);
        //  Debug.Log("Init Intern");
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'     
        // Reset retry attempt    
        retryAttempt = 0;
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));

        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.Log("Inter run");
    }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        LoadInterstitial();
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //canShowOpenAd = false;
        FunctionCommon.DelayTime(adCloseInterDelayTime, (() =>
        {
            actionCloseBanner?.Invoke();
        }));
        // Interstitial ad is hidden. Pre-load the next ad.
        LoadInterstitial();
        lastTime = DateTime.Now;

        GlobalEventManager.OnCloseInterstitial();
    }

    public void ShowInterstitial(Action Close_CallBack = null, float nextActionDelay = 0f)
    {
        // Debug.LogError($"FirtTime: {lastTime}");
        // Debug.LogError((DateTime.Now - lastTime)?.TotalSeconds);
        //  Debug.LogError($"{ PlayerData.Instance.HighestLevel <= AppConfig.Instance.INITIAL_INTER_AD_LEVEL }, {timeShowAds}");
        if (!NetworkRequirement() /*|| !timeShowAds*/)
        {
            // Debug.LogError("Dont show ads");
            Close_CallBack?.Invoke();
            return;
        }
        if (MaxSdk.IsInterstitialReady(interstitialID))
        {
            adCloseInterDelayTime = nextActionDelay;
            actionCloseBanner = Close_CallBack;
            MaxSdk.ShowInterstitial(interstitialID);
        }
        else
        {
            Close_CallBack?.Invoke();
        }
        GlobalEventManager.OnShowInterstitial();
    }
    #endregion

    #region Init Rewarded
    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        LoadRewardedAd();
    }

    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(rewardID);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        retryAttempt = 0;
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));

        Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        LoadRewardedAd();
    }
    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {

    }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //  canShowOpenAd = false;
        // Rewarded ad is hidden. Pre-load the next ad
        //  Debug.Log("Rewarded close");
        actionClose?.Invoke();
        actionClose = null;
        LoadRewardedAd();
        lastTime = DateTime.Now;
        GlobalEventManager.OnRewardedComplete(BallSortQuest.GameManager.Instance.Level.level);
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        // The rewarded ad displayed and the user should receive the reward.
        // Debug.Log("Rewarded run");
        actionClaim?.Invoke();
        actionClaim = null;
        //Debug.LogError(adsLocation);
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Ad revenue paid. Use this callback to track user revenue.
    }

    bool rewardedVideoAvailability;
    string adsLocation;
    public void ShowRewardedAd(Action onClaim, Action onClose, Action onClick = null, string localtion = "")
    {
        if (MaxSdk.IsRewardedAdReady(rewardID))
        {
            adsLocation = localtion;
            MaxSdk.ShowRewardedAd(rewardID);
            actionClaim = onClaim;
            actionClose = onClose;

            GlobalEventManager.OnShowRewarded(BallSortQuest.GameManager.Instance.Level.level);
        }
        else
        {
            LoadRewardedAd();
            Debug.Log("Rewarded ad is not ready");
        }

        // GlobalEventManager.Instance.OnShowRewarded();
    }

    public static bool NetworkRequirement()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
    #endregion

    public void InitializeAd_impression()
    {
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
    }

    private void OnAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo impressionData)
    {
        //        double revenue = impressionData.Revenue;
        //        var impressionParameters = new[] {
        //        new Firebase.Analytics.Parameter("ad_platform", "AppLovin"),
        //        new Firebase.Analytics.Parameter("ad_source", impressionData.NetworkName),
        //        new Firebase.Analytics.Parameter("ad_unit_name", impressionData.AdUnitIdentifier),
        //        new Firebase.Analytics.Parameter("ad_format", impressionData.AdFormat),
        //        new Firebase.Analytics.Parameter("value", revenue),
        //        new Firebase.Analytics.Parameter("currency", "USD"), // All AppLovin revenue is sent in USD
        //};
        //        Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", impressionParameters);
    }
}

public class AdsEvent
{
    public delegate void NoParamEvent();

    public delegate void BoolEvent(bool result);

    public delegate void OneParamsEvent(object param1);

    public delegate void TwoParamsEvent(object param1, object param2);
}
