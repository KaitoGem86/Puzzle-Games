using System.Collections;
using DG.Tweening;
using TMPro;
using PopupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace BallSortQuest
{
    public class PopupWinChallenge : BasePopup
    {
        [Space, Header("PopUp Elements")]
        [SerializeField] private GameObject _firstGroupButton;
        [SerializeField] private GameObject _secondGroupButton;
        [SerializeField] private TMP_Text _textCoins;
        [SerializeField] private ChestController _chest;
        [SerializeField] private GameObject _rewardCoin;

        //Will be replace by animation
        [Space, Header("Resource")]
        [SerializeField] private Sprite _chestClose;
        [SerializeField] private Sprite _chestOpen;

        private bool _isCanCollectReward;
        private int _multiRewardCoeff = 1;
        public void Show()
        {
            base.Show();
            _secondGroupButton.SetActive(false);
            _firstGroupButton.SetActive(true);
            UpdateTextCoin();
            // Set Chest's State
            StartCoroutine(CoroutineActiveChest(0.5f));
            _isCanCollectReward = true;
        }

        public void Close()
        {
            base.Hide();
        }

        public void OnClickNextLevel()
        {
            global::SFXTapController.Instance.OnClickButtonUI();
            GameManager.Instance.GameModeController.CurrentGameMode = TypeChallenge.None;
            ActionEvent.OnResetGamePlay?.Invoke();
            Close();
        }

        public void OnClickAccept()
        {
            global::SFXTapController.Instance.OnClickButtonUI();
            _firstGroupButton.SetActive(false);
            _secondGroupButton.SetActive(true);
            if (_isCanCollectReward)
                PlayerData.UserData.CoinNumber += 20 * _multiRewardCoeff; // hard code, need to improve
            UpdateTextCoin();
            _multiRewardCoeff = 1;
        }

        public void OnClickAdsButton()
        {
            //Show Ads
            _multiRewardCoeff = 2;
            //If success, call OnClickAccept
            OnClickAccept();
        }

        IEnumerator CoroutineActiveChest(float timeDelay)
        {
            _chest.gameObject.SetActive(true);
            SetChestState(false);
            yield return new WaitForSeconds(timeDelay);
            SetChestState(true);
            _rewardCoin.SetActive(true);
        }

        private void SetChestState(bool isOpened)
        {
            if(isOpened){
                _chest.OpenChest(null);
            }
            else{
                _chest.DefaultChest();
            }
        }

        private void UpdateTextCoin()
        {
            _textCoins.text = PlayerData.UserData.CoinNumber.ToString();
        }
    }
}
