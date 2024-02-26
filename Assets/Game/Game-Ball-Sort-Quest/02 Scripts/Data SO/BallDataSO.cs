using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest
{
    [CreateAssetMenu(fileName = "Ball Datas", menuName = "Data SO/Ball Datas")]
    public class BallDataSO : ScriptableObject
    {
        public List<BallData> BallDataList = new List<BallData>();

        public BallData getBallData(int index)
        {
            return BallDataList[index];
        }
    }
}