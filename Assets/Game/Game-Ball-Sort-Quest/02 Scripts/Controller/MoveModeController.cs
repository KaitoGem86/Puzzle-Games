using UnityEngine;
using TMPro;

namespace BallSortQuest {
    public class MoveModeController {
        private int _currentMove;
        private int _maxMove;
        private TMP_Text _moveText;

        public MoveModeController() { }

        public void ResetMaxMove(int maxMove){
            _maxMove = maxMove;
            _currentMove = 0;
        }

        public void SetMoveText(TMP_Text moveText){
            _moveText = moveText;
        }

        public void UpdateTextMove(){
            _moveText.text = _currentMove + "/" + _maxMove;
        }

        public void UpdateMoveValueAfterMove(){
            _currentMove++;
            UpdateTextMove();
        }

        public void CheckOverMove(){
            if (_currentMove >= _maxMove){
                Debug.Log("Over Move");
                PopupSystem.PopupManager.CreateNewInstance<PopupCloseChallengeMode>().Show("Hết lượt \n Thử lại?", TypeChallenge.Move, true);
                //Do something with GamePlay as set the GamePlayMode enum ???
                //GameManager.Instance.GameModeController.SetGameMode(TypeChallenge.None);
            }
        }
    }
}