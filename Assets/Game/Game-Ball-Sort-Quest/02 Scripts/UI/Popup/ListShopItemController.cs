using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BallSortQuest
{
    public class ListShopITemController
    {
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

        public ListShopITemController() { }
        public ListShopITemController(ShopItemDatas backgroundDatas, ShopItemDatas tubeDatas, Transform viewPort, GameObject shopItemPrefab)
        {
            _backgroundDatas = backgroundDatas;
            _tubeDatas = tubeDatas;
            _viewPort = viewPort;
            _shopItemPrefab = shopItemPrefab;
            _shopItems = new List<ShopItem>();
        }

        public void ClearBoard()
        {
            if (_shopItems == null) return;
            if (_shopItems.Count == 0) return;
            for (int i = _shopItems.Count - 1; i >= 0; i--)
            {
                SimplePool.Despawn(_shopItems[i].gameObject);
                _shopItems.RemoveAt(i);
            }
            _shopItems.Clear();
        }

        public void ShowListItem(ShopItemDatas datas)
        {
            Debug.Log("Show List Item " + datas.Type.ToString());
            _currentShopBoardType = datas.Type;

            for (int i = 0; i < datas.ItemDatas.Count; i++)
            {
                var item = SimplePool.Spawn(_shopItemPrefab, Vector2.zero, Quaternion.identity).GetComponent<ShopItem>();
                item.transform.SetParent(_viewPort);
                _shopItems.Add(item);
                _shopItems[i].Init(datas.ItemDatas[i], this, i);
            }

            switch (_currentShopBoardType)
            {
                case TypeItem.Background:
                    SetSelected(_shopItems[PlayerData.UserData.CurrentBackgroundIndex]);
                    break;
                case TypeItem.Tube:
                    SetSelected(_shopItems[PlayerData.UserData.CurrentTubeIndex]);
                    break;
            }
        }

        public IEnumerator GetRandomPurchasedItem()
        {
            yield return new WaitForEndOfFrame();
            var purchasedData = PlayerData.UserData.GetShopPurchaseData();
            if (purchasedData.GetPurchasedIndexs(_currentShopBoardType).Count == _shopItems.Count)
            {
                Debug.Log("All item purchased");
                yield break;
            }
            else
            {
                int randomItemIndex = Random.Range(0, _shopItems.Count);
                while (purchasedData.GetPurchasedIndexs(_currentShopBoardType).Contains(randomItemIndex))
                {
                    randomItemIndex = (randomItemIndex + 1) % _shopItems.Count;
                }
                var randomItem = _shopItems[randomItemIndex];
                PlayerData.UserData.AddPurchaseData(_currentShopBoardType, randomItemIndex);
                yield return ShowRandomRound(randomItemIndex);
            }
        }

        private IEnumerator ShowRandomRound(int index)
        {
            var purchasedData = PlayerData.UserData.GetShopPurchaseData().GetPurchasedIndexs(_currentShopBoardType);
            Debug.Log("Purchased Data " + purchasedData.Count + " " + _shopItems.Count);
            List<int> listRandom = Enumerable.Range(0, _shopItems.Count).Where(x => !purchasedData.Contains(x)).ToList();
            if (listRandom.Count == 0)
            {
                _shopItems[index].OnPurchase();
                yield break;
            }
            else
            {
                ShopItem currentItem = _shopItems[listRandom[0]];
                ShopItem randomItem = _shopItems[listRandom[0]];
                float timer = 2f;
                while (timer > 0)
                {
                    timer -= 0.3f;
                    currentItem.UnPurchase();
                    randomItem.OnPurchase();
                    currentItem = randomItem;
                    randomItem = _shopItems[listRandom[Random.Range(0, listRandom.Count)]];
                    yield return new WaitForSeconds(0.3f);
                }
                currentItem.UnPurchase();
                randomItem.UnPurchase();
            }

            _shopItems[index].OnPurchase();
        }

        public void SetSelected(ShopItem item)
        {
            if (_currentSelectedItem != null)
            {
                _currentSelectedItem.SetUnselected();
            }
            _currentSelectedItem = item;
            _currentSelectedItem.SetSelected();
        }

        public TypeItem CurrentShopBoardType => _currentShopBoardType;
    }
}