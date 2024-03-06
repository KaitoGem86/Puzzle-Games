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
        [SerializeField] private TMPro.TMP_Text _shopText;
        [SerializeField] private TMPro.TMP_Text _rewardText;
        [SerializeField] private TMPro.TMP_Text _evaluateText;
        [SerializeField] private TMPro.TMP_Text _shareText;


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
            global::SFXTapController.Instance.OnClickButtonUI();
            GameManager.Instance.StateGameController.Playing();
        }

        public void OnSoundSettingClick()
        {
            global::SFXTapController.Instance.OnClickButtonUI();
            _soundSetting.SetState(!PlayerData.UserData.IsSoundOn);
            PlayerData.UserData.IsSoundOn = !PlayerData.UserData.IsSoundOn;
        }

        public void OnVibrateSettingClick()
        {
            global::SFXTapController.Instance.OnClickButtonUI();
            _vibrateSetting.SetState(!PlayerData.UserData.IsVibrateOn);
            PlayerData.UserData.IsVibrateOn = !PlayerData.UserData.IsVibrateOn;
            if (PlayerData.UserData.IsVibrateOn)
                Handheld.Vibrate();
        }

        public void OnLanguageSettingClick()
        {
            global::SFXTapController.Instance.OnClickButtonUI();
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");
        }

        public void OnOpenStageMenu()
        {
            global::SFXTapController.Instance.OnClickButtonUI();
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");
        }

        public void OnOpenShopButtonClick()
        {
            global::SFXTapController.Instance.OnClickButtonUI();
            PopupManager.CreateNewInstance<ShopPanel1>().Show();
        }

        public void OnRewardButtonClick()
        {
            //DO SOMETHING
            global::SFXTapController.Instance.OnClickButtonUI();
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");

        }

        public void OnEvaluateButtonClick()
        {
            //DO SOMETHING
            Debug.Log("Evaluate");
            global::SFXTapController.Instance.OnClickButtonUI();
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
            global::SFXTapController.Instance.OnClickButtonUI();
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

        private void SetText()
        {
            _shopText.text = "Cửa hàng";
            _rewardText.text = "Phần thưởng";
            _evaluateText.text = "Đánh giá";
            _shareText.text = "Chia sẻ";
        }
    }
}