using PopupSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest
{
    public class PopupWin : SingletonPopup<PopupWin>
    {
        public void Show()
        {
            base.Show();
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
    }
}