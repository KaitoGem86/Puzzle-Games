using PopupSystem;
using TMPro;
using UnityEngine;

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

        private TwoStateElement _dailyButton;
        private TwoStateElement _endlessButton;

        public override void Awake()
        {
            base.Awake();
            _dailyButton = new TwoStateElement(_dailyButtonObject);
            _endlessButton = new TwoStateElement(_endlessButtonObject);
            UpdateTextCoin();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Show(){
            base.Show();
            UpdateTextCoin();
            OpenDailyChallengeSheet();
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

        private void UpdateTextCoin(){
            _coinText.text = PlayerData.UserData.CoinNumber.ToString();
        }
    }

}