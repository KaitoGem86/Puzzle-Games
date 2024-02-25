using BallSortQuest;
using TMPro;
using UnityEngine;

public class GameUIManager : SingletonMonoBehaviour<GameUIManager>
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private BackgroundController _backgroundController;
    [SerializeField] private TMP_Text _labelText;
    [SerializeField] private GameObject _closeButton;
    [SerializeField] private GameObject _challengeButton;
    [SerializeField] private GameObject _menuButton;
    public override void Awake()
    {
        base.Awake();
        BallSortQuest.ActionEvent.OnResetGamePlay += ResetGamePlayWithMode;
        BallSortQuest.ActionEvent.OnResetGamePlay += UpdateLevelText;
    }

    // Start is called before the first frame update
    void Start()
    {
        _backgroundController.InitBackground();
        ResetGamePlayWithMode();
        UpdateLevelText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenChallengePopup()
    {
        PopupSystem.PopupManager.CreateNewInstance<PopupChallenge>().Show();
        BallSortQuest.GameManager.Instance.StateGameController.OnMenu();
    }

    public void OpenCloseChallengeModePopup(){
        PopupSystem.PopupManager.CreateNewInstance<PopupCloseChallengeMode>().Show("Bạn có muốn bỏ cuộc không?", TypeChallenge.None);
        // BallSortQuest.GameManager.Instance.GameModeController.OnCloseGameChallengeMode();
        // BallSortQuest.GameManager.Instance.GameModeController.CurrentGameMode = TypeChallenge.None;
        // BallSortQuest.ActionEvent.OnResetGamePlay?.Invoke();
    }

    public void OpenRewardPopup(){
        PopupSystem.PopupManager.CreateNewInstance<PopupGoldReward>().Show(TimeFromToGoogle.Instance.Now());
    }

    public void OpenResetGameModePopup(){
        PopupSystem.PopupManager.CreateNewInstance<PopupCloseChallengeMode>().Show("Dữ liệu game sẽ bị mất, bạn có muốn tiếp tục không?", BallSortQuest.GameManager.Instance.GameModeController.CurrentGameMode);
    }

    public void UpdateLevelText()
    {
        if(BallSortQuest.GameManager.Instance.GameModeController.CurrentGameMode == TypeChallenge.None)
            levelText.text = BallSortQuest.GameManager.Instance.Level.level.ToString();
    }

    private void ResetGamePlayWithMode(){
        switch (BallSortQuest.GameManager.Instance.GameModeController.CurrentGameMode)
        {
            case TypeChallenge.None:
                SetUpGamePlayScene(false);
                _labelText.text = "Level";
                _labelText.gameObject.SetActive(true);
                levelText.fontSize = 64;
                break;
            case TypeChallenge.Hidden:
                SetUpGamePlayScene(true);
                _labelText.gameObject.SetActive(false);
                levelText.text = "HIDDEN";
                break;
            case TypeChallenge.Move:
                SetUpGamePlayScene(true);
                _labelText.gameObject.SetActive(false);
                levelText.text = "";
                levelText.fontSize = 100;
                BallSortQuest.GameManager.Instance.GameModeController.MoveModeController.SetMoveText(levelText);
                break;
            case TypeChallenge.Timer:
                SetUpGamePlayScene(true);
                _labelText.gameObject.SetActive(false);
                levelText.text = "";
                levelText.fontSize = 100;
                BallSortQuest.GameManager.Instance.GameModeController.TimerModeController.SetTimerText(levelText);
                break;
        }
    }

    private void SetUpGamePlayScene(bool isChallenge){
        _menuButton.SetActive(!isChallenge);
        _challengeButton.SetActive(!isChallenge);
        _closeButton.SetActive(isChallenge);
    }
}
