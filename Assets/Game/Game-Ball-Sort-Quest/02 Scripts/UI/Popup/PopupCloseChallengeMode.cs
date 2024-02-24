using PopupSystem;
using TMPro;
using UnityEngine;

namespace BallSortQuest{
    public class PopupCloseChallengeMode : BasePopup
    {
        [Header("Popup Ref")]
        [SerializeField] private TMP_Text _notiText;
        private TypeChallenge _typeChallengeToChangeTo;

        public void Show(string noti, TypeChallenge typeChallenge = TypeChallenge.None){
            base.Show();
            BallSortQuest.GameManager.Instance.StateGameController.OnMenu();
            _notiText.text = noti;
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
        }
    }
}