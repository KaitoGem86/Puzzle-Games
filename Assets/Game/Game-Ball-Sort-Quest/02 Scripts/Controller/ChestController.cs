using DG.Tweening;
using PopupSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest
{
    public class ChestController : MonoBehaviour
    {
        public AnimUIController _anim;
        [SerializeField] GameObject _effectChestObj;
        [SerializeField] float _duration, _delayTime, _jumpPower;
        private RectTransform _rectChest;
        private Vector2 _targetPosChest;

        private void Awake()
        {
            _rectChest = GetComponent<RectTransform>();
            _targetPosChest = _rectChest.anchoredPosition;
        }

        public void InitChest(Action callBack)
        {
            _anim.SetAnimation(0, false, 1, () =>
              {
                  OpenChest(callBack);
              });
        }

        public void InitChestWithJump(Vector2 target, Action callBack)
        {
            SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("chest_enter"));
            //Init Chest
            _rectChest.anchoredPosition = new Vector2(GetWidth(), target.y);
            _anim.transform.localScale = Vector2.one / 2;

            var sequence = DOTween.Sequence();
            sequence.Append(_rectChest.DOJumpAnchorPos(_targetPosChest, _jumpPower, 1, _duration).SetEase(Ease.Linear));
            sequence.Insert(0.1f,
            _anim.transform.DOScale(Vector2.one, _duration).SetEase(Ease.OutBounce).SetDelay(_delayTime).OnComplete(() =>
            {
                _anim.SetAnimation(0, true);
                callBack?.Invoke();
            }));
        }

        public void OpenChest(Action callBack)
        {
            Debug.Log("OpenChest");
            
            //Invoke(nameof(PlaySfxOpenGift), 0.5f);
            //_anim._skeAnim.AnimationState.SetAnimation(0, animationName: null, loop: false);
            _anim._skeAnim.AnimationState.ClearTrack(0);
            _anim.SetAnimation(0, false, timeScale: 1, () =>
            {
                _anim.SetAnimation(2, true);

            }, eventName: "Event", () =>
            {
                _effectChestObj.SetActive(true);
                callBack?.Invoke();
            });
        }

        public void DefaultChest()
        {
            _anim.SetAnimation(1, true, 1);
            _effectChestObj.SetActive(false);
        }

        private void PlaySfxOpenGift()
        {
            // SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("PopupClaimGift"));
            SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("openbox"));
            Invoke(nameof(PlaySfxBling), 0f);
        }

        private void PlaySfxBling()
        {
            SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("CollectGift"));
        }

        private float GetHeight()
        {
            return Screen.height;
        }

        private float GetWidth()
        {
            // Debug.Log(PopupManager.Instance.canvas.GetComponent<RectTransform>().anchoredPosition.x);
            return PopupManager.Instance.canvas.GetComponent<RectTransform>().anchoredPosition.x;
        }
    }
}