using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Public Methods
    public void OpenAdditionalCup()
    {
        PopupAdditionalCup.Instance.Open();
    }
    #endregion
}
