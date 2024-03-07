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
                _timerText.text = ConvertToTime(_currentTimer);
                if (_currentTimer <= 0)
                {
                    _isInTimer = false;
                    Debug.Log("Time Out");
                }
                _currentTimer--;
                yield return new WaitForSeconds(1);
            }
            //_timerText.text = ConvertToTime(_currentTimer);
            if (!_isModeComplete && _currentTimer <= 0)
                PopupSystem.PopupManager.CreateNewInstance<PopupCloseChallengeMode>().Show(
                    //"Hết thời gian \n Thử lại?"
                    GameLanguage.Get("txt_out_of_time")
                    , TypeChallenge.Timer, true);
        }

        public bool IsInTimer { get => _isInTimer; }

        private string ConvertToTime(int time)
        {
            int minutes = time / 60;
            int seconds = time % 60;
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}