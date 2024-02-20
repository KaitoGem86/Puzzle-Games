using PopupSystem;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

namespace BallSortQuest
{
    public class PopupWin : SingletonPopup<PopupWin>
    {

        [Space,Header("PopUp Elements")]
        [SerializeField] private GameObject _firstGroupButton;
        [SerializeField] private GameObject _secondGroupButton;
        [SerializeField] private GameObject[] _stars = new GameObject[3];
        public void Show()
        {
            base.Show();
            _secondGroupButton.SetActive(false);
            _firstGroupButton.SetActive(true);
            ActiveStar(3);
        }

        public void Close()
        {
            base.Hide();
        }

        public void OnClickNextLevel()
        {
            ActionEvent.OnResetGamePlay?.Invoke();
            Close();
        }

        public void OnClickAccept(){
            _firstGroupButton.SetActive(false);
            _secondGroupButton.SetActive(true);
        }

        private void ActiveStar(int star){
            for (int i = 0; i < _stars.Length; i++)
            {
                _stars[i].SetActive(i < star);
            }
        }
    }
}