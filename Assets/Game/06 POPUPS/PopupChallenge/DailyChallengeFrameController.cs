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
                _hiddenChallenge.PlayButton.onClick.RemoveAllListeners();
                _hiddenChallenge.PlayButton.onClick.AddListener(() => OnClickPlayChallenge(TypeChallenge.Hidden));
            }
            // else{
            //     _hiddenChallenge.PlayButton.interactable = false;
            // }

            if (true)
            {
                _timerChallenge.PlayButton.onClick.RemoveAllListeners();
                _timerChallenge.PlayButton.onClick.AddListener(() => OnClickPlayChallenge(TypeChallenge.Timer));
            }
            // else{
            //     _timerChallenge.PlayButton.interactable = false;
            // }

            if (true)
            {
                _moveChallenge.PlayButton.onClick.RemoveAllListeners();
                _moveChallenge.PlayButton.onClick.AddListener(() => OnClickPlayChallenge(TypeChallenge.Move));
            }
            // else{
            //     _moveChallenge.PlayButton.interactable = false;
            // }
        }

        public void OnClickPlayChallenge(TypeChallenge type)
        {
            switch (type)
            {
                case TypeChallenge.Hidden:
                    _hiddenChallenge.OnSelectChallenge();
                    if(!_hiddenChallenge.IsCanPlayNoNeedAds)
                    {
                        Debug.Log("Watch Ads");
                        //Do something with Ads
                    }
                    ActionEvent.OnResetGamePlay?.Invoke();
                    _container.Exit();
                    break;
                case TypeChallenge.Timer:
                    _timerChallenge.OnSelectChallenge();
                    if(!_timerChallenge.IsCanPlayNoNeedAds)
                    {
                        Debug.Log("Watch Ads");
                        //Do something with Ads
                    }
                    ActionEvent.OnResetGamePlay?.Invoke();
                    _container.Exit();
                    break;
                case TypeChallenge.Move:
                    _moveChallenge.OnSelectChallenge();
                    if(!_moveChallenge.IsCanPlayNoNeedAds)
                    {
                        Debug.Log("Watch Ads");
                        //Do something with Ads
                    }
                    ActionEvent.OnResetGamePlay?.Invoke();
                    _container.Exit();
                    break;
            }
            //Do something with GamePlay as ResetGamePlay ???

        }
    }
}