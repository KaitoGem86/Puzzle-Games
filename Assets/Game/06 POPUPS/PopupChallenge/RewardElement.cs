using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BallSortQuest{
    public enum TypeChallenge : byte{
        None,
        Hidden,
        Timer,
        Move
    }

    public class RewardElement{
        private TypeChallenge _type;
        private TMP_Text _titleText;
        private TMP_Text _rewardText;
        private TwoStateElement _iconChallenge;
        private Button _playButton;

        public RewardElement(Transform challengeRoot, TypeChallenge type){
            _type = type;
            _titleText = challengeRoot.GetChild(0).GetComponent<TMP_Text>();
            _iconChallenge = new TwoStateElement(challengeRoot.GetChild(1));
            _rewardText = challengeRoot.GetChild(2).GetChild(1).GetChild(1).GetComponent<TMP_Text>();
            _playButton = challengeRoot.GetChild(3).GetComponent<Button>();
        }

        public void SetDisPlayElement(){
            switch (_type){
                case TypeChallenge.Hidden:
                    _titleText.text = "Nhiệm vụ Ẩn";
                    _rewardText.text = "20";
                    break;
                case TypeChallenge.Timer:
                    _titleText.text = "Nhiệm vụ Thời Gian";
                    _rewardText.text = "20";
                    break;
                case TypeChallenge.Move:
                    _titleText.text = "Nhiệm vụ Di Chuyển";
                    _rewardText.text = "20";
                    break;
            }
            _iconChallenge.SetState(true);
        }

        public void OnSelectChallenge(){
            Debug.Log("Select Challenge: " + _type);
            //Do something with GamePlay as set the GamePlayMode enum ???
            GameManager.Instance.GameModeController.SetGameMode(_type);
        }

        public Button PlayButton => _playButton;
    }
}