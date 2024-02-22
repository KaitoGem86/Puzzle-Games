using UnityEngine;

namespace BallSortQuest{
    
    public class DailyChallengeFrameController : MonoBehaviour{

        [Header("Elements Challenge")]
        [SerializeField] private Transform _hiddenChallengeObject;
        [SerializeField] private Transform _timerChallengeObject;
        [SerializeField] private Transform _moveChallengeObject;

        [Header("Ref")]
        [SerializeField] private PopupChallenge _container;

        private RewardElement _hiddenChallenge;
        private RewardElement _timerChallenge;
        private RewardElement _moveChallenge;
                
        public void InitChallengeElements(){
            _hiddenChallenge = new RewardElement(_hiddenChallengeObject, TypeChallenge.Hidden);
            _timerChallenge = new RewardElement(_timerChallengeObject, TypeChallenge.Timer);
            _moveChallenge = new RewardElement(_moveChallengeObject, TypeChallenge.Move);
            _hiddenChallenge.SetDisPlayElement();
            _timerChallenge.SetDisPlayElement();
            _moveChallenge.SetDisPlayElement();
            _hiddenChallenge.PlayButton.onClick.RemoveAllListeners();
            _timerChallenge.PlayButton.onClick.RemoveAllListeners();
            _moveChallenge.PlayButton.onClick.RemoveAllListeners();
            _hiddenChallenge.PlayButton.onClick.AddListener(() => OnClickPlayChallenge(TypeChallenge.Hidden));
            _timerChallenge.PlayButton.onClick.AddListener(() => OnClickPlayChallenge(TypeChallenge.Timer));
            _moveChallenge.PlayButton.onClick.AddListener(() => OnClickPlayChallenge(TypeChallenge.Move));
        }

        public void OnClickPlayChallenge(TypeChallenge type){
            switch (type){
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
            _container.Exit();
        }
    }
}