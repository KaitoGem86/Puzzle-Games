using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest
{
    public class DataManager : SingletonMonoBehaviour<DataManager>
    {
        public LevelDataSO LevelDataSO;
        public BallDataSO BallDataSO;

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