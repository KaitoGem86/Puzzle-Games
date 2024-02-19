using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("Is Pause Game");
            // PlayerData.SaveUserData();
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Is Quit Game");
        PlayerData.SaveUserData();
    }
}
