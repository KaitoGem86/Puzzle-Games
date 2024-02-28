using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest{
    public class ListShopITemController{
        //Data
        private readonly ShopItemDatas _backgroundDatas;
        private readonly ShopItemDatas _tubeDatas;

        //Ref
        private readonly Transform _viewPort;

        //control list item
        private List<ShopItem> _shopItems;
        private TypeItem _currentShopBoardType;
        
        public ListShopITemController(){}
        public ListShopITemController(ShopItemDatas backgroundDatas, ShopItemDatas tubeDatas, Transform viewPort){
            _backgroundDatas = backgroundDatas;
            _tubeDatas = tubeDatas;
            _viewPort = viewPort;
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
        }
    }
}