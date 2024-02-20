using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace BallSortQuest
{
    public class UserData
    {
        #region Varriables
        /// <summary>
        /// Achievements
        /// </summary>
        public int HighestLevel;
        /// <summary>
        /// Booster
        /// </summary>
        public int BoosterRevokeNumber;
        public int BoosterAddNumber;
        public int CoinNumber;
        #endregion

        #region Method
        public void UpdateHighestLevel()
        {
            if (HighestLevel < DataManager.Instance.LevelDataSO.getListLevel() - 1){
                this.HighestLevel++;
                UnityEngine.Debug.Log($"Highest Level: {this.HighestLevel}");
            }
                
        }

        public void UpdateValueBooster(TypeBooster type, int value)
        {
            switch (type)
            {
                case TypeBooster.Revoke:
                    this.BoosterRevokeNumber += value;
                    break;
                case TypeBooster.AddTube:
                    this.BoosterAddNumber += value;
                    break;
            }
        }
        #endregion
    }
}