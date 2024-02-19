using PopupSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupAdditionalCup : SingletonPopup<PopupAdditionalCup>
{
    public void Open()
    {
        base.Show();
    }

    public void Close()
    {
        base.Hide();
    }
}
