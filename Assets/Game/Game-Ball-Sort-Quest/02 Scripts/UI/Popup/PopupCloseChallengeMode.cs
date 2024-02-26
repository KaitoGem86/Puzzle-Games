using PopupSystem;
using TMPro;
using UnityEngine;

namespace BallSortQuest{
    public class PopupCloseChallengeMode : BasePopup
    {
        [Header("Popup Ref")]
        [SerializeField] private TMP_Text _notiText;
        private TypeChallenge _typeChallengeToChangeTo;
        private bool _isGameModeEnd;

        public void Show(string noti, TypeChallenge typeChallenge = TypeChallenge.None, bool isGameModeEnd = false){
            base.Show();
            BallSortQuest.GameManager.Instance.StateGameController.OnMenu();
            _notiText.text = noti;
            _typeChallengeToChangeTo = typeChallenge;
            _isGameModeEnd = isGameModeEnd;
        }

        public void OnClickAccept(){
            base.Hide();
            BallSortQuest.GameManager.Instance.GameModeController.OnCloseGameMode();
            BallSortQuest.GameManager.Instance.GameModeController.CurrentGameMode = _typeChallengeToChangeTo;
            BallSortQuest.ActionEvent.OnResetGamePlay?.Invoke();
            BallSortQuest.GameManager.Instance.StateGameController.Playing();
        }

        public void OnClickCancel(){
            base.Hide();
            BallSortQuest.GameManager.Instance.StateGameController.Playing();
            if(_isGameModeEnd){
                BallSortQuest.GameManager.Instance.GameModeController.OnCloseGameMode();
                BallSortQuest.GameManager.Instance.GameModeController.CurrentGameMode = TypeChallenge.None;
                BallSortQuest.ActionEvent.OnResetGamePlay?.Invoke();
            }
        }
    }
}