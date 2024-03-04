using UnityEngine;
using UnityEngine.UI;

namespace BallSortQuest
{
    public class ShopItem1 : MonoBehaviour
    {
        [Header("Elements")]
        [SerializeField] private Image _icon;
        // [SerializeField] private GameObject _unpurchaseBackground;
        // [SerializeField] private GameObject _purchasedBackground;
        // [SerializeField] private GameObject _selectedBackground;

        [SerializeField] private GameObject _lockMask;
        [SerializeField] private GameObject _highlightBorder;
        [SerializeField] private GameObject _selectedPin;

        private ListShopITemController1 _listShopITemController;
        private bool _isPurchase;
        private int _id;

        public void Init(ShopItemData data, ListShopITemController1 container, int id)
        {
            _icon.sprite = data.Icon;
            _icon.SetNativeSize();
            _listShopITemController = container;
            _id = id;
            _isPurchase = PlayerData.UserData.GetShopPurchaseData().GetPurchasedIndexs(_listShopITemController.CurrentShopBoardType).Contains(_id);
            _lockMask.SetActive(!_isPurchase);
            _highlightBorder.SetActive(false);
            _selectedPin.SetActive(false);
            //_purchasedBackground.SetActive(_isPurchase);
        }

        public void SetSelected()
        {
            // _unpurchaseBackground.SetActive(false);
            // _purchasedBackground.SetActive(false);
            // _selectedBackground.SetActive(true);
            _lockMask.SetActive(false);
            _highlightBorder.SetActive(true);
            _selectedPin.SetActive(true);
        }

        public void SetUnselected()
        {
            // _unpurchaseBackground.SetActive(!_isPurchase);
            // _purchasedBackground.SetActive(_isPurchase);
            // _selectedBackground.SetActive(false);
            _lockMask.SetActive(!_isPurchase);
            _highlightBorder.SetActive(false);
            _selectedPin.SetActive(false);
        }

        public void OnPurchaseGacha()
        {
            //_isPurchase = true;
            // _unpurchaseBackground.SetActive(!_isPurchase);
            // _purchasedBackground.SetActive(_isPurchase);
            _highlightBorder.SetActive(true);
        }

        public void UnPurchaseGacha()
        {
            _highlightBorder.SetActive(false);
        }

        public void Purchased(){
            _isPurchase = true;
            _lockMask.SetActive(false);
            //_purchasedBackground.SetActive(_isPurchase);
        }

        public void OnSelect()
        {
            if (!_isPurchase)
                return;
            //_listShopITemController.SetSelected(this);
            _listShopITemController.SetSelected(this);
            // _highlightBorder.SetActive(true);
            // _selectedPin.SetActive(true);
            PlayerData.UserData.SelectShop(_listShopITemController.CurrentShopBoardType, _id);
        }
    }
}