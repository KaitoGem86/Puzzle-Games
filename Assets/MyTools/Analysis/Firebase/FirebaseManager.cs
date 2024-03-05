using Firebase;
using Firebase.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEngine;
using Unity.VisualScripting;

public class FirebaseManager : SingletonMonoBehaviour<FirebaseManager>
{
    private const string TAG = "[FIREBASE]";
    public FirebaseApp app;
    public bool isOk = false;

    public override void Awake()
    {
    
        GlobalEventManager.Instance.EvtSendEvent += SendEvent;
        //GlobalEventManager.Instance.EvtUpdateUserProperties += SetUserPropeties;
    }

    private void Start(){
        Init();
    }

    private void OnDestroy()
    {
        if (GlobalEventManager.Exists())
        {
            GlobalEventManager.Instance.EvtSendEvent -= SendEvent;
            //  GlobalEventManager.Instance.EvtUpdateUserProperties -= SetUserPropeties;
        }
    }

    public void Init()
    {
        Debug.Log($"{TAG} Init...");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                app = FirebaseApp.DefaultInstance;
                isOk = true;
                FetchValue();
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

                // Notifacation
                //Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
                //Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
                Debug.Log($"{TAG} Init done!");
            }
            else
            {
                Debug.Log($"{TAG} Init fail!");
            }
        });
    }

    // public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    // {
    //     Debug.Log("Received Registration Token: " + token.Token);
    // }

    // public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    // {
    //     Debug.Log("Received a new message from: " + e.Message.From);
    // }
    private void SendEvent(string eName, Parameter[] parameters)
    {
        if (!isOk)
            return;
        if (parameters == null)
        {
            FirebaseAnalytics.LogEvent(eName);
            Debug.LogError($"{eName}");
        }
        else
        {
            FirebaseAnalytics.LogEvent(eName, parameters);
            Debug.LogError($"{eName}_{parameters}");
        }
    }

    #region  FIREBASE REMOTE CONFIG
    private void FetchValue()
    {
        TimeSpan time = new TimeSpan(0, 0, 10);
        FirebaseRemoteConfig.DefaultInstance.FetchAsync(time).ContinueWithOnMainThread(task =>
        {
            var info = FirebaseRemoteConfig.DefaultInstance.Info;
            if (info.LastFetchStatus == LastFetchStatus.Success)
            {
                Debug.Log($"{TAG}: Read remote config...");
                FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
                AppConfig.Instance.BannerAdLevel = GetFetchIntValue("banner_ad_level", AppConfig.Instance.BannerAdLevel);
                AppConfig.Instance.InterAdLevel = GetFetchIntValue("interstitial_ad_level", AppConfig.Instance.InterAdLevel);            }
                AppConfig.Instance.InterFrequencyTime = GetFetchIntValue("interstitial_frequency_time", AppConfig.Instance.InterFrequencyTime);
                AppConfig.Instance.IsShowInterWithClosePopupRemoveAds = GetFetchBoolValue("is_show_interstitial_with_close_popup_remove_ads", AppConfig.Instance.IsShowInterWithClosePopupRemoveAds);
        });
    }

    private int GetFetchIntValue(string valueName, int defaultValue)
    {
        string value = FirebaseRemoteConfig.DefaultInstance.GetValue(valueName).StringValue;
        if (value != null)
        {
            //Debug.Log(value);
            return int.Parse(value);
        }
        else
        {
            return defaultValue;
        }
    }

    private string GetFetchStringValue(string valueName, string defaultValue)
    {
        string value = FirebaseRemoteConfig.DefaultInstance.GetValue(valueName).StringValue;
        if (value != null)
        {
            // Debug.Log(value);
            return value;
        }
        else
        {
            return defaultValue;
        }
    }

    public bool GetFetchBoolValue(string valueName, bool defaultValue)
    {
        bool value = FirebaseRemoteConfig.DefaultInstance.GetValue(valueName).BooleanValue;

        if (value != defaultValue)
        {
            return value;
        }
        else
        {
            return defaultValue;
        }
    }
    #endregion
}