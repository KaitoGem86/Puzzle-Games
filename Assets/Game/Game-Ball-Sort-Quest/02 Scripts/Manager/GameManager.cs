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
        public GameModeController GameModeController;


        #region Unity Method
        public override void Awake()
        {
            Datamanager = DataManager.Instance;
            _userData = PlayerData.UserData;

            ActionEvent.OnResetGamePlay += InitLevel;
            GameModeController = new GameModeController();

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

        public void

        InitLevel()
        {
            StateGameController.Playing();
            if (GameModeController.CurrentGameMode == TypeChallenge.None)
                this.Level = Datamanager.LevelDataSO.getLevel(_userData.HighestLevel);
            else
            {
                Debug.Log("Challenge Mode" + _userData.HighestChallengeLevel);
                this.Level = Datamanager.ChallengeLevelDataSO.getLevel(_userData.HighestChallengeLevel);
            }
        }

        public void Win()
        {
            StateGameController.Win();
            _userData.UpdateWinGameUserDataValue();
            SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("victory1"));
            //PopupWin.Instance.Show();
            if (GameModeController.CurrentGameMode == TypeChallenge.None)
                PopupSystem.PopupManager.CreateNewInstance<PopupWin>().Show();
            else
            {
                PopupSystem.PopupManager.CreateNewInstance<PopupWinChallenge>().Show();
                //Debug.Log("Win Challenge");
            }
            GameModeController.OnGameModeComplete();
        }

        public void OnClickReplay()
        {
            ActionEvent.OnResetGamePlay?.Invoke();
        }
    }
}