using System.Collections;
using TMPro;
using UnityEngine;

namespace BallSortQuest
{
    public class TimerModeController
    {
        private int _currentTimer;
        private TMP_Text _timerText;

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

        public IEnumerator StartTimer()
        {
            Debug.Log("Start Timer " + _currentTimer );
            while (_currentTimer > 0)
            {
                yield return new WaitForSeconds(1);
                _timerText.text = _currentTimer.ToString();
                _currentTimer--;
            }
        }
    }
}