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
        private GameObject _adsIcon;
        private AnimButton _playButton;
        private bool _isCanPlayNoNeedAds;

        public RewardElement(Transform challengeRoot, TypeChallenge type){
            _type = type;
            _titleText = challengeRoot.GetChild(0).GetComponent<TMP_Text>();
            _iconChallenge = new TwoStateElement(challengeRoot.GetChild(1));
            _rewardText = challengeRoot.GetChild(2).GetChild(1).GetChild(1).GetComponent<TMP_Text>();
            _playButton = challengeRoot.GetChild(3).GetComponent<AnimButton>();
            _adsIcon = _playButton.transform.GetChild(1).gameObject;
        }

        public void SetDisPlayElement(bool isCanPlayNoNeedAds = true){
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
            _isCanPlayNoNeedAds = isCanPlayNoNeedAds;
            if(isCanPlayNoNeedAds){
                _adsIcon.SetActive(false);
                _iconChallenge.SetState(true);
            }
            else{
                _adsIcon.SetActive(true);
                _iconChallenge.SetState(false);
            }
        }

        public void OnSelectChallenge(){
            //Do something with GamePlay as set the GamePlayMode enum ???
            GameManager.Instance.GameModeController.SetGameMode(_type);
        }

        public AnimButton PlayButton => _playButton;
        public bool IsCanPlayNoNeedAds => _isCanPlayNoNeedAds;
    }
}