using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest
{
    public class BoosterRevoke : BoosterController
    {
        private void Awake()
        {
            ActionEvent.OnResetGamePlay += UpdateQuantily;
        }

        public void Start()
        {
            UpdateQuantily();
        }

        private void OnDestroy()
        {
            ActionEvent.OnResetGamePlay -= UpdateQuantily;
        }

        public void OnClickRevoke()
        {
            if (PlayerData.UserData.BoosterRevokeNumber > 0)
            {
                ActionEvent.OnUseBoosterRevoke?.Invoke();
                DisplayBooster(PlayerData.UserData.BoosterRevokeNumber);
            }
            else
            {
                Debug.Log("Show ads");
            }
        }

        private void UpdateQuantily()
        {
            int quatily = PlayerData.UserData.BoosterRevokeNumber;
            PlayerData.UserData.UpdateValueBooster(this.Type, 5 - quatily);
            base.DisplayBooster(PlayerData.UserData.BoosterRevokeNumber);
        }
    }
}