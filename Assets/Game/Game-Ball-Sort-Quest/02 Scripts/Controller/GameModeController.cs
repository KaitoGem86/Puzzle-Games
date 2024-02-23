using UnityEngine;

namespace BallSortQuest{
    public class GameModeController {
        private TypeChallenge _currentGameMode;

        private TimerModeController _timerModeController;

        public GameModeController(){
            _currentGameMode = TypeChallenge.None;
            _timerModeController = new TimerModeController();
        }

        public void SetGameMode(TypeChallenge type){
            _currentGameMode = type;
        }

        public void OnGameModeComplete(){
            if(_currentGameMode == TypeChallenge.Hidden){
                PlayerData.UserData.HiddenState = ChallengeState.Success;
            }
            else if(_currentGameMode == TypeChallenge.Timer){
                PlayerData.UserData.TimerState = ChallengeState.Success;
            }
            else if(_currentGameMode == TypeChallenge.Move){
                PlayerData.UserData.MoveState = ChallengeState.Success;
            }
            else {
                Debug.Log("GameModeController: OnGameModeComplete: GameMode Normal");
            }
        }

        public void OnGameModeFail(){

        }

        public TypeChallenge CurrentGameMode { get => _currentGameMode; set => _currentGameMode = value; }
        public TimerModeController TimerModeController { get => _timerModeController; }
    }
}