using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ArrowAnimation : MonoBehaviour
{
    public Button _button;
    private Sequence _arrowSequence;

    private void Start()
    {
        _button = GetComponent<Button>();
    }
    

    private void ArrowSpring()
    {
        _arrowSequence.Kill();

        _arrowSequence = DOTween.Sequence();
        // _arrowSequence.Append(transform.DOShakeScale(1, new Vector3(.2f, 0, 0), randomness: 5).SetLoops(-1));
        _arrowSequence.Append(transform.DOScale(new Vector3(1.2f, 1f, 1f), 1));
        _arrowSequence.Append(transform.DOScale(new Vector3(1.0f, 1f, 1f), 1)).SetLoops(-1);
    }

    public void Show()
    {
        _arrowSequence.Kill();
        _arrowSequence = DOTween.Sequence();
        _arrowSequence.AppendCallback(delegate { _button.interactable = true; });
        _arrowSequence.Append(transform.DOMoveX(60, .7f).
            From(-90).SetEase(Ease.OutExpo));
        _arrowSequence.AppendCallback(ArrowSpring);

    }

    public void Hide()
    {
        _arrowSequence.Kill();
        _arrowSequence = DOTween.Sequence();
        _arrowSequence.AppendCallback(delegate { _button.interactable = false; });
        _arrowSequence.Append(transform.DOMoveX(-90, .7f).
            From(60).SetEase(Ease.InExpo));
        _arrowSequence.AppendCallback(ArrowSpring);
    }
}
