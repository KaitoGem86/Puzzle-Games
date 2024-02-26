using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest
{

    [CreateAssetMenu(fileName = "Leve Data", menuName = "Data SO/LevelData", order = 1)]
    public class LevelDataSO : ScriptableObject
    {
        public List<Level> LevelDatas = new List<Level>();

        public void AddData(Level level)
        {
            this.LevelDatas.Add(level);
        }

        public Level getLevel(int index)
        {
            return this.LevelDatas[index];
        }

        public int getListLevel()
        {
            return this.LevelDatas.Count;
        }
    }
}