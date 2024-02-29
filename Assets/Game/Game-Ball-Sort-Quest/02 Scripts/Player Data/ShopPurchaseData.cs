using System.Collections.Generic;

namespace BallSortQuest{
    public class ShopPurchaseData{
        public List<int> PurchasedBackgroundIndexs;
        public List<int> PurchasedTubeIndexs;

        public ShopPurchaseData(){
            PurchasedBackgroundIndexs = new List<int>();
            PurchasedTubeIndexs = new List<int>();
        }

        public List<int> GetPurchasedIndexs(TypeItem type){
            if(type == TypeItem.Background){
                return PurchasedBackgroundIndexs;
            }
            else{
                return PurchasedTubeIndexs;
            }
        }
    }
}