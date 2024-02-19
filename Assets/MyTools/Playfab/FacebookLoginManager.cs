//// Import statements introduce all the necessary classes for this example.
//using Facebook.Unity;
//using PlayFab;
//using PlayFab.ClientModels;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using LoginResult = PlayFab.ClientModels.LoginResult;


//public class FacebookLoginManager : SingletonMonoBehaviour<FacebookLoginManager>
//{

//    #region Unity Methods

//    public Action OnFaceBookLoginComplete;
//    // holds the latest message to be displayed on the screen
//    private string _message;
//    public string ID, userName;
//    public Sprite spriteAvatar;
//    public bool isLoginComple;
//    private bool _isLoginFaceFirst;
//    public static bool isLogin => FB.IsLoggedIn;
//    public int location;

//    public void Start()
//    {
//        // This call is required before any other calls to the Facebook API. We pass in the callback to be invoked once initialization is finished
//        if (!FB.IsInitialized)
//        {
//            FB.Init(() =>
//            {
//                //  Debug.LogError("Init complete");
//                if (FB.IsInitialized)
//                {
//                    FB.ActivateApp();
//                    //   Debug.LogError("Facebook Activate App");
//                    InitUserFB();
//                }
//                else
//                {
//                    //   Debug.LogError("Couldn't Initialized Facebook!");
//                    //  FB.Init();
//                }
//            });
//        }

//        //if (!AdsManager.NetworkRequirement())
//        //{
//        //    PlayerData.Instance.IsLoginFaceFirst = false;
//        //}
//    }
//    #endregion

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------
//    public void OnclickLoginFaceBook()
//    {
//        ///if (!FB.IsLoggedIn)
//        OnFacebookInitialized();
//    }

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------
//    private void OnFacebookInitialized()
//    {

//        // SetMessage("Logging into Facebook...");

//        // Once Facebook SDK is initialized, if we are logged in, we log out to demonstrate the entire authentication cycle.
//        //if (FB.IsLoggedIn)
//        //{          
//        //    FB.LogOut();
//        //}

//        // We invoke basic login procedure and pass in the callback to process the result
//        FB.LogInWithReadPermissions(new List<string>() { "public_profile" }, OnFacebookLoggedIn);
//    }

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------
//    private void OnFacebookLoggedIn(ILoginResult result)
//    {
//        // If result has no errors, it means we have authenticated in Facebook successfully
//        FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, FacebookProfilePhotoCallback);
//        FB.API("/me?fields=name", HttpMethod.GET,
//            (result) =>
//            {

//                if (result.Error == null)
//                {
//                    //fbName.text = result.ResultDictionary["name"].ToString();
//                    userName = result.ResultDictionary["name"].ToString();
//                    foreach (var item in result.ResultDictionary)
//                    {
//                        Debug.Log(item.Key + " - " + item.Value);

//                    }

//                    string oldNameInProfile = StringHelp.GetNameInGame(ProfilePlayerManager.Instance.userProfile.playerName);
//                    string newNameInProfile = ProfilePlayerManager.Instance.userProfile.playerName.Replace(oldNameInProfile, result.ResultDictionary["name"].ToString());
//                    ProfilePlayerManager.Instance.userProfile.SetPlayerName(newNameInProfile);
//                    //Save In PlayFAbID cua may
//                    PlayFabManager.Instance.SaveMyDisplayName();
//                }
//                else
//                {
//                    // Debug.Log("FACEBOOK error: " + result.Error);

//                }

//            });

//        StartCoroutine(LoginFB(result));

//    }

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------
//    IEnumerator LoginFB(ILoginResult result)
//    {
//        int cd = 0;
//        while (string.IsNullOrEmpty(userName) || cd <= 10)
//        {
//            cd++;
//            yield return new WaitForSecondsRealtime(0.1f);

//        }

