using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BallSortQuest
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        [Header("REFFERENCE")]
        [SerializeField] TMP_Text _levelText;

        #region Unity Method
        public override void Awake()
        {
            ActionEvent.OnResetGamePlay += DisplayLeveText;
        }

        // Start is called before the first frame update
        void Start()
        {
            DisplayLeveText();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            ActionEvent.OnResetGamePlay -= DisplayLeveText;
        }
        #endregion

        private void DisplayLeveText()
        {
            _levelText.text = $"{PlayerData.UserData.HighestLevel + 1:00}";
        }
    }
}