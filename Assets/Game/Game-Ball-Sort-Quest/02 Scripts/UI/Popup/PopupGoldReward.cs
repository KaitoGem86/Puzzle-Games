using System;
using System.Collections;
using PopupSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace BallSortQuest
{
    public class PopupGoldReward : BasePopup
    {
        [Header("Popup Elements")]

        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private GameObject _rewardImage;
        [SerializeField] private ChestController _chestImage;

        [SerializeField] private TMP_Text _goldText;

        [Space, Header("Resource Elements")]
        [SerializeField] private Sprite _openChestSprite;
        [SerializeField] private Sprite _closeChestSprite;

        [Space, Header("Text Elements")]

        [SerializeField] private TMP_Text _buttonLabel;

        private TimeSpan _timer;
        private bool _isCanGetReward;

        public void Show()
        {
            base.Show();
            GameManager.Instance.StateGameController.OnMenu();
            StopAllCoroutines();
            StartCoroutine(StartTimer());
        }

        public void Show(DateTime time)
        {
            base.Show();
            GameManager.Instance.StateGameController.OnMenu();
            //_labelText.text = "Bạn đã hoàn thành màn chơi";
            _timer = time - DateTime.Parse(PlayerData.UserData.LastTimeGetReward);
            _timer = new TimeSpan(1, 0, 0) - _timer;
            if (_timer.Hours >= 1)
            {
                _isCanGetReward = true;
            }
            else
            {
                _isCanGetReward = false;
            }
            StopAllCoroutines();
            SetRewardBoard();
        }

        public void Close()
        {
            base.Hide();
            GameManager.Instance.StateGameController.Playing();
            StopCoroutine(StartTimer());
        }

        public void OnClickInfoButton()
        {
            PopupManager.CreateNewInstance<NotificationPopup>().Show("Thông báo\nMỗi giờ bạn sẽ nhận được 30 vàng miễn phí.");
        }

        public void OnClickRewardButton()
        {
            global::SFXTapController.Instance.OnClickButtonUI();
            if (_isCanGetReward)
            {
                PlayerData.UserData.CoinNumber += 30;
                UpdateGoldText();
                PlayerData.UserData.LastTimeGetReward = TimeFromToGoogle.Instance.Now().ToString();
                PlayerData.SaveUserData();
                _isCanGetReward = false;
                _timer = new TimeSpan(1, 0, 0);
                SetRewardBoard();
                _chestImage.OpenChest(null);
            }
            else
            {
                Close();
            }
        }

        public void OnClickAdsRewardButton()
        {
            Debug.Log("Watch Ads");
            //Do something with Ads
            global::SFXTapController.Instance.OnClickButtonUI();
            bool isShowBanner = false;
            AdsManager.Instance.ShowRewardedAd(
                () =>
                {
                    isShowBanner = true;
                },
                () =>
                {
                    if (isShowBanner)
                    {
                        PlayerData.UserData.CoinNumber += 10;
                        UpdateGoldText();
                    }
                }
            );
        }

        private void UpdateGoldText()
        {
            _goldText.text = PlayerData.UserData.CoinNumber.ToString();
        }

        private void SetRewardBoard()
        {
            if (_isCanGetReward)
            {
                _rewardImage.SetActive(true);
                _timerText.transform.parent.gameObject.SetActive(false);
                _buttonLabel.text = GameLanguage.Get("txt_get_reward");
                _buttonLabel.font = GameLanguage.Instance.langs.Find(x => x.lang_code.Equals(GameLanguage.Instance.crr_lang_code)).tm_font;
            }
            else
            {
                _rewardImage.SetActive(false);
                _chestImage.gameObject.SetActive(true);
                _buttonLabel.text = GameLanguage.Get("txt_accept");
                _buttonLabel.font = GameLanguage.Instance.langs.Find(x => x.lang_code.Equals(GameLanguage.Instance.crr_lang_code)).tm_font;
                _timerText.transform.parent.gameObject.SetActive(true);
                StartCoroutine(StartTimer());
            }
            _chestImage.DefaultChest();
            UpdateGoldText();
        }

        private IEnumerator StartTimer()
        {
            if (_timer.Hours >= 1)
            {
                _timer = new TimeSpan(0, 59, 59);
            }
            while (true)
            {
                _timer = _timer.Add(new TimeSpan(0, 0, -1));
                _timerText.text = _timer.Minutes.ToString("00") + ":" + _timer.Seconds.ToString("00");
                if (_timer.Hours <= 0 && _timer.Minutes <= 0 && _timer.Seconds <= 0)
                {
                    _isCanGetReward = true;
                    SetRewardBoard();
                    yield break;
                }
                yield return new WaitForSeconds(1);
            }

        }
    }
}