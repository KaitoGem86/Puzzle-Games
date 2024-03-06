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
        [SerializeField] private AnimButton _rewardAllButton;
        [Space, Header("Text Ref")]
        [SerializeField] private TMP_Text _dailyQuestText1;
        [SerializeField] private TMP_Text _dailyQuestText2;
        [SerializeField] private TMP_Text _endlessQuestText1;
        [SerializeField] private TMP_Text _endlessQuestText2;
        [SerializeField] private TMP_Text _hiddenQuestText;
        [SerializeField] private TMP_Text _timeQuestText;
        [SerializeField] private TMP_Text _moveQuestText;
        [SerializeField] private TMP_Text _rewardAllText;

        //private TwoStateElement _dailyButton;
        //private TwoStateElement _endlessButton;
        private TwoStateElement _hiddenIcon;
        private TwoStateElement _timerIcon;
        private TwoStateElement _moveIcon;
        private TwoStateElement _rewardAllButtonIcon;


        public override void Awake()
        {
            base.Awake();
            //_dailyButton = new TwoStateElement(_dailyButtonObject);
            //_endlessButton = new TwoStateElement(_endlessButtonObject);
            _timerIcon ??= new TwoStateElement(_timerRoot);
            _hiddenIcon ??= new TwoStateElement(_hiddenRoot);
            _moveIcon ??= new TwoStateElement(_moveRoot);
            _rewardAllButtonIcon ??= new TwoStateElement(_rewardAllButton.transform);
            UpdateTextCoin();
        }

        public void Show()
        {
            base.Show();
            UpdateTextCoin();
            SetText();
            OpenDailyChallengeSheet();
            UpdatePanelRewardCompleteAll();
            _dailySheet.InitChallengeElements();
        }

        public void Exit()
        {
            base.Hide();
            GameManager.Instance.StateGameController.Playing();
        }

        public void OpenDailyChallengeSheet()
        {

            //_dailyButton.SetState(true);
            //_endlessButton.SetState(false);
            //_dailySheet.gameObject.SetActive(true);
        }

        public void OpenEndlessChallengeSheet()
        {
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng này đang được phát triển");
            // _dailyButton.SetState(false);
            // _endlessButton.SetState(true);
            // _dailySheet.gameObject.SetActive(false);
        }

        public void CollectRewardAll()
        {
            global::SFXTapController.Instance.OnClickButtonUI();
            PlayerData.UserData.CoinNumber += 50;
            PlayerData.UserData.IsCanCollectAllReward = false;
            UpdateTextCoin();
            _rewardAllButton.Interactable = false;
            _rewardAllButtonIcon.SetState(false);
        }

        private void UpdatePanelRewardCompleteAll()
        {
            _hiddenIcon.SetState(PlayerData.UserData.HiddenState.Equals(ChallengeState.Success));
            _timerIcon.SetState(PlayerData.UserData.TimerState.Equals(ChallengeState.Success));
            _moveIcon.SetState(PlayerData.UserData.MoveState.Equals(ChallengeState.Success));
            _rewardAllButton.Interactable = _hiddenIcon.IsOn && _timerIcon.IsOn && _moveIcon.IsOn && PlayerData.UserData.IsCanCollectAllReward;
            _rewardAllButtonIcon.SetState(_rewardAllButton.Interactable);
        }

        private void UpdateTextCoin()
        {
            _coinText.text = PlayerData.UserData.CoinNumber.ToString();
        }

        private void SetText()
        {
            _dailyQuestText1.text = "Nhiệm vụ hàng ngày";
            _dailyQuestText2.text = "Nhiệm vụ hàng ngày";
            _endlessQuestText1.text = "Chế độ cạnh tranh vô tận";
            _endlessQuestText2.text = "Chế độ cạnh tranh vô tận";
            _hiddenQuestText.text = "Nhiệm vụ Ẩn";
            _timeQuestText.text = "Nhiệm vụ Thời Gian";
            _moveQuestText.text = "Nhiệm vụ Di Chuyển";
            _rewardAllText.text = "Phần thưởng hoàn thành tất cả";
        }
    }

}