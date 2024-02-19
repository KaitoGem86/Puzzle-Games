using System.Collections;
using System.Collections.Generic;
using BallSortQuest;
using UnityEngine;

namespace BallSortQuest
{
    public class SFXTapController : MonoBehaviour
    {
        public void OnClickButtonUI()
        {
            SoundManager.Instance.PlayUIButtonClick();
        }
    }

}
