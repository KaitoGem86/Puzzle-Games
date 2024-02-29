using System;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest{

    [CreateAssetMenu(fileName = "ShopItemDatas", menuName = "ShopItemDatas")]
    public class ShopItemDatas : ScriptableObject{
        public TypeItem Type;
        public List<ShopItemData> ItemDatas;
    }

    [Serializable]
    public class ShopItemData{
        public Sprite Icon;
        //public bool IsPurchased;
    }
}