//        if (result == null || string.IsNullOrEmpty(result.Error))
//        {
//            // SetMessage("Facebook Auth Complete! Access Token: " + AccessToken.CurrentAccessToken.TokenString + "\nLogging into PlayFab...");

//            /*
//             * We proceed with making a call to PlayFab API. We pass in current Facebook AccessToken and let it create
//             * and account using CreateAccount flag set to true. We also pass the callback for Success and Failure results
//             */
//            PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest
//            {

//                CreateAccount = true,
//                AccessToken = AccessToken.CurrentAccessToken.TokenString,
//                TitleId = PlayFabSettings.TitleId,
//                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
//                {
//                    GetPlayerProfile = true,
//                    GetUserAccountInfo = true,
//                    GetTitleData = true,
//                    GetPlayerStatistics = true,

//                    // GetUserVirtualCurrency = true,
//                    ProfileConstraints = new PlayerProfileViewConstraints()
//                    {
//                        //  ShowAvatarUrl = true,
//                        ShowDisplayName = true,
//                        //     ShowLocations = true,
//                        //   ShowLinkedAccounts = true,
//                    }
//                }
//            },
//                OnPlayfabFacebookAuthCompleteChangeProfile, OnPlayfabFacebookAuthFailed);
//        }
//        else
//        {
//            // If Facebook authentication failed, we stop the cycle with the message
//            SetMessage("Facebook Auth Failed: " + result.Error + "\n" + result.RawResult, true);
//        }
//    }

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------
//    // When processing both results, we just set the message, explaining what's going on.
//    private void OnPlayfabFacebookAuthCompleteChangeProfile(LoginResult result)
//    {
//        isLoginComple = true;
//        PlayFabManager.Instance.MyID = result.PlayFabId;
//        ProfilePlayerManager.Instance.userProfile.SetPlayFabID(result.PlayFabId);
//        //PlayFabManager.Instance.MyName = userName; // đã cho phép sửa tên bằng PlayFab thì bỏ 

//        //Save In PlayFAbID cua FaceBoook
//        PlayFabManager.Instance.SaveMyDisplayName();

//        OnFaceBookLoginComplete?.Invoke();
//        if (result.InfoResultPayload.PlayerProfile != null)
//        {
//            if (_isLoginFaceFirst) return;
//            PlayFabManager.Instance.GetDataPlayFab();
//            PopupAcount.Instance.Show();
//            _isLoginFaceFirst = true;
//        }
//        else
//        {
//            PlayFabManager.Instance.DontAcceptGetData();
//        }
//        PlayfabProfileCallback(result);
//    }
//    private void OnPlayfabFacebookAuthComplete(LoginResult result)
//    {
//        isLoginComple = true;
//        PlayFabManager.Instance.MyID = result.PlayFabId;
//        //  PlayFabManager.Instance.MyName = userName; // đã cho phép sửa tên bằng PlayFab thì bỏ 
//        OnFaceBookLoginComplete?.Invoke();
//        if (result.InfoResultPayload.PlayerProfile != null)
//        {
//            if (_isLoginFaceFirst) return;
//            PlayFabManager.Instance.GetDataPlayFab();
//            PopupAcount.Instance.Show();
//            _isLoginFaceFirst = true;
//        }
//        else
//        {
//            PlayFabManager.Instance.DontAcceptGetData();
//        }
//        PlayfabProfileCallback(result);
//    }

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------
//    private void OnPlayfabFacebookAuthFailed(PlayFabError error)
//    {
//        //  SetMessage("PlayFab Facebook Auth Failed: " + error.GenerateErrorReport(), true);
//        //  Debug.LogError("Faild");
//        FB.LogOut();
//        isLoginComple = false;
//        OnFaceBookLoginComplete?.Invoke();
//    }

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------
//    public void SetMessage(string message, bool error = false)
//    {
//        _message = message;
//        if (error)
//        {
//            Debug.LogError(_message);
//        }
//        else
//        {
//            Debug.Log(_message);
//        }
//    }

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------
//    private void InitUserFB()
//    {
//        CheckLogin();
//    }

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------
//    private void CheckLogin()
//    {
//        if (FB.IsLoggedIn)
//        {
//            _isLoginFaceFirst = true;
//            //ShowBtnLogin(false);
//            // Debug.LogError("Check Auto Login");
//            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, FacebookProfilePhotoCallback);
//            FB.API("/me?fields=name", HttpMethod.GET, FacebookProfileCallback);

