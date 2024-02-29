using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace BallSortQuest
{
    public enum TypeItem
    {
        Tube,
        Background,
        Ball
    }

    public class ShopItem : MonoBehaviour
    {
        [Header("Elements")]
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _unpurchaseBackground;
        [SerializeField] private GameObject _purchasedBackground;
        [SerializeField] private GameObject _selectedBackground;

        private ListShopITemController _listShopITemController;
        private bool _isPurchase;
        private int _id;

        public void Init(ShopItemData data, ListShopITemController container, int id)
        {
            _icon.sprite = data.Icon;
            _listShopITemController = container;
            _id = id;
            _isPurchase = PlayerData.UserData.GetShopPurchaseData().GetPurchasedIndexs(_listShopITemController.CurrentShopBoardType).Contains(_id);
            _unpurchaseBackground.SetActive(!_isPurchase);
            _purchasedBackground.SetActive(_isPurchase);
        }

        public void SetSelected()
        {
            _unpurchaseBackground.SetActive(false);
            _purchasedBackground.SetActive(false);
            _selectedBackground.SetActive(true);
        }

        public void SetUnselected()
        {
            _unpurchaseBackground.SetActive(!_isPurchase);
            _purchasedBackground.SetActive(_isPurchase);
            _selectedBackground.SetActive(false);
        }

        public void OnPurchase()
        {
            _isPurchase = true;
            _unpurchaseBackground.SetActive(!_isPurchase);
            _purchasedBackground.SetActive(_isPurchase);
        }

        public void UnPurchase()
        {
            _isPurchase = false;
            _unpurchaseBackground.SetActive(!_isPurchase);
            _purchasedBackground.SetActive(_isPurchase);
        }

        public void OnSelect()
        {
            if (!_isPurchase)
                return;
            _listShopITemController.SetSelected(this);
            PlayerData.UserData.SelectShop(_listShopITemController.CurrentShopBoardType, _id);
        }
    }
}