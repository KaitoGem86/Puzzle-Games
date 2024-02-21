using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BallSortQuest
{
    public class PlayerData : MonoBehaviour
    {
#if UNITY_EDITOR
        private static string path = "Assets/Game/Game-Ball-Sort-Quest/06. Data/JsonText/PlayerData/UserData.txt";
#else
     private static string path = Path.Combine(Application.persistentDataPath, "UserData.txt");
#endif
        private static UserData _userData;
        //public static UserData UserData = new UserData();

        private void Start()
        {
            InitData();
        }

        private void InitData()
        {
            //UserData
            if (!PlayerPrefs.HasKey(Const.KEY_USER_DATA))
            {
                UserData = new UserData();
                UserData.InitUserDataValue();
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
            {
                File.WriteAllText(path, saveData);
                Debug.LogError("Save");
            }
        }

        public static UserData UserData{
            get {
                if (_userData == null)
                {
                    LoadUserData();
                    //UserData.InitUserDataValue();
                }
                return _userData;
            }
            set{
                _userData = value;
            }
        }
    }
}