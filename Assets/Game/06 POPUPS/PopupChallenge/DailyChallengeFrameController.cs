using UnityEngine;

namespace BallSortQuest
{

    public class DailyChallengeFrameController : MonoBehaviour
    {

        [Header("Elements Challenge")]
        [SerializeField] private Transform _hiddenChallengeObject;
        [SerializeField] private Transform _timerChallengeObject;
        [SerializeField] private Transform _moveChallengeObject;

        [Header("Ref")]
        [SerializeField] private PopupChallenge _container;

        private RewardElement _hiddenChallenge;
        private RewardElement _timerChallenge;
        private RewardElement _moveChallenge;

        public void InitChallengeElements()
        {
            _hiddenChallenge = new RewardElement(_hiddenChallengeObject, TypeChallenge.Hidden);
            _timerChallenge = new RewardElement(_timerChallengeObject, TypeChallenge.Timer);
            _moveChallenge = new RewardElement(_moveChallengeObject, TypeChallenge.Move);
            var time = TimeFromToGoogle.Instance.Now();
            _hiddenChallenge.SetDisPlayElement(PlayerData.UserData.IsCanPlayChallengeWithNoAds(TypeChallenge.Hidden, time));
            _timerChallenge.SetDisPlayElement(PlayerData.UserData.IsCanPlayChallengeWithNoAds(TypeChallenge.Timer, time));
            _moveChallenge.SetDisPlayElement(PlayerData.UserData.IsCanPlayChallengeWithNoAds(TypeChallenge.Move, time));
            if (true)
            {
                _hiddenChallenge.PlayButton.onPointerClick.RemoveAllListeners();
                _hiddenChallenge.PlayButton.onPointerClick.AddListener(() => OnClickPlayChallenge(TypeChallenge.Hidden));
            }

            if (true)
            {
                _timerChallenge.PlayButton.onPointerClick.RemoveAllListeners();
                _timerChallenge.PlayButton.onPointerClick.AddListener(() => OnClickPlayChallenge(TypeChallenge.Timer));
            }

            if (true)
            {
                _moveChallenge.PlayButton.onPointerClick.RemoveAllListeners();
                _moveChallenge.PlayButton.onPointerClick.AddListener(() => OnClickPlayChallenge(TypeChallenge.Move));
            }
        }

        public void OnClickPlayChallenge(TypeChallenge type)
        {
            global::SFXTapController.Instance.OnClickButtonUI();
            switch (type)
            {
                case TypeChallenge.Hidden:
                    _hiddenChallenge.OnSelectChallenge();
                    if (!_hiddenChallenge.IsCanPlayNoNeedAds)
                    {
                        Debug.Log("Watch Ads");
                        //Do something with Ads
                        bool isShowBanner = false;
                        AdsManager.Instance.ShowRewardedAd(
                            () => { isShowBanner = true; },
                            () => { if (isShowBanner) 
                                    { 
                                        ActionEvent.OnResetGamePlay?.Invoke();
                                        _container.Exit();
                                    }
                                }
                        );
                    }
                    else
                    {
                        ActionEvent.OnResetGamePlay?.Invoke();
                        _container.Exit();
                    }
                    break;
                case TypeChallenge.Timer:
                    _timerChallenge.OnSelectChallenge();
                    if (!_timerChallenge.IsCanPlayNoNeedAds)
                    {
                        Debug.Log("Watch Ads");
                        bool isShowBanner = false;
                        AdsManager.Instance.ShowRewardedAd(
                            () => { isShowBanner = true; },
                            () => { if (isShowBanner) 
                                    { 
                                        ActionEvent.OnResetGamePlay?.Invoke();
                                        _container.Exit();
                                    }
                                }
                        );
                    }
                    ActionEvent.OnResetGamePlay?.Invoke();
                    _container.Exit();
                    break;
                case TypeChallenge.Move:
                    _moveChallenge.OnSelectChallenge();
                    if (!_moveChallenge.IsCanPlayNoNeedAds)
                    {
                        Debug.Log("Watch Ads");
                        bool isShowBanner = false;
                        AdsManager.Instance.ShowRewardedAd(
                            () => { isShowBanner = true; },
                            () => { if (isShowBanner) 
                                    { 
                                        ActionEvent.OnResetGamePlay?.Invoke();
                                        _container.Exit();
                                    }
                                }
                        );
                    }
                    ActionEvent.OnResetGamePlay?.Invoke();
                    _container.Exit();
                    break;
            }
            //Do something with GamePlay as ResetGamePlay ???

        }
    }
}