using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTapController : MonoBehaviour
{
    public void OnClickButtonUI()
    {
        SoundManager.Instance.PlayUIButtonClick();
    }
}
