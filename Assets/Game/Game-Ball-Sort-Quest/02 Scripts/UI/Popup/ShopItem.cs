using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace BallSortQuest{
    public enum TypeItem{
        Tube,
        Background
    }
    
    public class ShopItem: MonoBehaviour{
        [Header("Elements")]
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _unpurchaseBackground;
        [SerializeField] private GameObject _purchasedBackground;
        [SerializeField] private GameObject _selectedBackground;
        
        private ListShopITemController _listShopITemController;
        private bool _isPurchase;

        public void Init(ShopItemData data, ListShopITemController container){
            _icon.sprite = data.Icon;
            _isPurchase = data.IsPurchased;
            _unpurchaseBackground.SetActive(!_isPurchase);
            _purchasedBackground.SetActive(_isPurchase);
            _listShopITemController = container;
        }

        public void SetSelected(){
            _unpurchaseBackground.SetActive(false);
            _purchasedBackground.SetActive(false);
            _selectedBackground.SetActive(true);
        }

        public void SetUnselected(){
            _unpurchaseBackground.SetActive(!_isPurchase);
            _purchasedBackground.SetActive(_isPurchase);
            _selectedBackground.SetActive(false);
        }

        public void OnPurchase(){
            _isPurchase = true;
            _unpurchaseBackground.SetActive(!_isPurchase);
            _purchasedBackground.SetActive(_isPurchase);
        }

        public void OnSelect(){
            _listShopITemController.SetSelected(this);
        }
    }
}