using System.Collections;
using PopupSystem;
using TMPro;
using UnityEngine;

namespace BallSortQuest{
    public class PopupGoldReward : BasePopup{
        [SerializeField] private TMP_Text _timerText;

        private int _timer;

        public void Show(){
            base.Show();
            Debug.Log("Time Show Popup " + TimeFromToGoogle.Instance.Now() );
        }

        public void Show(int timer){
            base.Show();
            _timer = timer;
        }

        public void Close(){
            base.Hide();
        }

        private IEnumerator StartTimer(){
            yield return new WaitForSeconds(3);
        }
    }
}