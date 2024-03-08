using PopupSystem;
using UnityEngine;

namespace BallSortQuest
{
    public class PopupLanguageSettings : BasePopup
    {
        [SerializeField] private GameObject _languageItemPrefab;
        [SerializeField] private Transform _containerList;
        private LanguageItem _currentLanguageItem;

        public void Show()
        {
            base.Show();
            int count = 0;
            foreach (var language in GameLanguage.DictionaryLang)
            {
                LanguageItem item = SimplePool.Spawn(_languageItemPrefab, Vector3.zero, Quaternion.identity).GetComponent<LanguageItem>();
                item.transform.SetParent(_containerList);
                item.Init(this, language.Key, count ++);
            }
        }

        public void Close()
        {
            global::SFXTapController.Instance.OnClickButtonUI();
            base.Hide();
        }

        public void OnLanguageSettingClick()
        {
            global::SFXTapController.Instance.OnClickButtonUI();
            //Do something
            GameLanguage.Instance.SetLanguage(CurrentLanguageCode);
            base.Hide();
        }

        public LanguageItem CurrentLanguageItem
        {
            get => _currentLanguageItem;
            set => _currentLanguageItem = value;
        }
        public string CurrentLanguageCode {get; set;}
    }
}