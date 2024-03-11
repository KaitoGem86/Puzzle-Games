using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BallSortQuest
{
    public class LanguageItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _languageText;
        [SerializeField] private Transform _checkButtonRoot;
        private TwoStateElement _checkButton;
        private string _langCode;
        private PopupLanguageSettings _container;
        public void Init(PopupLanguageSettings container, string lang_code, int index)
        {
            Debug.Log("LanguageItem Init " + lang_code);
            this.transform.localScale = Vector3.one;
            _checkButton ??= new TwoStateElement(_checkButtonRoot);
            _langCode = lang_code;
            _container = container;
            switch (lang_code)
            {
                case "EN":
                    _languageText.text = "ENGLISH";
                    _languageText.font = GameLanguage.Instance.langs.Find(x => x.lang_code.Equals("EN")).tm_font;
                    break;
                case "VI":
                    _languageText.text = "TIẾNG VIỆT";
                    _languageText.font = GameLanguage.Instance.langs.Find(x => x.lang_code.Equals("VI")).tm_font;
                    break;
                case "JP":
                    _languageText.text = "日本語";
                    _languageText.font = GameLanguage.Instance.langs.Find(x => x.lang_code.Equals("JP")).tm_font;
                    break;
            }
            this.GetComponent<Image>().color = index % 2 == 0 ? new Color(0, 0, 0, 0) : new Color(36, 108, 245, 70);
            _checkButton.SetState(lang_code == GameLanguage.Instance.crr_lang_code);
            if (lang_code == GameLanguage.Instance.crr_lang_code)
            {
                _container.CurrentLanguageItem = this;
                _container.CurrentLanguageCode = lang_code;
            }
        }
        public void OnClickSetLanguage()
        {
            if (_container.CurrentLanguageItem == this) return;
            if (_container.CurrentLanguageItem != null)
            {
                _container.CurrentLanguageItem._checkButton.SetState(false);
            }
            _container.CurrentLanguageItem = this;
            global::SFXTapController.Instance.OnClickButtonUI();
            _container.CurrentLanguageCode = _langCode;
            _checkButton.SetState(true);
        }
    }
}