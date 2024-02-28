using UnityEngine;
using PopupSystem;
using TMPro;

namespace BallSortQuest
{
    public class ShopPanel : BasePopup
    {
        [Header("Shop Elements")]
        [SerializeField] private Transform _marketNavigateButton;
        [SerializeField] private Transform _tubeNavigateButton;
        [SerializeField] private Transform _backgroundNavigateButton;
        [SerializeField] private TMP_Text _coinText;
        [SerializeField] private GameObject _shopItemPrefab;

        [Space(10),Header("Purchase Boards")]
        [SerializeField] private GameObject _marketBoard;
        [SerializeField] private GameObject _listSlideBoard;
        [SerializeField] private Transform _listViewPort;

        [Space(10), Header("Data")]
        [SerializeField] private ShopItemDatas _backgroundDatas;
        [SerializeField] private ShopItemDatas _tubeDatas;

        private TwoStateElement _marketButton;
        private TwoStateElement _backgroundButton;
        private TwoStateAtMidElement _tubeButton;

        private ListShopITemController _listShopITemController;

        public void Show(){
            base.Show();
            _marketButton ??= new TwoStateElement(_marketNavigateButton);
            _backgroundButton ??= new TwoStateElement(_backgroundNavigateButton);
            _tubeButton ??= new TwoStateAtMidElement(_tubeNavigateButton);
            _listShopITemController ??= new ListShopITemController(_backgroundDatas, _tubeDatas, _listViewPort, _shopItemPrefab);
            OnMarketNavigateButtonClick();
            UpdateCoinText();
        }

        public void Close(){
            base.Hide();
        }

        public void OnMarketNavigateButtonClick(){
            _marketButton.SetState(true);
            _backgroundButton.SetState(false);
            _tubeButton.SetState(false, true);
            _marketBoard.SetActive(true);
            _listSlideBoard.SetActive(false);
        }

        public void OnTubeNavigateButtonClick(){
            _marketButton.SetState(false);
            _backgroundButton.SetState(false);
            _tubeButton.SetState(true, true);
            _marketBoard.SetActive(false);
            _listSlideBoard.SetActive(true);
            _listShopITemController.ClearBoard();
            _listShopITemController.ShowListItem(_tubeDatas);
        }

        public void OnBackgroundNavigateButtonClick(){
            _marketButton.SetState(false);
            _backgroundButton.SetState(true);
            _tubeButton.SetState(false, false);
            _marketBoard.SetActive(false);
            _listSlideBoard.SetActive(true);
            _listShopITemController.ClearBoard();
            _listShopITemController.ShowListItem(_backgroundDatas);
        }

        public void UpdateCoinText(){
            _coinText.text = PlayerData.UserData.CoinNumber.ToString();
        }

        public void OnClickButtonPurchase(){
            //DO SOMETHING
            Debug.Log("Purchase");
        }
    }
}