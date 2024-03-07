using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTapController : SingletonMonoBehaviour<SFXTapController>
{
    public void OnClickButtonUI()
    {
        if (BallSortQuest.PlayerData.UserData.IsSoundOn)
            SoundManager.Instance.PlayUIButtonClick();
    }
}
