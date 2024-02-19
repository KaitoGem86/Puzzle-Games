using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private static string path = "Assets/Game/05 Data/JsonText/PlayerData/UserData.txt";

    public static UserData UserData = new UserData();

    private void Start()
    {
        InitData();
    }

    private void InitData()
    {
        //UserData
        if (!PlayerPrefs.HasKey(Const.KEY_USER_DATA))
        {
            SaveUserData();
            if (!File.Exists(path))
            {
                // Create a file to write to.
                File.Create(path);
            }
        }
        else
        {
            LoadUserData();
        }
    }

    public static void LoadUserData()
    {
        var saveData = PlayerPrefs.GetString(Const.KEY_USER_DATA);
        var data = JsonUtility.FromJson<UserData>(saveData);
        UserData = data;
    }

    public static void SaveUserData()
    {
        string saveData = JsonUtility.ToJson(UserData);
        PlayerPrefs.SetString(Const.KEY_USER_DATA, saveData);

        File.WriteAllText(path, saveData);
    }
}
