using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest
{
    public class BoosterAddTube : BoosterController
    {
        public void Start()
        {
            base.DisplayBooster(PlayerData.UserData.BoosterAddNumber);
        }

        public void OnClickAddTube()
        {
            ActionEvent.OnUserBoosterAdd?.Invoke();
            DisplayBooster(PlayerData.UserData.BoosterAddNumber);
        }
    }
}