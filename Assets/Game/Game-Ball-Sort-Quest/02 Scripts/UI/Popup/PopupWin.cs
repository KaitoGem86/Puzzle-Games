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
        [SerializeField] private TMP_Text _textCoins;
        [SerializeField] private Image _processBar;
        [SerializeField] private ChestController _chest;
        [SerializeField] private GameObject _rewardCoin;
        [SerializeField] private TMP_Text _processText;
        //[SerializeField] private GameObject _completeProcessGroup;
        //[SerializeField] private GameObject _unCompleteProcessGroup;

        //Will be replace by animation
        [Space, Header("Resource")]
        [SerializeField] private Sprite _chestClose;
        [SerializeField] private Sprite _chestOpen;

        private bool _isCanCollectReward;
        private int _multiRewardCoeff = 1;
        public void Show()
        {
            base.Show();
            UpdateTextCoin();
            // Delay after activate 3 stars
            UpdateProcessBar(0.5f);
            // Set Chest's State
            SetChestState(false);
            _secondGroupButton.SetActive(false);
            _firstGroupButton.SetActive(false);
            if (_isCanCollectReward)
            {
                //_completeProcessGroup.SetActive(true);
                //_unCompleteProcessGroup.SetActive(false);
                _firstGroupButton.SetActive(true);
            }
            else
            {
                //_unCompleteProcessGroup.SetActive(true);
                //_completeProcessGroup.SetActive(false);
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
            global::SFXTapController.Instance.OnClickButtonUI();
            ActionEvent.OnResetGamePlay?.Invoke();
            Close();
        }

        public void OnClickAccept()
        {
            global::SFXTapController.Instance.OnClickButtonUI();
            _firstGroupButton.SetActive(false);
            _secondGroupButton.SetActive(true);
            if (_isCanCollectReward)
                PlayerData.UserData.CoinNumber += 100 * _multiRewardCoeff; // hard code, need to improve
            UpdateTextCoin();
            _multiRewardCoeff = 1;
        }

        public void OnClickAdsButton()
        {
            //Show Ads
            global::SFXTapController.Instance.OnClickButtonUI();
            _multiRewardCoeff = 2;
            //If success, call OnClickAccept
            OnClickAccept();
        }

        public void OnClickGotoShop()
        {
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
                                StartCoroutine(CoroutineActiveRewardCoin(0.7f));
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



        private void SetChestState(bool isOpened)
        {
            if(isOpened){
                _chest.OpenChest(null);
            }
            else{
                _chest.DefaultChest();
            }
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