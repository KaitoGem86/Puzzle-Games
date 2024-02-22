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
        public int ProcessValue;
        public int CurrentBackgroundIndex;
        public ChallengeState HiddenState;
        public ChallengeState TimerState;
        public ChallengeState MoveState;
        
        #endregion


        //Use if PlayerPref doesn't not contain Key is Const.USER_DATA
        public void InitUserDataValue(){
            StepToReachSpecialLevel = 1;
            HiddenState = ChallengeState.InComplete;
            TimerState = ChallengeState.InComplete;
            MoveState = ChallengeState.InComplete;
            ProcessValue = 0;
        }

        #region Method
        public void UpdateWinGameUserDataValue()
        {
            if (HighestLevel < DataManager.Instance.LevelDataSO.getListLevel() - 1){
                //Update highest level (increment)
                this.HighestLevel++;
                //Update step to reach special level(increment)
                this.StepToReachSpecialLevel = (this.StepToReachSpecialLevel + 1) % DataManager.Instance.StepToReachSpecialLevel;
                if (this.StepToReachSpecialLevel == 0)
                {
                    UnityEngine.Debug.Log("Special level");
                }

                //Update process value
                this.ProcessValue += BallSortQuest.DataManager.Instance.ProcessIncrementValue;
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