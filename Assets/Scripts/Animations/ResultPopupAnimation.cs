using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ResultPopupAnimation : MonoBehaviour
{
    [SerializeField] private Transform _startPoint, _endPoint;    
    
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
        _popupSequnce.Append((transform.DOLocalMove(_endPoint.localPosition, .7f))
            .From(_startPoint.localPosition));

    }

    public void Hide()
    {
        _popupSequnce.Kill();
        _popupSequnce = DOTween.Sequence();
        _popupSequnce.Append((transform.DOLocalMove(_startPoint.localPosition, .7f))
            .From(_endPoint.localPosition));
    }
    
    
    
}
