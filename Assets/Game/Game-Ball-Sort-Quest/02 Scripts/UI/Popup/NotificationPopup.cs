using PopupSystem;
using TMPro;
using UnityEngine;

namespace BallSortQuest
{
    public class NotificationPopup : BasePopup
    {
        [SerializeField] private TMP_Text _title;

        private bool _isFirstLayerPopup;
        
        public void Show(string title, bool isFirsLayerPopup = false)
        {
            base.Show();
            _title.text = title;
            _title.font = GameLanguage.Instance.langs.Find(x => x.lang_code.Equals(GameLanguage.Instance.crr_lang_code)).tm_font;
            _isFirstLayerPopup = isFirsLayerPopup;
            if (isFirsLayerPopup)
            {
                GameManager.Instance.StateGameController.OnMenu();
            }
        }

        public void Close()
        {
            base.Hide();
            global::SFXTapController.Instance.OnClickButtonUI();
            if (_isFirstLayerPopup)
            {
                GameManager.Instance.StateGameController.Playing();
            }
        }
    }

}
