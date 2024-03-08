using PopupSystem;
using UnityEngine;

namespace BallSortQuest
{
    public class PopupLanguageSettings : BasePopup
    {
        [SerializeField] private GameObject _languageItemPrefab;
        [SerializeField] private Transform _containerList;

        public void Show()
        {
            base.Show();
            int count = 0;
            foreach (var language in GameLanguage.DictionaryLang)
            {
                LanguageItem item = SimplePool.Spawn(_languageItemPrefab, Vector3.zero, Quaternion.identity).GetComponent<LanguageItem>();
                item.transform.SetParent(_containerList);
                item.Init(language.Key, count ++);
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
            base.Hide();
        }
    }
}