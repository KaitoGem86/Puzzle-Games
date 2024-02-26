using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BallSortQuest
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField] TextAsset _textData;
        [SerializeField] LevelDataSO _levelDataSO;

        public void ReadData()
        {
            _levelDataSO.LevelDatas.Clear();

            string dataJson = _textData.text;

            List<Dictionary<string, string>> datas = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(dataJson);

            if (datas != null & datas.Count > 0)
            {
                foreach (var data in datas)
                {
                    Level level = new Level(data);
                    _levelDataSO.AddData(level);
                }
                Debug.Log("Read Level Data Success");
            }

        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(LevelData))]
    public class LoadDataFromJson : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LevelData control = (LevelData)target;
            if (GUILayout.Button("READ ALL DATA FROM JSON"))
            {
                control.ReadData();
            }
        }
    }
#endif

    [System.Serializable]
    public class Level
    {
        public int level;
        public int tube;
        public int tubeSlot;
        public List<int> data;
        public int move;

        public Level(Dictionary<string, string> data)
        {
            if (!string.IsNullOrEmpty(data["level"]))
            {
                this.level = int.Parse(data["level"]);
            }

            if (!string.IsNullOrEmpty(data["branch"]))
            {
                this.tube = int.Parse(data["branch"]);
            }

            if (!string.IsNullOrEmpty(data["branchSlot"]))
            {
                this.tubeSlot = int.Parse(data["branchSlot"]);
            }

            this.data = new List<int>();
            if (!string.IsNullOrEmpty(data["data"]))
            {
                string[] datas = data["data"].Split(',');

                for (int i = 0; i < datas.Length; i++)
                {
                    this.data.Add(int.Parse(datas[i]));
                }
            }

            if (!string.IsNullOrEmpty(data["move"]))
            {
                this.move = int.Parse(data["move"]);
            }
        }
    }
}