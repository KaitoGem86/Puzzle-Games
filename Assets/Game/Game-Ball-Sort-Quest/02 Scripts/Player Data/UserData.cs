using System;
using System.Threading;
using UnityEngine;
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
        public int CurrentTubeIndex;
        public int CurrentBallIndex;
        public ChallengeState HiddenState;
        public ChallengeState TimerState;
        public ChallengeState MoveState;
        public string LastTimeGetReward;
        public string LastTimeCompleteHidden;
        public string LastTimeCompleteTimer;
        public string LastTimeCompleteMove;
        public bool IsCanCollectAllReward;
        public string ShopPurchaseData;

        public bool IsSoundOn;
        public bool IsVibrateOn;
        #endregion


        //Use if PlayerPref doesn't not contain Key is Const.USER_DATA
        public void InitUserDataValue()
        {
            Debug.Log("InitUserDataValue");
            HighestLevel = 0;
            HighestChallengeLevel = 0;
            StepToReachSpecialLevel = 1;
            HiddenState = ChallengeState.InComplete;
            TimerState = ChallengeState.InComplete;
            MoveState = ChallengeState.InComplete;
            ProcessValue = 0;
            LastTimeGetReward = TimeFromToGoogle.Instance.Now().AddHours(-1).ToString();
            LastTimeCompleteHidden = TimeFromToGoogle.Instance.Now().AddDays(-1).ToString();
            LastTimeCompleteTimer = TimeFromToGoogle.Instance.Now().AddDays(-1).ToString();
            LastTimeCompleteMove = TimeFromToGoogle.Instance.Now().AddDays(-1).ToString();
            IsSoundOn = true;
            IsVibrateOn = true;
            CurrentBackgroundIndex = 0;
            CurrentTubeIndex = 0;
            CurrentBallIndex = 0;
            AddPurchaseData(TypeItem.Background, 0);
            AddPurchaseData(TypeItem.Tube, 0);
            AddPurchaseData(TypeItem.Ball, 0);
        }

        #region Method
        public void UpdateWinGameUserDataValue()
        {
            if (GameManager.Instance.GameModeController.CurrentGameMode == TypeChallenge.None)
            {


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
            }
            else
            {
                if (HighestChallengeLevel < DataManager.Instance.ChallengeLevelDataSO.getListLevel() - 1)
                {
                    this.HighestChallengeLevel++;
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

        public void UpdateCompleteChallengeMode(TypeChallenge type){
            switch (type)
            {
                case TypeChallenge.Hidden:
                    this.HiddenState = ChallengeState.Success;
                    this.LastTimeCompleteHidden = TimeFromToGoogle.Instance.Now().ToString();
                    break;
                case TypeChallenge.Timer:
                    this.TimerState = ChallengeState.Success;
                    this.LastTimeCompleteTimer = TimeFromToGoogle.Instance.Now().ToString();
                    break;
                case TypeChallenge.Move:
                    this.MoveState = ChallengeState.Success;
                    this.LastTimeCompleteMove = TimeFromToGoogle.Instance.Now().ToString();
                    break;
                default:
                    break;
            }
        }

        public bool IsCanPlayChallengeWithNoAds(TypeChallenge type, DateTime time){
            switch (type)
            {
                case TypeChallenge.Hidden:
                    if(HiddenState.Equals(ChallengeState.Success) && time.DayOfYear == DateTime.Parse(LastTimeCompleteHidden).DayOfYear)
                        return false;
                    return true;
                case TypeChallenge.Timer:
                    if(TimerState.Equals(ChallengeState.Success) && time.DayOfYear == DateTime.Parse(LastTimeCompleteTimer).DayOfYear)
                        return false;
                    return true; 
                case TypeChallenge.Move:
                    if(MoveState.Equals(ChallengeState.Success) && time.DayOfYear == DateTime.Parse(LastTimeCompleteMove).DayOfYear)
                        return false;
                    return true;
                default:
                    return true;
            }
        }

        public void UpdateStateChallengeAfterADay(DateTime time){
            if(time.DayOfYear != DateTime.Parse(LastTimeCompleteHidden).DayOfYear){
                HiddenState = ChallengeState.InComplete;
                IsCanCollectAllReward = true;
            }
            if(time.DayOfYear != DateTime.Parse(LastTimeCompleteTimer).DayOfYear){
                TimerState = ChallengeState.InComplete;
                IsCanCollectAllReward = true;
            }
            if(time.DayOfYear != DateTime.Parse(LastTimeCompleteMove).DayOfYear){
                MoveState = ChallengeState.InComplete;
                IsCanCollectAllReward = true;
            }
        }

        public ShopPurchaseData GetShopPurchaseData(){
            return JsonUtility.FromJson<ShopPurchaseData>(ShopPurchaseData);
        }

        public void AddPurchaseData(TypeItem typeItem, int index){
            ShopPurchaseData data = GetShopPurchaseData();
            if(data == null){
                data = new ShopPurchaseData();
            }
            switch (typeItem)
            {
                case TypeItem.Tube:
                    data.PurchasedTubeIndexs.Add(index);
                    break;
                case TypeItem.Background:
                    data.PurchasedBackgroundIndexs.Add(index);
                    break;
                case TypeItem.Ball:
                    data.PurchasedBallIndexs.Add(index);
                    break;
                default:
                    throw new Exception("Type item not found: " + typeItem.ToString());
            }
            ShopPurchaseData = JsonUtility.ToJson(data);
        }

        public void SelectShop(TypeItem typeItem, int index){
            switch (typeItem)
            {
                case TypeItem.Tube:
                    CurrentTubeIndex = index;
                    ActionEvent.OnSelectShopTube?.Invoke();
                    break;
                case TypeItem.Background:
                    CurrentBackgroundIndex = index;
                    ActionEvent.OnSelectShopBackground?.Invoke();
                    break;
                case TypeItem.Ball:
                    CurrentBallIndex = index;
                    ActionEvent.OnSelectShopBall?.Invoke();
                    break;
                default:
                    throw new Exception("Type item not found: " + typeItem.ToString());
            }
        }
        #endregion
    }
}