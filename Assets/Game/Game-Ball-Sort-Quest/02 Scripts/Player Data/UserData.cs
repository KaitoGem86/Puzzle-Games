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
        public int StepToReachSpecialLevel;
        #endregion

        #region Method
        public void UpdateHighestLevel()
        {
            if (HighestLevel < DataManager.Instance.LevelDataSO.getListLevel() - 1){
                this.HighestLevel++;
                this.StepToReachSpecialLevel = (this.StepToReachSpecialLevel + 1) % DataManager.Instance.StepToReachSpecialLevel;
                if (this.StepToReachSpecialLevel == 0)
                {
                    UnityEngine.Debug.Log("Special level");
                }
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