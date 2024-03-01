using System.Collections.Generic;

namespace BallSortQuest{
    public class ShopPurchaseData{
        public List<int> PurchasedBackgroundIndexs;
        public List<int> PurchasedTubeIndexs;
        public List<int> PurchasedBallIndexs;

        public ShopPurchaseData(){
            PurchasedBackgroundIndexs = new List<int>();
            PurchasedTubeIndexs = new List<int>();
            PurchasedBallIndexs = new List<int>();
        }

        public List<int> GetPurchasedIndexs(TypeItem type){
            switch (type)
            {
                case TypeItem.Tube:
                    return PurchasedTubeIndexs;
                case TypeItem.Background:
                    return PurchasedBackgroundIndexs;
                case TypeItem.Ball:
                    return PurchasedBallIndexs;
                default:
                    return null;
            }
        }
    }
}