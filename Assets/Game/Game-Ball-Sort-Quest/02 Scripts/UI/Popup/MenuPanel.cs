using PopupSystem;
using UnityEngine;

namespace BallSortQuest
{
    public class MenuPanel : BasePopup
    {
        [Header("Menu Elements")]
        [SerializeField] private Transform _soundSettingButton;
        [SerializeField] private Transform _vibrateSettingButton;

        private TwoStateElement _soundSetting;
        private TwoStateElement _vibrateSetting;

        public void Show(){
            base.Show();
            GameManager.Instance.StateGameController.OnMenu();
            _soundSetting ??= new TwoStateElement(_soundSettingButton);
            _vibrateSetting ??= new TwoStateElement(_vibrateSettingButton);
            _soundSetting.SetState(PlayerData.UserData.IsSoundOn);
            _vibrateSetting.SetState(PlayerData.UserData.IsVibrateOn);
        }

        public void Close(){
            base.Hide();
            GameManager.Instance.StateGameController.Playing();
        }

        public void OnSoundSettingClick(){
            _soundSetting.SetState(!PlayerData.UserData.IsSoundOn);
            PlayerData.UserData.IsSoundOn = !PlayerData.UserData.IsSoundOn;
        }

        public void OnVibrateSettingClick(){
            _vibrateSetting.SetState(!PlayerData.UserData.IsVibrateOn);
            PlayerData.UserData.IsVibrateOn = !PlayerData.UserData.IsVibrateOn;
            if (PlayerData.UserData.IsVibrateOn)
                Handheld.Vibrate();
        }

        public void OnOpenShopButtonClick(){
            PopupManager.CreateNewInstance<ShopPanel>().Show();
        }

        public void OnEvaluateButtonClick(){
            //DO SOMETHING
            Debug.Log("Evaluate");
        }

        public void OnHelpButtonClick(){
            //DO SOMETHING
            Debug.Log("Help");
        }

        public void OnGoToFaceBook(){
            //DO SOMETHING
            Debug.Log("GoToFaceBook");
        }

        public void OnAchivementButtonClick(){
            //DO SOMETHING
            Debug.Log("Achivement");
        }

        public void OnInfoButtonClick(){
            //DO SOMETHING
            Debug.Log("Info");
        }

        public void OnMoreGameButtonClick(){
            //DO SOMETHING
            Debug.Log("MoreGame");
        }

        public void OnSharedButtonClick(){
            //DO SOMETHING
            Debug.Log("Shared");
        }

        public void OnColorBlindModeButtonClick(){
            //DO SOMETHING
            Debug.Log("ColorBlindMode");
        }

        public void OnRemoveAdsButtonClick(){
            //DO SOMETHING
            Debug.Log("RemoveAds");
        }
    }
}