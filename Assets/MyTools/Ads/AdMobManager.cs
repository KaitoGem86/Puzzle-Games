//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds.Api;
//using System;

//public class AdMobManager : MonoBehaviour
//{
//    [Header("ANDROID")]
//    public string androidBannerID;
//    public string androidInterstitialID;
//    public string androidrewardID;
//    public string androiOpenAdsID;
//    [Header("IOS")]
//    public string iosBannerID;
//    public string iosInterstitialID;
//    public string iosRewardID;


//    private string bannerID;
//    private string interID;
//    private string rewardID;



//    private bool isBannerReady = false;
//    private bool isBannerShow = false;

//    private BannerView bannerView;
//    private InterstitialAd interstitialAd;
//    private RewardedAd rewardedAd;

//    private Action closeInter;
//    private Action closeVideo, closeVideoWithEarnReward;


//    private void Awake()
//    {
//#if UNITY_ANDROID
//        bannerID = androidBannerID;
//        interID = androidInterstitialID;
//        rewardID = androidrewardID;
//#elif UNITY_IOS
//        bannerID = iosBannerID;
//        interID = iosInterstitialID;
//        rewardID = iosRewardID;
//#else
//        bannerID = "unexpected_platform";
//        interID = "unexpected_platform";
//        rewardID = "unexpected_platform";
//#endif
//       // Init();
//    }

//    // Banner Methods //
//    public void ShowBanner()
//    {
//        if (bannerView == null || !isBannerReady)

//            RequestBanner();
//        if (!isBannerShow)
//        {
//            bannerView.Show();
//            isBannerShow = true;
//        }
//    }

//    public void HideBanner()
//    {
//        if (isBannerShow)
//        {
//            bannerView.Hide();
//            isBannerShow = false;
//        }

//    }

//    // private void ResetBanner()
//    // {
//    //     if (bannerView != null)
//    //     {
//    //         bannerView.Destroy();
//    //         isBannerReady = false;
//    //         isBannerShow = false;
//    //     }

//    // }


//    // Interstitial Methods //

//    public bool CheckInter()
//    {
//        if (interstitialAd == null)
//        {
//            LoadInter();
//            return false;
//        }
//        else
//        {
//            return interstitialAd.IsLoaded();
//        }
//    }

//    public void LoadInter()
//    {
//        if (!AdsManager.NetworkRequirement()) return;
//        if (interstitialAd == null) RequestInterstitial();

//        AdRequest request = new AdRequest.Builder().Build();
//        interstitialAd.LoadAd(request);
//    }

//    public void ShowInter(Action onClose)
//    {
//        if (CheckInter())
//        {
//            closeInter = onClose;
//            interstitialAd.Show();
//        }
//    }

//    // public void ResetInter()
//    // {
//    //     if (interstitialAd != null) interstitialAd.Destroy();
//    // }

//    // Video Rewarded Methods //
//    public bool CheckVideo()
//    {
//        if (rewardedAd == null)
//        {
//            LoadVideo();
//            return false;
//        }
//        else
//        {
//            return rewardedAd.IsLoaded();
//        }
//    }

//    public void LoadVideo()
//    {
//        if (!AdsManager.NetworkRequirement()) return;

//        if (rewardedAd == null) RequestVideoRewarded();

//        AdRequest request = new AdRequest.Builder().Build();
//        rewardedAd.LoadAd(request);
//    }

//    public void ShowVideo(Action onCompleteWithEarnReward, Action onClose)
//    {
//        if (CheckVideo())
//        {
//            closeVideo = onClose;
//            closeVideoWithEarnReward = onCompleteWithEarnReward;
//            rewardedAd.Show();
//        }
//    }

//    private void Init()
//    {
//        MobileAds.Initialize(initStatus => { });

//        LoadInter();
//        LoadVideo();
//    }

//    private void RequestBanner()
//    {
//        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);
//        AdRequest request = new AdRequest.Builder().Build();
//        bannerView.LoadAd(request);
//        // Called when an ad request has successfully loaded.
//        this.bannerView.OnAdLoaded += this.HandleOnAdBannerLoaded;
//        // Called when an ad request failed to load.
//        this.bannerView.OnAdFailedToLoad += this.HandleOnAdBannerFailedToLoad;
//        // Called when an ad is clicked.
//        this.bannerView.OnAdOpening += this.HandleOnAdBannerOpened;
//        // Called when the user returned from the app after an ad click.
//        this.bannerView.OnAdClosed += this.HandleOnAdBannerClosed;
//    }

