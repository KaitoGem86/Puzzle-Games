using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    #region Varriables
    /// <summary>
    /// Achievements
    /// </summary>
    public int HighestLevel;
    #endregion

    #region Method
    public void UpdateHighestLevel()
    {
        //if (HighestLevel < DataManager.Instance.LevelDataSO.getListLevel() - 1)
        //    this.HighestLevel++;
    }
    #endregion
}
