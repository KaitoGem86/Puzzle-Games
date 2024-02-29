using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest
{
    public class DataManager : SingletonMonoBehaviour<DataManager>
    {
        public LevelDataSO  LevelDataSO;
        public LevelDataSO ChallengeLevelDataSO;
        public BallDataSO BallDataSO;
        public ListBallDataSO ListBallDataSO;
        public BackgroundDatas BackgroundDatas;
        public TubeDataSO TubeDatas;
        public int StepToReachSpecialLevel;
        public int ProcessIncrementValue;

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                Debug.Log("Is Pause Game");
                PlayerData.SaveUserData();
            }
        }

        private void OnApplicationQuit()
        {
            Debug.Log("Is Quit Game");
            PlayerData.SaveUserData();
        }
    }
}