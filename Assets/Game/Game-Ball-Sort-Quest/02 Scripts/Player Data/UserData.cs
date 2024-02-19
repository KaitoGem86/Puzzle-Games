using System.Collections;
using System.Collections.Generic;

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
        #endregion

        #region Method
        public void UpdateHighestLevel()
        {
            if (HighestLevel < DataManager.Instance.LevelDataSO.getListLevel() - 1)
                this.HighestLevel++;
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