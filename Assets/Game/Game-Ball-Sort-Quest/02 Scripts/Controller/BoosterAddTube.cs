using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BallSortQuest
{
    public class BoosterAddTube : BoosterController
    {
        private bool _isOpenAds = false;
        private void Awake()
        {
            ActionEvent.OnResetGamePlay += ResetQuantity;
        }

        public void Start()
        {
            UpdateBoosterQuantity(true);
        }

        public void OnClickAddTube()
        {
            if(_isOpenAds){
                Debug.Log("Show ads");
                //Do something
            }
            else{
                UpdateBoosterQuantity();
            }
            ActionEvent.OnUserBoosterAdd?.Invoke();
        }

        private void ResetQuantity(){
            if(PlayerData.UserData.HighestLevel < 50){
                UpdateBoosterQuantity(true);
            }
            else{
                //Do something
            }
        }

        private void UpdateBoosterQuantity(bool isReset = false){
            int quantity = PlayerData.UserData.BoosterAddNumber;
            if(isReset){
                quantity = -1;
            }
            else{
                quantity -= 1;
            }
            _isOpenAds = quantity < -1;
            PlayerData.UserData.UpdateValueBooster(this.Type, quantity - PlayerData.UserData.BoosterAddNumber);
            base.DisplayBooster(PlayerData.UserData.BoosterAddNumber);
            Debug.Log("Quantity: " + PlayerData.UserData.BoosterAddNumber);
        }
    }
}