using TMPro;
using UnityEngine;

public class GameUIManager : SingletonMonoBehaviour<GameUIManager>
{
    [SerializeField] private TMP_Text levelText;
    public override void Awake()
    {
        base.Awake();
        BallSortQuest.ActionEvent.OnResetGamePlay += UpdateLevelText;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLevelText(){
        levelText.text = BallSortQuest.GameManager.Instance.Level.level.ToString();
    }
}
