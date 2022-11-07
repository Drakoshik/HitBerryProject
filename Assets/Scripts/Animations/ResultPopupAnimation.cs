using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ResultPopupAnimation : MonoBehaviour
{
    private Sequence _popupSequnce;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Show()
    {
        _popupSequnce.Kill();
        _popupSequnce = DOTween.Sequence();
        _popupSequnce.Append(_rectTransform.DOLocalMoveY(100, .7f).
            From(310).SetEase(Ease.OutExpo));

    }

    public void Hide()
    {
        _popupSequnce.Kill();
        _popupSequnce = DOTween.Sequence();
        _popupSequnce.Append(_rectTransform.DOLocalMoveY(310, .7f).
            From(100).SetEase(Ease.InExpo));
    }
    
    
    
}
