using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        [field: SerializeField] public Level Level { get; private set; }
        public DataManager Datamanager { get; private set; }
        private UserData _userData;
        [SerializeField] GamePlayManager _gamePlayManager;
        public GamePlayManager GamePlayManager => _gamePlayManager;
        public StateGameController StateGameController;


        #region Unity Method
        public override void Awake()
        {
            Datamanager = DataManager.Instance;
            _userData = PlayerData.UserData;

            ActionEvent.OnResetGamePlay += InitLevel;

            InitLevel();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Win();
            }
        }

        private void OnDestroy()
        {
            ActionEvent.OnResetGamePlay -= InitLevel;
        }
        #endregion

        public void InitLevel()
        {
            //  Debug.Log($"So luong level:{Datamanager.LevelDataSO.getListLevel()}");
            //Debug.LogError($"Current Highest Level: {_userData.HighestLevel}");
            StateGameController.Playing();
            this.Level = Datamanager.LevelDataSO.getLevel(_userData.HighestLevel);
            Debug.Log($"Current Level: {this.Level.level}");
        }

        public void Win()
        {
            StateGameController.Win();
            _userData.UpdateHighestLevel();
            SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("victory1"));
            //PopupWin.Instance.Show();
            PopupSystem.PopupManager.CreateNewInstance<PopupWin>().Show();
        }

        public void OnClickReplay()
        {
            ActionEvent.OnResetGamePlay?.Invoke();
        }
    }
}