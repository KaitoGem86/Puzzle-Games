using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using PopupSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BallSortQuest
{
    public class PopupWin : SingletonPopup<PopupWin>
    {

        [Space, Header("PopUp Elements")]
        [SerializeField] private GameObject _firstGroupButton;
        [SerializeField] private GameObject _secondGroupButton;
        [SerializeField] private GameObject[] _stars = new GameObject[3];
        [SerializeField] private TMP_Text _textCoins;
        [SerializeField] private Image _processBar;
        [SerializeField] private Image _chest;
        [SerializeField] private GameObject _rewardCoin;

        //Will be replace by animation
        [Space, Header("Resource")]
        [SerializeField] private Sprite _chestClose;
        [SerializeField] private Sprite _chestOpen;
        
        private bool _isCanCollectReward;
        public void Show()
        {
            base.Show();
            _secondGroupButton.SetActive(false);
            _firstGroupButton.SetActive(true);
            ActiveStar(3, 0.5f);
            UpdateTextCoin();
            // Delay after activate 3 stars
            UpdateProcessBar(1.5f);
            // Set Chest's State
            SetChestState(false);
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

        public void OnClickAccept()
        {
            _firstGroupButton.SetActive(false);
            _secondGroupButton.SetActive(true);
            if (_isCanCollectReward)
                PlayerData.UserData.CoinNumber += 100; // hard code, need to improve
            UpdateTextCoin();
        }

        //Tai thoi diem nay, PlayerData.UserData.ProcessValue da duoc update, co the > 100
        private void UpdateProcessBar(float delay = 0)
        {
            int processValue = PlayerData.UserData.ProcessValue;
            _rewardCoin.SetActive(false);
            _processBar.fillAmount = (float)(processValue - 35) / 100;
            _processBar.transform.parent.gameObject.SetActive(true);
            if (processValue >= 100)
            {
                _isCanCollectReward = true;
                PlayerData.UserData.ProcessValue -= 100;
            }
            else
                _isCanCollectReward = false;
            _processBar.DOFillAmount((float)processValue / 100, 0.3f)
                .SetEase(Ease.OutBack)
                .SetDelay(delay)
                .OnComplete(() =>
                {
                    if (_isCanCollectReward)
                    {
                        processValue -= 100;
                        _processBar.fillAmount = 0;
                        SetChestState(_isCanCollectReward);
                        _processBar.DOFillAmount((float)processValue / 100, 0.3f)
                            .SetEase(Ease.OutBack)
                            .OnComplete( () =>
                            {
                                StartCoroutine(CoroutineActiveRewardCoin(0.3f));
                            });
                    }
                });
            IEnumerator CoroutineActiveRewardCoin(float timeDelay)
            {
                yield return new WaitForSeconds(timeDelay);
                _processBar.transform.parent.gameObject.SetActive(false);
                _rewardCoin.SetActive(true);
            }
        }

        private void ActiveStar(int star, float delay = 0)
        {
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
                    .SetDelay(i * 0.3f)
                    .SetEase(Ease.OutBack)
                );
            }
        }

        private void SetChestState(bool isOpened){
            _chest.sprite = isOpened ? _chestOpen : _chestClose;
            _chest.SetNativeSize();
        }

        private void UpdateTextCoin()
        {
            _textCoins.text = PlayerData.UserData.CoinNumber.ToString();
        }
    }
}