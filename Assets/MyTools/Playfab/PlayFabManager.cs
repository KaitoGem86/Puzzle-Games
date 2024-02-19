using Newtonsoft.Json;
//using PlayFab;
//using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayFabManager : SingletonMonoBehaviour<PlayFabManager>
{
    #region Private Varible
    [Header("DATA LEADERBOARD TOTAL")]
    [SerializeField] private int myPlace;
    [SerializeField] private string myID;
    [SerializeField] private string myName;
    [SerializeField] private string myScore;
    [Header("DATA LEADERBOARD WEEKLY")]
    [Space(10)]
    [SerializeField] private int _myPlaceWeeky;
    [SerializeField] private int _myPlaceFriends;
    [SerializeField] private string _myScoreWeekly;
    [Header("DATA")]
    [Space(10)]
    private bool isLogin;

    private bool isConnectInternet => AdsManager.NetworkRequirement();
    //   private bool isLoginFaceComplete => FacebookLoginManager.Instance.isLoginComple;
    #endregion

    #region Public Varible
    public static bool isLoadDataLeaderBoard = true;
    public bool IsLogin { get => isLogin; set => isLogin = value; }
    public int MyPlace { get => myPlace; set => myPlace = value; }
    public string MyID { get => myID; set => myID = value; }
    public string MyScore { get => myScore; set => myScore = value; }
    public string MyName { get => myName; set => myName = value; }
    public int MyPlaceWeeky { get => _myPlaceWeeky; set => _myPlaceWeeky = value; }
    public int MyPlaceFriends { get => _myPlaceFriends; set => _myPlaceFriends = value; }
    public string MyScoreWeekly { get => _myScoreWeekly; set => _myScoreWeekly = value; }


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError($"PlayFabManager.Start");
        StartCoroutine(DetectUseConnectInternet());
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            //  GetAppearance();
        }
    }
#endif

    private IEnumerator DetectUseConnectInternet()
    {
        while (!isConnectInternet)
        {
            yield return new WaitForSecondsRealtime(1f);
        }
        //  Login();
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    #region login PlayFab
    //public void Login()
    //{
    //    if (AdsManager.NetworkRequirement() /*&& !FacebookLoginManager.isLogin*/)
    //    {
    //        var request = new LoginWithCustomIDRequest
    //        {
    //            CustomId = SystemInfo.deviceUniqueIdentifier,
    //            CreateAccount = true,

    //            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
    //            {
    //                GetPlayerProfile = true
    //            }
    //        };
    //        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    //    }
    //}

    //void OnSuccess(LoginResult result)
    //{
    //    Debug.Log("Login Success");
    //    this.IsLogin = true;
    //    MyID = result.PlayFabId;
    //    string name = null;
    //    if (result.InfoResultPayload.PlayerProfile != null)
    //        name = result.InfoResultPayload.PlayerProfile.DisplayName;
    //}
    #endregion

}
