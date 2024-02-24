using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGameController : MonoBehaviour
{
    public StateGame State;

    public void Playing()
    {
        this.State = StateGame.Playing;
    }

    public void Win()
    {
        this.State = StateGame.Win;
    }

    public void OnMenu()
    {
        this.State = StateGame.OnMenu;
    }

    private void Lose()
    {
        this.State = StateGame.Lose;
    }

    private void Pause()
    {
        this.State = StateGame.Pause;
    }


}

public enum StateGame
{
    Playing,
    Win,
    Lose,
    Pause,
    OnMenu,
}
