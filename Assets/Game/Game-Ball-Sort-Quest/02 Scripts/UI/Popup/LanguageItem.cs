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
        public void Init(string lang_code, int index){
            _checkButton ??= new TwoStateElement(_checkButtonRoot);
            switch(lang_code){
                case "EN":
                    _languageText.text = "ENGLISH";
                    break;
                case "VI":
                    _languageText.text = "TIẾNG VIỆT";
                    break;
                case "JP":
                    _languageText.text = "日本語";
                    break;
            }
            this.GetComponent<Image>().color = index % 2 == 0 ? new Color(0,0,0,0) : new Color(36,108,245,70);
            _checkButton.SetState(lang_code == GameLanguage.Instance.crr_lang_code);
        }
        public void SetLanguage(string language)
        {
        }
    }
}