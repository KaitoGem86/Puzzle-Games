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
        }
    }
}