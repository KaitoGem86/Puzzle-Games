using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BallSortQuest
{
    public enum TypeChallenge : byte
    {
        None,
        Hidden,
        Timer,
        Move
    }

    public class RewardElement
    {
        private TypeChallenge _type;
        private TMP_Text _titleText;
        private TMP_Text _rewardText;
        //private TwoStateElement _iconChallenge;
        private TwoStateElement _twoStateButton;
        private AnimButton _playButton;
        private bool _isCanPlayNoNeedAds;

        public RewardElement(Transform challengeRoot, TypeChallenge type)
        {
            _type = type;
            _titleText = challengeRoot.GetChild(0).GetComponent<TMP_Text>();
            //_iconChallenge = new TwoStateElement(challengeRoot.GetChild(1));
            _rewardText = challengeRoot.GetChild(2).GetChild(1).GetChild(1).GetComponent<TMP_Text>();
            _playButton = challengeRoot.GetChild(3).GetComponent<AnimButton>();
            _twoStateButton = new TwoStateElement(_playButton.transform);
        }

        public void SetDisPlayElement(bool isCanPlayNoNeedAds = true)
        {
            var font = GameLanguage.Instance.langs.Find(x => x.lang_code.Equals(GameLanguage.Instance.crr_lang_code)).tm_font;
            _titleText.font = font;
            switch (_type)
            {
                case TypeChallenge.Hidden:
                    _titleText.text = GameLanguage.Get("txt_hidden_challenge");
                    _rewardText.text = "20";
                    break;
                case TypeChallenge.Timer:
                    _titleText.text = GameLanguage.Get("txt_timer_challenge");
                    _rewardText.text = "20";
                    break;
                case TypeChallenge.Move:
                    _titleText.text = GameLanguage.Get("txt_move_challenge");
                    _rewardText.text = "20";
                    break;
            }
            _isCanPlayNoNeedAds = isCanPlayNoNeedAds;
            if (isCanPlayNoNeedAds)
            {
                _twoStateButton.SetState(true);
                _playButton.transform.GetChild(2).GetComponent<TMP_Text>().text = GameLanguage.Get("txt_play");
                _playButton.transform.GetChild(2).GetComponent<TMP_Text>().font = font;
                //_iconChallenge.SetState(true);
            }
            else
            {
                _twoStateButton.SetState(false);
                //_iconChallenge.SetState(false);
            }
        }

        public void OnSelectChallenge()
        {
            //Do something with GamePlay as set the GamePlayMode enum ???
            GameManager.Instance.GameModeController.SetGameMode(_type);
        }

        public AnimButton PlayButton => _playButton;
        public bool IsCanPlayNoNeedAds => _isCanPlayNoNeedAds;
    }
}