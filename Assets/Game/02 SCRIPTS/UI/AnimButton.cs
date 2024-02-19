using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum AnimButtonType
{
    Scale,
    Move,
    None
}

public class AnimButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] AnimButtonType _typeButton;
    [SerializeField] RectTransform _currenRect;
    [SerializeField] float _duration = 0.1f;
    [SerializeField] Vector2 _target;
    [SerializeField] UnityEvent _onPointerDown, _onPointerUp, _onPointerClik, _onPointerExit;
    private Vector2 _originalPos;
    private bool _canClick = true;

    private void Awake()
    {
        if (_typeButton.Equals(AnimButtonType.None)) return;
        _originalPos = _currenRect.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_canClick) return;
        _canClick = false;
        ShowButton();
        _onPointerDown?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideButton();
        _onPointerExit?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        HideButton();
        _onPointerUp?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onPointerClik?.Invoke();
    }

    private void ShowButton()
    {
        switch (_typeButton)
        {
            case AnimButtonType.Scale:
                _currenRect.DOScale(_target, _duration).SetEase(Ease.OutQuad);
                break;
            case AnimButtonType.Move:
                _currenRect.DOAnchorPos(_target, _duration).SetEase(Ease.OutQuad);
                break;
        }
    }

    private void HideButton()
    {
        switch (_typeButton)
        {
            case AnimButtonType.Scale:
                _currenRect.DOScale(Vector2.one, _duration).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    _canClick = true;
                });
                break;
            case AnimButtonType.Move:
                _currenRect.DOAnchorPos(_originalPos, _duration).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    _canClick = true;
                });
                break;
        }
    }
}