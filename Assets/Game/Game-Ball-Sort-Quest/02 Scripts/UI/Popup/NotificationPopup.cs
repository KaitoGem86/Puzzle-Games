using PopupSystem;
using TMPro;
using UnityEngine;

namespace BallSortQuest
{
    public class NotificationPopup : BasePopup
    {
        [SerializeField] private TMP_Text _title;
        
        public void Show(string title)
        {
            base.Show();
            _title.text = title;
        }

        public void Close()
        {
            base.Hide();
        }
    }

}
