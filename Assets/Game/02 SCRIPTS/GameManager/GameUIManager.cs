using BallSortQuest;
using TMPro;
using UnityEngine;

public class GameUIManager : SingletonMonoBehaviour<GameUIManager>
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private BackgroundController _backgroundController;
    [SerializeField] private GameObject _safeAreaCanvas;
    [SerializeField] private TMP_Text _tutorialText;
    [SerializeField] private GameObject _closeButton;
    [SerializeField] private GameObject _challengeButton;
    [SerializeField] private GameObject _menuButton;
    [SerializeField] private ParticleSystem _topParticle;
    [SerializeField] private ParticleSystem _leftParticle;
    [SerializeField] private ParticleSystem _rightParticle;

    private Vector3 _originalTopParticleScale;
    private Vector3 _originalLeftParticleScale;
    private Vector3 _originalRightParticleScale;

    public override void Awake()
    {
        base.Awake();
        BallSortQuest.ActionEvent.OnResetGamePlay += ResetGamePlayWithMode;
        BallSortQuest.ActionEvent.OnResetGamePlay += UpdateLevelText;
        BallSortQuest.ActionEvent.OnSelectShopBackground += InitBackground;
    }

    public void OnDestroy()
    {
        BallSortQuest.ActionEvent.OnResetGamePlay -= ResetGamePlayWithMode;
        BallSortQuest.ActionEvent.OnResetGamePlay -= UpdateLevelText;
        BallSortQuest.ActionEvent.OnSelectShopBackground -= InitBackground;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitBackground();
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

    public void OpenCloseChallengeModePopup()
    {
        PopupSystem.PopupManager.CreateNewInstance<PopupCloseChallengeMode>().Show("Bạn có muốn bỏ cuộc không?", TypeChallenge.None);
    }

    public void OpenRewardPopup()
    {
        PopupSystem.PopupManager.CreateNewInstance<PopupGoldReward>().Show(TimeFromToGoogle.Instance.Now());
    }

    public void OpenRankingPopup()
    {
        PopupSystem.PopupManager.CreateNewInstance<NotificationPopup>().Show("Chức năng đang phát triển");
    }

    public void OpenResetGameModePopup()
    {
        PopupSystem.PopupManager.CreateNewInstance<PopupCloseChallengeMode>().Show("Dữ liệu game sẽ bị mất, bạn có muốn tiếp tục không?", BallSortQuest.GameManager.Instance.GameModeController.CurrentGameMode);
    }

    public void OpenMenuPopup()
    {
        PopupSystem.PopupManager.CreateNewInstance<MenuPanel>().Show();
    }

    private void UpdateLevelText()
    {
        if (BallSortQuest.GameManager.Instance.GameModeController.CurrentGameMode == TypeChallenge.None)
            levelText.text +=BallSortQuest.GameManager.Instance.Level.level.ToString();
    }



    /// <summary>
    /// Set particle size follow camera size. Default size is setted when camera size is 5
    /// </summary>
    /// <param name="cameraSize"></param>
    public void SetParticleSize(float cameraSize)
    {
        InitializeParticleSizes();
        // _topParticle.transform.localScale  = cameraSize / 5 * _originalTopParticleScale;
        // _leftParticle.transform.localScale = cameraSize / 5 * _originalLeftParticleScale;
        // _rightParticle.transform.localScale = cameraSize / 5 * _originalRightParticleScale;
    }

    private void InitializeParticleSizes()
    {
        _originalTopParticleScale = _topParticle.transform.localScale;
        _originalLeftParticleScale = _leftParticle.transform.localScale;
        _originalRightParticleScale = _rightParticle.transform.localScale;
    }

    public void PlayBorderParticle()
    {
        _topParticle.Play();
        _leftParticle.Play();
        _rightParticle.Play();
    }

    private void ResetGamePlayWithMode()
    {
        switch (BallSortQuest.GameManager.Instance.GameModeController.CurrentGameMode)
        {
            case TypeChallenge.None:
                if (BallSortQuest.GameManager.Instance.Level.level == 1)
                {
                    _safeAreaCanvas.SetActive(false);
                    _tutorialText.text = "Vui lòng chọn nước để đổ vào chai thủy tinh";
                    return;
                }
                _safeAreaCanvas.SetActive(true);
                if (BallSortQuest.GameManager.Instance.Level.level == 2)
                {
                    _safeAreaCanvas.SetActive(false);
                    _tutorialText.text = "Bạn có thể đổ nước lên trên màu giống nhau";
                }
                else
                {
                    _tutorialText.gameObject.SetActive(false);
                }
                SetUpGamePlayScene(false);
                levelText.fontSize = 72;
                break;
            case TypeChallenge.Hidden:
                SetUpGamePlayScene(true);
                levelText.text = "HIDDEN";
                levelText.fontSize = 72;
                break;
            case TypeChallenge.Move:
                SetUpGamePlayScene(true);
                levelText.text = "";
                levelText.fontSize = 100;
                BallSortQuest.GameManager.Instance.GameModeController.MoveModeController.SetMoveText(levelText);
                break;
            case TypeChallenge.Timer:
                SetUpGamePlayScene(true);
                levelText.text = "";
                levelText.fontSize = 100;
                BallSortQuest.GameManager.Instance.GameModeController.TimerModeController.SetTimerText(levelText);
                break;
        }
    }

    private void SetUpGamePlayScene(bool isChallenge)
    {
        _menuButton.SetActive(!isChallenge);
        _challengeButton.SetActive(!isChallenge);
        _closeButton.SetActive(isChallenge);
    }

    private void InitBackground()
    {
        _backgroundController.InitBackground();
    }
}
