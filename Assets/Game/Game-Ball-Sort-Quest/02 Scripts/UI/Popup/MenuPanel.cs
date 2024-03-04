using PopupSystem;
using UnityEngine;

namespace BallSortQuest
{
    public class MenuPanel : BasePopup
    {
        [Header("Menu Elements")]
        [SerializeField] private Transform _soundSettingButton;
        [SerializeField] private Transform _vibrateSettingButton;

        [Space, Header("Text Ref")]
        [SerializeField] private TMPro.TMP_Text _stageText;
        [SerializeField] private TMPro.TMP_Text _shopText;
        [SerializeField] private TMPro.TMP_Text _rewardText;
        [SerializeField] private TMPro.TMP_Text _evaluateText;
        [SerializeField] private TMPro.TMP_Text _helpText;
        [SerializeField] private TMPro.TMP_Text _achivementText;
        [SerializeField] private TMPro.TMP_Text _infoText;
        [SerializeField] private TMPro.TMP_Text _shareText;
        [SerializeField] private TMPro.TMP_Text _colorBlindModeText;
        [SerializeField] private TMPro.TMP_Text _removeAdsText;
        [SerializeField] private TMPro.TMP_Text _soundText;
        [SerializeField] private TMPro.TMP_Text _vibrateText;
        [SerializeField] private TMPro.TMP_Text _languageText;


        private TwoStateElement _soundSetting;
        private TwoStateElement _vibrateSetting;

        public void Show()
        {
            base.Show();
            SetText();
            GameManager.Instance.StateGameController.OnMenu();
            _soundSetting ??= new TwoStateElement(_soundSettingButton);
            _vibrateSetting ??= new TwoStateElement(_vibrateSettingButton);
            _soundSetting.SetState(PlayerData.UserData.IsSoundOn);
            _vibrateSetting.SetState(PlayerData.UserData.IsVibrateOn);
        }

        public void Close()
        {
            base.Hide();
            GameManager.Instance.StateGameController.Playing();
        }

        public void OnSoundSettingClick()
        {
            _soundSetting.SetState(!PlayerData.UserData.IsSoundOn);
            PlayerData.UserData.IsSoundOn = !PlayerData.UserData.IsSoundOn;
        }

        public void OnVibrateSettingClick()
        {
            _vibrateSetting.SetState(!PlayerData.UserData.IsVibrateOn);
            PlayerData.UserData.IsVibrateOn = !PlayerData.UserData.IsVibrateOn;
            if (PlayerData.UserData.IsVibrateOn)
                Handheld.Vibrate();
        }

        public void OnLanguageSettingClick()
        {
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");
        }

        public void OnOpenStageMenu()
        {
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");
        }

        public void OnOpenShopButtonClick()
        {
            PopupManager.CreateNewInstance<ShopPanel>().Show();
        }

        public void OnEvaluateButtonClick()
        {
            //DO SOMETHING
            Debug.Log("Evaluate");
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");

        }

        public void OnHelpButtonClick()
        {
            //DO SOMETHING
            Debug.Log("Help");
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");

        }

        public void OnGoToFaceBook()
        {
            //DO SOMETHING
            Debug.Log("GoToFaceBook");
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");
        }

        public void OnAchivementButtonClick()
        {
            //DO SOMETHING
            Debug.Log("Achivement");
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");

        }

        public void OnInfoButtonClick()
        {
            //DO SOMETHING
            Debug.Log("Info");
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");

        }

        public void OnMoreGameButtonClick()
        {
            //DO SOMETHING
            Debug.Log("MoreGame");
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");

        }

        public void OnSharedButtonClick()
        {
            //DO SOMETHING
            Debug.Log("Shared");
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");

        }

        public void OnColorBlindModeButtonClick()
        {
            //DO SOMETHING
            Debug.Log("ColorBlindMode");
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");

        }

        public void OnRemoveAdsButtonClick()
        {
            //DO SOMETHING
            Debug.Log("RemoveAds");
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");

        }

        private void SetText(){
            _stageText.text = "Stage";
            _shopText.text = "Cửa hàng";
            _rewardText.text = "Phần thưởng";
            _evaluateText.text = "Đánh giá";
            _helpText.text = "Trợ giúp";
            _achivementText.text = "Thành tựu";
            _infoText.text = "Thông tin";
            _shareText.text = "Chia sẻ";
            _colorBlindModeText.text = "Chế độ mù màu";
            _removeAdsText.text = "Loại bỏ quảng cáo";
            _soundText.text = "Âm thanh";
            _vibrateText.text = "Rung";
            _languageText.text = "Ngôn ngữ";
        }
    }
}