using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : SingletonMonoBehaviour<GameUIManager>
{
    [SerializeField] private TMP_Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelText(BallSortQuest.GameManager.Instance.Level.level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLevelText(int level){
        levelText.text = level.ToString();
    }
}
