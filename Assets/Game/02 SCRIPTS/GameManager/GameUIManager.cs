using BallSortQuest;
using TMPro;
using UnityEngine;

public class GameUIManager : SingletonMonoBehaviour<GameUIManager>
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private BackgroundController _backgroundController;
    public override void Awake()
    {
        base.Awake();
        BallSortQuest.ActionEvent.OnResetGamePlay += UpdateLevelText;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelText();
        _backgroundController.InitBackground();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenChallengePopup()
    {
        PopupSystem.PopupManager.CreateNewInstance<PopupChallenge>().Show();
    }

    public void UpdateLevelText()
    {
        levelText.text = BallSortQuest.GameManager.Instance.Level.level.ToString();
        _backgroundController.InitBackground();
    }
}
