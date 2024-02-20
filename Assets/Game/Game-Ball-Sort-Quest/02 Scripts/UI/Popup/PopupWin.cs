using DG.Tweening;
using PopupSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

namespace BallSortQuest
{
    public class PopupWin : SingletonPopup<PopupWin>
    {

        [Space,Header("PopUp Elements")]
        [SerializeField] private GameObject _firstGroupButton;
        [SerializeField] private GameObject _secondGroupButton;
        [SerializeField] private GameObject[] _stars = new GameObject[3];
        [SerializeField] private TMP_Text _textCoins;
        public void Show()
        {
            base.Show();
            _secondGroupButton.SetActive(false);
            _firstGroupButton.SetActive(true);
            ActiveStar(3, 0.5f);
            UpdateTextCoin();
        }

        public void Close()
        {
            base.Hide();
        }

        public void OnClickNextLevel()
        {
            ActionEvent.OnResetGamePlay?.Invoke();
            Close();
        }

        public void OnClickAccept(){
            _firstGroupButton.SetActive(false);
            _secondGroupButton.SetActive(true);
            PlayerData.UserData.CoinNumber += 100; // hard code, need to improve
            UpdateTextCoin();
        }

        private void ActiveStar(int star, float delay = 0){
            var seq = DOTween.Sequence();
            foreach (var item in _stars)
            {
                item.SetActive(false);
                item.transform.localScale = Vector3.zero;
            }
            _stars[0].SetActive(true);
            seq.Append(_stars[0].transform.DOScale(Vector3.one * 0.75f, 0.5f)
                .SetDelay(delay)
                .SetEase(Ease.OutBack)
            );
            for (int i = 1; i < _stars.Length; i++)
            {
                _stars[i].SetActive(i < star);
                seq.Append(_stars[i].transform.DOScale(Vector3.one * 0.75f, 0.5f)
                    .SetDelay(i*0.3f)
                    .SetEase(Ease.OutBack)
                );
            }
        }

        private void UpdateTextCoin(){
            _textCoins.text = PlayerData.UserData.CoinNumber.ToString();
        }
    }
}