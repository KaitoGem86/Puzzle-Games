//using Firebase;
//using Firebase.Analytics;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Firebase.Extensions;
//using Firebase.RemoteConfig;
//using UnityEngine;

//public class FirebaseManager : SingletonMonoBehaviour<FirebaseManager>
//{
//    private const string TAG = "[FIREBASE]";
//    public FirebaseApp app;   
//    public bool isOk = false;

//    public override void Awake()
//    {
//        Init();
//        GlobalEventManager.Instance.EvtSendEvent += SendEvent;
//      //  GlobalEventManager.Instance.EvtUpdateUserProperties += SetUserPropeties;
//    }

//    private void OnDestroy()
//    {
//        if (GlobalEventManager.Exists())
//        {
//            GlobalEventManager.Instance.EvtSendEvent -= SendEvent;
//          //  GlobalEventManager.Instance.EvtUpdateUserProperties -= SetUserPropeties;
//        }
//    }

//    public void Init()
//    {
//       Debug.Log($"{TAG} Init...");
//        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
//        {
//            var dependencyStatus = task.Result;
//            if (dependencyStatus == Firebase.DependencyStatus.Available)
//            {
//                app = FirebaseApp.DefaultInstance;
//                isOk = true;
//                FetchValue();
//                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

//                // Notifacation
//                //Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
//                //Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
//                Debug.Log($"{TAG} Init done!");
//            }
//            else
//            {
//               Debug.Log($"{TAG} Init fail!");
//            }
//        });
//    }

//    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
//    {
//        Debug.Log("Received Registration Token: " + token.Token);
//    }

//    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
//    {
//        Debug.Log("Received a new message from: " + e.Message.From);
//    }
//    private void SendEvent(string eName, Parameter[] parameters)
//    {
//        if (!isOk)
//            return;
//        if (parameters == null)
//        {
//            FirebaseAnalytics.LogEvent(eName);
//            //Debug.LogError($"{eName}");
//        }
//        else
//        {
//            FirebaseAnalytics.LogEvent(eName, parameters);
//          //  Debug.LogError($"{eName}_{parameters}");
//        }
//    }
//    private void SetUserPropeties()
//    {
//        if (!isOk) return;
//        //FirebaseAnalytics.LogEvent(eName);
//        //Debug.LogError(eName);
//    }

//    private void FetchValue()
//    {
//        SetDefaultValue();
//        TimeSpan time = new TimeSpan(0, 0, 10);
//        FirebaseRemoteConfig.DefaultInstance.FetchAsync(time).ContinueWithOnMainThread(task =>
//        {
//            var info = FirebaseRemoteConfig.DefaultInstance.Info;
//            if (info.LastFetchStatus == LastFetchStatus.Success)
//            {
//               Debug.Log($"{TAG}: Read remote config...");
//            }
//        });
//    }

//    private void SetDefaultValue()
//    {
        
//    }
//}