//            PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest
//            {
//                CreateAccount = true,
//                AccessToken = AccessToken.CurrentAccessToken.TokenString,
//                TitleId = PlayFabSettings.TitleId,
//                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
//                {
//                    GetPlayerProfile = true,
//                    GetUserAccountInfo = true,
//                    GetTitleData = true,
//                    GetPlayerStatistics = true,

//                    // GetUserVirtualCurrency = true,
//                    ProfileConstraints = new PlayerProfileViewConstraints()
//                    {
//                        //  ShowAvatarUrl = true,
//                        ShowDisplayName = true,
//                        ShowLocations = true,
//                        //  ShowLinkedAccounts = true,
//                    }
//                }

//            },
//                OnPlayfabFacebookAuthComplete, OnPlayfabFacebookAuthFailed);
//        }
//    }

//    public void LogOut()
//    {
//        if (FB.IsLoggedIn)
//        {
//            PlayFabClientAPI.ForgetAllCredentials();
//            FB.LogOut();
//        }
//    }

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------
//    private void FacebookProfileCallback(IGraphResult result)
//    {
//        if (result.Error == null)
//        {
//            //fbName.text = result.ResultDictionary["name"].ToString();
//            userName = result.ResultDictionary["name"].ToString();
//            foreach (var item in result.ResultDictionary)
//            {
//                Debug.Log(item.Key + " - " + item.Value);

//            }
//        }
//        else
//        {
//            // Debug.Log("FACEBOOK error: " + result.Error);

//        }
//    }

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------
//    private void FacebookProfilePhotoCallback(IGraphResult result)
//    {
//        if (result.Error != null)
//        {
//            //  Debug.Log("FACEBOOK error: " + result.Error);
//            FB.LogOut();
//            OnFaceBookLoginComplete?.Invoke();
//            return;
//        }

//        // spriteAvatar = Sprite.Create(result.Texture, new Rect(0, 0, result.Texture.width, result.Texture.height), new Vector2(0.5f, 0.5f));
//    }

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------
//    private void PlayfabProfileCallback(PlayFab.ClientModels.LoginResult result)
//    {
//        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
//        {
//            DisplayName = userName,
//        }, (success) =>
//        {
//            // Debug.Log("Set PLAYFAB user's name");
//            if (string.IsNullOrEmpty(userName))
//            {
//                userName = "Player";
//            }
//        }, (error) =>
//        {
//            FB.LogOut();
//            Debug.Log($"Name Failed: {error.ErrorMessage} !");
//        });

//        string url = "https" + "://graph.facebook.com/" + AccessToken.CurrentAccessToken.UserId + "/picture";
//        url += "?access_token=" + AccessToken.CurrentAccessToken.TokenString;

//        PlayFabClientAPI.UpdateAvatarUrl(new UpdateAvatarUrlRequest
//        {
//            ImageUrl = url
//        }, (succes) => { Debug.Log("Set PLAYFAB user's avatar"); }, (error) => { Debug.Log("Avatar Failed !"); });
//    }

//    //---------------------------------------------------------------------------------------------------------------------------------------------------------
//    public string ShowText(/*GameObject login*/)
//    {
//        if (isLoginComple)
//        {
//            return GameLanguage.Get("txt_login_complete");
//        }
//        else
//        {
//            return GameLanguage.Get("txt_login_failed");
//        }
//    }
//}