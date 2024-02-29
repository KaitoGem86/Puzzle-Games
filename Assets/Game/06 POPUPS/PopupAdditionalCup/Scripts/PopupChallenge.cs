using PopupSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BallSortQuest
{
    public class PopupChallenge : BasePopup
    {
        [Header("Sheet Ref")]
        [SerializeField] private Transform _dailyButtonObject;
        [SerializeField] private Transform _endlessButtonObject;
        [SerializeField] private DailyChallengeFrameController _dailySheet;
        [Space, Header("Element Ref")]
        [SerializeField] private TMP_Text _coinText;
        [SerializeField] private Transform _hiddenRoot;
        [SerializeField] private Transform _timerRoot;
        [SerializeField] private Transform _moveRoot;
        [SerializeField] private Button _rewardAllButton;

        private TwoStateElement _dailyButton;
        private TwoStateElement _endlessButton;
        private TwoStateElement _hiddenIcon;
        private TwoStateElement _timerIcon;
        private TwoStateElement _moveIcon;


        public override void Awake()
        {
            base.Awake();
            _dailyButton = new TwoStateElement(_dailyButtonObject);
            _endlessButton = new TwoStateElement(_endlessButtonObject);
            _timerIcon ??= new TwoStateElement(_timerRoot);
            _hiddenIcon ??= new TwoStateElement(_hiddenRoot);
            _moveIcon ??= new TwoStateElement(_moveRoot);
            UpdateTextCoin();
        }

        public void Show(){
            base.Show();
            UpdateTextCoin();
            OpenDailyChallengeSheet();
            UpdatePanelRewardCompleteAll();
            _dailySheet.InitChallengeElements();
        }

        public void Exit(){
            base.Hide();
            GameManager.Instance.StateGameController.Playing();
        }

        public void OpenDailyChallengeSheet(){
            _dailyButton.SetState(true);
            _endlessButton.SetState(false);
            _dailySheet.gameObject.SetActive(true);
        }

        public void OpenEndlessChallengeSheet(){
            _dailyButton.SetState(false);
            _endlessButton.SetState(true);
            _dailySheet.gameObject.SetActive(false);
        }

        public void CollectRewardAll(){
            PlayerData.UserData.CoinNumber += 50;
            PlayerData.UserData.IsCanCollectAllReward = false;
            UpdateTextCoin();
            _rewardAllButton.interactable = false;
        }

        private void UpdatePanelRewardCompleteAll()
        {
            _hiddenIcon.SetState(PlayerData.UserData.HiddenState.Equals(ChallengeState.Success));
            _timerIcon.SetState(PlayerData.UserData.TimerState.Equals(ChallengeState.Success));
            _moveIcon.SetState(PlayerData.UserData.MoveState.Equals(ChallengeState.Success));
            _rewardAllButton.interactable = _hiddenIcon.IsOn && _timerIcon.IsOn && _moveIcon.IsOn && PlayerData.UserData.IsCanCollectAllReward;
        }

        private void UpdateTextCoin(){
            _coinText.text = PlayerData.UserData.CoinNumber.ToString();
        }
    }

}