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
        private bool _isModeComplete;

        public TimerModeController() { }

        public void SetTimerText(TMP_Text timerText)
        {
            _timerText = timerText;
        }

        public void SetTimer(int maxTimer)
        {
            _currentTimer = maxTimer;
        }

        public void OnCompleteTimer(bool isModeComplete)
        {
            Debug.Log("On Complete Timer");
            //Do something with GamePlay as set the GamePlayMode enum ???
            //GameManager.Instance.GameModeController.SetGameMode(TypeChallenge.None);
            _isInTimer = false;
            _isModeComplete = isModeComplete;
        }

        public IEnumerator StartTimer()
        {
            Debug.Log("Start Timer " + _currentTimer);
            _isInTimer = true;
            while (_isInTimer)
            {
                _timerText.text = _currentTimer.ToString();
                _currentTimer--;
                if (_currentTimer <= 0)
                {
                    _isInTimer = false;
                }
                yield return new WaitForSeconds(1);
            }
            if (!_isModeComplete)
                PopupSystem.PopupManager.CreateNewInstance<PopupCloseChallengeMode>().Show("Hết thời gian \n Thử lại?", TypeChallenge.Timer, true);
        }

        public bool IsInTimer { get => _isInTimer; }
    }
}