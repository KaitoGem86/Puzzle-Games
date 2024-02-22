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
            _hiddenChallenge.SetDisPlayElement();
            _timerChallenge.SetDisPlayElement();
            _moveChallenge.SetDisPlayElement();
            if (PlayerData.UserData.HiddenState == ChallengeState.InComplete)
            {
                _hiddenChallenge.PlayButton.onClick.RemoveAllListeners();
                _hiddenChallenge.PlayButton.onClick.AddListener(() => OnClickPlayChallenge(TypeChallenge.Hidden));
            }
            else{
                _hiddenChallenge.PlayButton.interactable = false;
            }

            if (PlayerData.UserData.TimerState == ChallengeState.InComplete)
            {
                _timerChallenge.PlayButton.onClick.RemoveAllListeners();
                _timerChallenge.PlayButton.onClick.AddListener(() => OnClickPlayChallenge(TypeChallenge.Timer));
            }
            else{
                _timerChallenge.PlayButton.interactable = false;
            }
            
            if (PlayerData.UserData.MoveState == ChallengeState.InComplete)
            {
                _moveChallenge.PlayButton.onClick.RemoveAllListeners();
                _moveChallenge.PlayButton.onClick.AddListener(() => OnClickPlayChallenge(TypeChallenge.Move));
            }
            else{
                _moveChallenge.PlayButton.interactable = false;
            }
        }

        public void OnClickPlayChallenge(TypeChallenge type)
        {
            switch (type)
            {
                case TypeChallenge.Hidden:
                    _hiddenChallenge.OnSelectChallenge();
                    break;
                case TypeChallenge.Timer:
                    _timerChallenge.OnSelectChallenge();
                    break;
                case TypeChallenge.Move:
                    _moveChallenge.OnSelectChallenge();
                    break;
            }
            //Do something with GamePlay as ResetGamePlay ???
            ActionEvent.OnResetGamePlay?.Invoke();
            _container.Exit();
        }
    }
}