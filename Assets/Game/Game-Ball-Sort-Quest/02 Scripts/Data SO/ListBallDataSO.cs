using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest{
    [CreateAssetMenu(fileName = "BallDatas", menuName = "BallDataSO")]
    public class ListBallDataSO : ScriptableObject{
        public List<BallSpriteData> BallDatas;
    }

    [System.Serializable]
    public class BallSpriteData{
        public List<BallData> BallDataList = new List<BallData>();

        public BallData getBallData(int index)
        {
            return BallDataList[index];
        }
    }
}