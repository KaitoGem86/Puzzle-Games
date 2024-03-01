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
        [SerializeField] private TMP_Text _processText;
        [SerializeField] private GameObject _completeProcessGroup;
        [SerializeField] private GameObject _unCompleteProcessGroup;

        //Will be replace by animation
        [Space, Header("Resource")]
        [SerializeField] private Sprite _chestClose;
        [SerializeField] private Sprite _chestOpen;

        private bool _isCanCollectReward;
        private int _multiRewardCoeff = 1;
        public void Show()
        {
            base.Show();
            
            ActiveStar(3, 0.5f);
            UpdateTextCoin();
            // Delay after activate 3 stars
            UpdateProcessBar(1.5f);
            // Set Chest's State
            SetChestState(false);
            _secondGroupButton.SetActive(false);
            _firstGroupButton.SetActive(false);
            if (_isCanCollectReward)
            {
                _completeProcessGroup.SetActive(true);
                _unCompleteProcessGroup.SetActive(false);
                _firstGroupButton.SetActive(true);
            }
            else
            {
                _unCompleteProcessGroup.SetActive(true);
                _completeProcessGroup.SetActive(false);
                _secondGroupButton.SetActive(true);
            }
        }

        public void Close()
        {
            base.Hide();
            StopCoroutine("UpdateProcessText");
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
                PlayerData.UserData.CoinNumber += 100 * _multiRewardCoeff; // hard code, need to improve
            UpdateTextCoin();
            _multiRewardCoeff = 1;
        }

        public void OnClickAdsButton(){
            //Show Ads
            _multiRewardCoeff = 2;
            //If success, call OnClickAccept
            OnClickAccept();
        }

        public void OnClickGotoShop(){
            OnClickNextLevel();
            PopupSystem.PopupManager.CreateNewInstance<ShopPanel>().Show();
        }

        //Tai thoi diem nay, PlayerData.UserData.ProcessValue da duoc update, co the > 100
        private void UpdateProcessBar(float delay = 0)
        {
            int processValue = PlayerData.UserData.ProcessValue;
            int processTextValue = processValue;
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
                            .OnComplete(() =>
                            {
                                StartCoroutine(CoroutineActiveRewardCoin(1.7f));
                            });
                    }
                });
            StartCoroutine(UpdateProcessText(0.3f, processTextValue, processTextValue - 35, delay));
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

        private void SetChestState(bool isOpened)
        {
            _chest.sprite = isOpened ? _chestOpen : _chestClose;
            _chest.SetNativeSize();
        }

        private void UpdateTextCoin()
        {
            _textCoins.text = PlayerData.UserData.CoinNumber.ToString();
        }

        private IEnumerator UpdateProcessText(float time, int endValue, int startValue, float delayTime = 0.0f)
        {
            int minPivot = Mathf.Min(endValue, 100);
            float stepTimer = time / (minPivot - startValue);
            _processText.text = startValue.ToString() + "%";
            yield return new WaitForSeconds(delayTime);
            while (true)
            {
                startValue++;
                _processText.text = startValue.ToString() + "%";
                if (startValue >= minPivot)
                {
                    _processText.text = endValue.ToString() + "%";
                    break;
                }
                yield return new WaitForSeconds(stepTimer);
            }
            if (endValue >= 100)
            {
                endValue -= 100;
                startValue = 0;
                while (true)
                {
                    startValue++;
                    _processText.text = startValue.ToString() + "%";
                    if (startValue >= endValue)
                    {
                        _processText.text = endValue.ToString() + "%";
                        yield break;
                    }
                    yield return new WaitForSeconds(stepTimer);
                }
            }
        }
    }
}