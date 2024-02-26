using UnityEngine;

namespace BallSortQuest{
    public class GameModeController {
        private TypeChallenge _currentGameMode;

        private TimerModeController _timerModeController;
        private MoveModeController _moveModeController;
        private HiddenModeController _hiddenModeController;

        public GameModeController(){
            _currentGameMode = TypeChallenge.None;
            _timerModeController = new TimerModeController();
            _moveModeController = new MoveModeController();
            _hiddenModeController = new HiddenModeController();
        }

        public void SetGameMode(TypeChallenge type){
            _currentGameMode = type;
        }

        //Use when start developing flow game
        public void OnGameModeComplete(){
            PlayerData.UserData.UpdateCompleteChallengeMode(_currentGameMode);
            if(_currentGameMode == TypeChallenge.Timer){
                _timerModeController.OnCompleteTimer(isModeComplete: true);
            }
        }

        public void OnCloseGameMode(){
            if(_currentGameMode == TypeChallenge.Hidden){
                PlayerData.UserData.HiddenState = ChallengeState.InComplete;
            }
            else if(_currentGameMode == TypeChallenge.Timer){
                PlayerData.UserData.TimerState = ChallengeState.InComplete;
                _timerModeController.OnCompleteTimer(isModeComplete: false);
            }
            else if(_currentGameMode == TypeChallenge.Move){
                PlayerData.UserData.MoveState = ChallengeState.InComplete;
            }
            else {
                Debug.Log("GameModeController: OnCloseGameChallengeMode: GameMode Normal");
            }
        }

        public void OnGameModeFail(){

        }

        public TypeChallenge CurrentGameMode { get => _currentGameMode; set => _currentGameMode = value; }
        public TimerModeController TimerModeController { get => _timerModeController; }
        public MoveModeController MoveModeController { get => _moveModeController; }
        public HiddenModeController HiddenModeController { get => _hiddenModeController; }
    }
}