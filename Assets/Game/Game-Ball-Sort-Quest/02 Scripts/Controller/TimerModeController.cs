using System.Collections;
using TMPro;
using UnityEngine;

namespace BallSortQuest
{
    public class TimerModeController
    {
        private int _currentTimer;
        private TMP_Text _timerText;
        private bool _isInTimer;

        public TimerModeController() { }

        public TimerModeController(int maxTimer)
        {
            _currentTimer = maxTimer;
        }

        public void SetTimerText(TMP_Text timerText)
        {
            _timerText = timerText;
        }

        public void SetTimer(int maxTimer)
        {
            _currentTimer = maxTimer;
        }

        public void OnCompleteTimer()
        {
            Debug.Log("On Complete Timer");
            //Do something with GamePlay as set the GamePlayMode enum ???
            //GameManager.Instance.GameModeController.SetGameMode(TypeChallenge.None);
            _isInTimer = false;
        }

        public IEnumerator StartTimer()
        {
            Debug.Log("Start Timer " + _currentTimer );
            _isInTimer = true;
            while (_isInTimer)
            {
                yield return new WaitForSeconds(1);
                _timerText.text = _currentTimer.ToString();
                _currentTimer--;
                if (_currentTimer <= 0)
                {
                    _isInTimer = false;
                }
            }
            _timerText.text = "0";
            Debug.Log("End Timer");
        }
    }
}