//    private void RequestInterstitial()
//    {
//        interstitialAd = new InterstitialAd(interID);

//        // Called when an ad request has successfully loaded.
//        interstitialAd.OnAdLoaded += HandleOnAdInterLoaded;
//        // Called when an ad request failed to load.
//        interstitialAd.OnAdFailedToLoad += HandleOnAdInterFailedToLoad;
//        // Called when an ad is shown.
//        interstitialAd.OnAdOpening += HandleOnAdInterOpened;
//        // Called when the ad is closed.
//        interstitialAd.OnAdClosed += HandleOnAdInterClosed;
//    }

//    private void RequestVideoRewarded()
//    {
//        if (rewardedAd != null) rewardedAd.Destroy(); 
//        rewardedAd = new RewardedAd(rewardID);

//        // Called when an ad request has successfully loaded.
//        rewardedAd.OnAdLoaded += HandleRewardedAdVideoLoaded;
//        // Called when an ad request failed to load.
//        rewardedAd.OnAdFailedToLoad += HandleRewardedAdVideoFailedToLoad;
//        // Called when an ad is shown.
//        rewardedAd.OnAdOpening += HandleRewardedAdVideoOpening;
//        // Called when an ad request failed to show.
//        rewardedAd.OnAdFailedToShow += HandleRewardedAdVideoFailedToShow;
//        // Called when the user should be rewarded for interacting with the ad.
//        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
//        // Called when the ad is closed.
//        rewardedAd.OnAdClosed += HandleRewardedAdVideoClosed;
//    }

//    #region BANNER
//    private void HandleOnAdBannerLoaded(object sender, EventArgs args)
//    {
//        isBannerReady = true;
//        Debug.Log($"[BANNER] Banner Loaded!");
//    }

//    private void HandleOnAdBannerFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        isBannerReady = false;
//        Debug.Log($"[BANNER] Banner Failed To Load!");
//    }

//    private void HandleOnAdBannerOpened(object sender, EventArgs args) { }
//    private void HandleOnAdBannerClosed(object sender, EventArgs args) { }
//    #endregion

//    #region INTERSTITIAL
//    private void HandleOnAdInterLoaded(object sender, EventArgs args)
//    {
//        Debug.Log($"[INTERSTITIAL] Interstitial Loaded!");
//    }

//    private void HandleOnAdInterFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        Debug.Log($"[INTERSTITIAL] Interstitial Failed To Load: {args.LoadAdError.GetMessage()}");
//    }

//    private void HandleOnAdInterOpened(object sender, EventArgs args)
//    {
//        Debug.Log($"[INTERSTITIAL] Show Interstitial Ad!");
//    }

//    private void HandleOnAdInterClosed(object sender, EventArgs args)
//    {
//        closeInter?.Invoke();
//        closeInter = null;


//        LoadInter();
//    }

//    #endregion

//    #region VIDEO REWARDS
//    private void HandleRewardedAdVideoLoaded(object sender, EventArgs args)
//    {
//        Debug.Log($"[REWARD] Video Reward Loaded!");
//    }

//    private void HandleRewardedAdVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        Debug.Log($"[REWARD] Video Reward Failed To Load: {args.LoadAdError.GetMessage()}");
//    }

//    private void HandleRewardedAdVideoOpening(object sender, EventArgs args)
//    {
//        Debug.Log($"[REWARD] Show Video Reward Ad!");
//    }

//    private void HandleRewardedAdVideoFailedToShow(object sender, AdErrorEventArgs args)
//    {
//        Debug.Log($"[REWARD] Video Reward Failed To Show: {args.AdError.GetMessage()}");
//    }

//    private void HandleRewardedAdVideoClosed(object sender, EventArgs args)
//    {
//        Debug.Log($"[REWARD] Video Reward Closed!");
//        closeVideo?.Invoke();

//        LoadVideo();
//    }

//    public void HandleUserEarnedReward(object sender, Reward args)
//    {
//        Debug.Log($"[REWARD] User Earned Reward!");
//        closeVideoWithEarnReward?.Invoke();
//    }
//    #endregion
//}
