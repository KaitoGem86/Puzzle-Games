using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PopupSystem;
using TMPro;

namespace BallSortQuest
{
    public class ShopPanel1 : BasePopup
    {
        [Header("Shop Elements")]
        //[SerializeField] private Transform _marketNavigateButton;
        [SerializeField] private Transform _tubeNavigateButton;
        [SerializeField] private Transform _backgroundNavigateButton;
        [SerializeField] private Transform _ballNavigateButton;
        [SerializeField] private TMP_Text _coinText;
        [SerializeField] private GameObject _shopItemPrefab;

        [Space(10), Header("Purchase Boards")]
        [SerializeField] private GameObject _listSlideBoard;
        [SerializeField] private Transform _listViewPort;

        [Space(10), Header("Data")]
        [SerializeField] private ShopItemDatas _backgroundDatas;
        [SerializeField] private ShopItemDatas _tubeDatas;
        [SerializeField] private ShopItemDatas _ballDatas;

        //private TwoStateElement _marketButton;
        private TwoStateElement _backgroundButton;
        private TwoStateElement _tubeButton;
        private TwoStateElement _ballButton;

        private ListShopITemController1 _listShopITemController;

        public void Show()
        {
            base.Show();
            //_marketButton ??= new TwoStateElement(_marketNavigateButton);
            _backgroundButton ??= new TwoStateElement(_backgroundNavigateButton);
            _tubeButton ??= new TwoStateAtMidElement(_tubeNavigateButton);
            _ballButton ??= new TwoStateAtMidElement(_ballNavigateButton);
            _listShopITemController ??= new ListShopITemController1(_backgroundDatas, _tubeDatas, _ballDatas, _listViewPort, _shopItemPrefab);
            //OnMarketNavigateButtonClick();
            OnTubeNavigateButtonClick();
            UpdateCoinText();
        }

        public void Close()
        {
            base.Hide();
            StopCoroutine(GetRandomPurchasedItem());
        }

        // public void OnMarketNavigateButtonClick()
        // {
        //     //_marketButton.SetState(true);
        //     _backgroundButton.SetState(false);
        //     _tubeButton.SetState(false, true);
        //     _ballButton.SetState(false, true);
        //     _marketBoard.SetActive(true);
        //     _listSlideBoard.SetActive(false);
        // }

        public void OnTubeNavigateButtonClick()
        {
            //_marketButton.SetState(false);
            _backgroundButton.SetState(false);
            _tubeButton.SetState(true);
            _ballButton.SetState(false);
            _listSlideBoard.SetActive(true);
            _listShopITemController.ClearBoard();
            _listShopITemController.ShowListItem(_tubeDatas);
        }

        public void OnBallNavigateButtonClick()
        {
            //_marketButton.SetState(false);
            _backgroundButton.SetState(false);
            _tubeButton.SetState(false);
            _ballButton.SetState(true);
            _listSlideBoard.SetActive(true);
            _listShopITemController.ClearBoard();
            _listShopITemController.ShowListItem(_ballDatas);
        }

        public void OnBackgroundNavigateButtonClick()
        {
            //_marketButton.SetState(false);
            _backgroundButton.SetState(true);
            _tubeButton.SetState(false);
            _ballButton.SetState(false);
            _listSlideBoard.SetActive(true);
            _listShopITemController.ClearBoard();
            _listShopITemController.ShowListItem(_backgroundDatas);
        }


        public void UpdateCoinText()
        {
            _coinText.text = PlayerData.UserData.CoinNumber.ToString();
        }

        public void OnClickButtonPurchase()
        {
            //DO SOMETHING
            Debug.Log("Purchase");
            if (PlayerData.UserData.CoinNumber < 800)
            {
                PlayerData.UserData.CoinNumber += 800;
            }

            PlayerData.UserData.CoinNumber -= 800;
            UpdateCoinText();
            StartCoroutine(GetRandomPurchasedItem());
        }

        private IEnumerator GetRandomPurchasedItem()
        {
            yield return _listShopITemController.GetRandomPurchasedItem();
        }
    }
}