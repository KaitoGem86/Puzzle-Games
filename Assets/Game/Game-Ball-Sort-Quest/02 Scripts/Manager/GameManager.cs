using System.Threading.Tasks;
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

        public void InitLevel()
        {
            AdsManager.Instance.ShowBanner();
            StateGameController.Playing();
            if (GameModeController.CurrentGameMode == TypeChallenge.None)
                this.Level = Datamanager.LevelDataSO.getLevel(_userData.HighestLevel);
            else
            {
                Debug.Log("Challenge Mode" + _userData.HighestChallengeLevel);
                this.Level = Datamanager.ChallengeLevelDataSO.getLevel(_userData.HighestChallengeLevel);
            }
            GlobalEventManager.OnLevelPlay(this.Level.level);
            GlobalEventManager.OnLevelReplay(this.Level.level);
        }

        public async void Win()
        {
            StateGameController.Win();
            _userData.UpdateWinGameUserDataValue();
            if (PlayerData.UserData.IsSoundOn)
                SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("victory1"));
            //PopupWin.Instance.Show();
            if (GameModeController.CurrentGameMode == TypeChallenge.None)
            {
                await Task.Delay(800);
                PopupSystem.PopupManager.CreateNewInstance<PopupWin>().Show();
            }
            else
            {
                await Task.Delay(800);
                PopupSystem.PopupManager.CreateNewInstance<PopupWinChallenge>().Show();
                //Debug.Log("Win Challenge");
            }
            GameModeController.OnGameModeComplete();
            GlobalEventManager.OnLevelComplete(this.Level.level);
        }

        public void OnClickReplay()
        {
            switch (GameModeController.CurrentGameMode)
            {
                case TypeChallenge.None:
                    ActionEvent.OnResetGamePlay?.Invoke();
                    break;
                case TypeChallenge.Hidden:
                    ActionEvent.OnResetGamePlay?.Invoke();
                    break;
                case TypeChallenge.Move:
                    ActionEvent.OnResetGamePlay?.Invoke();
                    break;
                case TypeChallenge.Timer:
                    StopCoroutine(GameModeController.TimerModeController.StartTimer());
                    ActionEvent.OnResetGamePlay?.Invoke();
                    break;
            }
        }
    }
}