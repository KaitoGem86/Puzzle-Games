using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest{
    public class ListShopITemController{
        //Data
        private readonly ShopItemDatas _backgroundDatas;
        private readonly ShopItemDatas _tubeDatas;

        //Ref
        private readonly Transform _viewPort;
        private readonly GameObject _shopItemPrefab;

        //control list item
        private List<ShopItem> _shopItems;
        private TypeItem _currentShopBoardType;
        private ShopItem _currentSelectedItem;

        public ListShopITemController(){}
        public ListShopITemController(ShopItemDatas backgroundDatas, ShopItemDatas tubeDatas, Transform viewPort, GameObject shopItemPrefab){
            _backgroundDatas = backgroundDatas;
            _tubeDatas = tubeDatas;
            _viewPort = viewPort;
            _shopItemPrefab = shopItemPrefab;
            _shopItems = new List<ShopItem>();
        }   

        public void ClearBoard(){
            if (_shopItems == null) return;
            if (_shopItems.Count == 0) return;
            foreach (var item in _shopItems){
                SimplePool.Despawn(item.gameObject);
            }
            _shopItems.Clear();
        }

        public void ShowListItem(ShopItemDatas datas){
            Debug.Log("Show List Item " + datas.Type.ToString());
            _currentShopBoardType = datas.Type;
            foreach (var itemData in datas.ItemDatas){
                var item = SimplePool.Spawn(_shopItemPrefab, Vector2.zero, Quaternion.identity).GetComponent<ShopItem>();
                item.transform.SetParent(_viewPort);
                _shopItems.Add(item);
                item.Init(itemData, this);
            }
            switch(_currentShopBoardType){
                case TypeItem.Background:
                    SetSelected(_shopItems[PlayerData.UserData.CurrentBackgroundIndex]);
                    break;
                case TypeItem.Tube:
                    break;
            }
        }

        public IEnumerator GetRandomPurchasedItem(){
            yield return new WaitForEndOfFrame();
            Debug.Log("Get Random Purchased Item: " + Random.Range(0, _shopItems.Count));
            //var randomItem = _shopItems[Random.Range(0, _shopItems.Count)];
        }

        public void SetSelected(ShopItem item){
            if (_currentSelectedItem != null){
                _currentSelectedItem.SetUnselected();
            }
            _currentSelectedItem = item;
            _currentSelectedItem.SetSelected();
        }
    }
}