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
        
        private bool _isPurchase;

        public void Init(ShopItemData data){
            _icon.sprite = data.Icon;
            _isPurchase = data.IsPurchased;
            _unpurchaseBackground.SetActive(!_isPurchase);
            _purchasedBackground.SetActive(_isPurchase);
        
        }

        public void SetSelected(bool isSelected){
            _unpurchaseBackground.SetActive(!isSelected && !_isPurchase);
            _purchasedBackground.SetActive(isSelected && _isPurchase);
            _selectedBackground.SetActive(isSelected);
        }
    }
}