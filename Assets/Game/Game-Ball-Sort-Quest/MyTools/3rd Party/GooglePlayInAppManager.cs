//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//#if UNITY_ANDROID
//using Google.Play.Review;
//using Google.Play.AppUpdate;
//using Google.Play.Common;
//#endif

//public class GooglePlayInAppManager : SingletonMonoBehaviour<GooglePlayInAppManager>
//{
//#if UNITY_ANDROID
//    #region IN-APP REVIEW
//    private ReviewManager reviewManager;
//    private PlayReviewInfo playReviewInfo;

//    public void Review()
//    {
//       // Debug.Log("[IN-APP REVIEW]: Review");
//        StartCoroutine(RequestReviews());
//    }

//    public void CheckAppUpdate()
//    {
//      //  Debug.Log("[IN-APP UPDATE]: Checking...");
//        StartCoroutine(RequestUpdate());
//    }

//    private IEnumerator RequestReviews()
//    {
//        reviewManager = new ReviewManager();

//        var requestFlowOperation = reviewManager.RequestReviewFlow();
//        yield return requestFlowOperation;

//        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
//        {
//          //  Debug.LogWarning($"[IN-APP REVIEW] Request Error: {requestFlowOperation.Error}!"); ;
//            yield break;
//        }

//        playReviewInfo = requestFlowOperation.GetResult();

//        var launchFlowOperation = reviewManager.LaunchReviewFlow(playReviewInfo);
//        yield return launchFlowOperation;

//        playReviewInfo = null;

//        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
//        {
//           // Debug.LogWarning($"[IN-APP REVIEW] Launch Error: {launchFlowOperation.Error}!"); ;
//            yield break;
//        }
//    }
//    #endregion

//    #region IN-APP UPDATE
//    private AppUpdateManager appUpdateManager;
//    private IEnumerator RequestUpdate()
//    {
//        appUpdateManager = new AppUpdateManager();
//        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation = appUpdateManager.GetAppUpdateInfo();
//        yield return appUpdateInfoOperation;

//      //  Debug.LogError($"[IN-APP UPDATE] Error: {appUpdateInfoOperation.Error}");
//        if (appUpdateInfoOperation.IsSuccessful)
//        {
//            AppUpdateInfo appUpdateInfoResult = appUpdateInfoOperation.GetResult();

//            Debug.LogWarning($"[IN-APP UPDATE] Update: {appUpdateInfoResult.UpdateAvailability}");
//            Debug.LogWarning($"[IN-APP UPDATE] Status: {appUpdateInfoResult.AppUpdateStatus}");
//            Debug.LogWarning($"[IN-APP UPDATE] Version: {appUpdateInfoResult.AvailableVersionCode}");
//            Debug.LogWarning($"[IN-APP UPDATE] Priority: {appUpdateInfoResult.UpdatePriority}");
//            Debug.LogWarning($"[IN-APP UPDATE] Download size: {appUpdateInfoResult.TotalBytesToDownload}");

//            var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();

//            StartCoroutine(StartImmediateUpdate(appUpdateInfoResult, appUpdateOptions));
//        }
//    }

//    private IEnumerator StartImmediateUpdate(AppUpdateInfo appUpdateInfo, AppUpdateOptions appUpdateOptions)
//    {
//        var startUpdateRequest = appUpdateManager.StartUpdate(
//            appUpdateInfo,
//            appUpdateOptions
//        );
//        yield return startUpdateRequest;

//       // Debug.Log($"[IN-APP UPDATE] Status: Done!");
//    }
//    #endregion

//    public override void Awake()
//    {
//        CheckAppUpdate();
//    }
//#endif
//}
