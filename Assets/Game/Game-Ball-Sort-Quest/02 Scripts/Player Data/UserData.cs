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
        public int HighestChallengeLevel;
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
        public void InitUserDataValue()
        {
            HighestLevel = 0;
            HighestChallengeLevel = 0;
            StepToReachSpecialLevel = 1;
            HiddenState = ChallengeState.InComplete;
            TimerState = ChallengeState.InComplete;
            MoveState = ChallengeState.InComplete;
            ProcessValue = 0;
        }

        #region Method
        public void UpdateWinGameUserDataValue()
        {
            if (GameManager.Instance.GameModeController.CurrentGameMode == TypeChallenge.None)
                if (HighestLevel < DataManager.Instance.LevelDataSO.getListLevel() - 1)
                {
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
            else
                if (HighestChallengeLevel < DataManager.Instance.ChallengeLevelDataSO.getListLevel() - 1)
                {
                    this.HighestChallengeLevel++;
                }
        }

        public void UpdateWinChallengeUserDataValue(TypeChallenge type)
        {
            switch (type)
            {
                case TypeChallenge.Hidden:
                    this.HiddenState = ChallengeState.Success;
                    break;
                case TypeChallenge.Timer:
                    this.TimerState = ChallengeState.Success;
                    break;
                case TypeChallenge.Move:
                    this.MoveState = ChallengeState.Success;
                    break;
            }
            if (HighestChallengeLevel < DataManager.Instance.ChallengeLevelDataSO.getListLevel() - 1)
            {
                this.HighestChallengeLevel++;
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