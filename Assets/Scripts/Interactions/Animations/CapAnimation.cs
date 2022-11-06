using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CapAnimation : MonoBehaviour
{
    private Sequence _capSequence;
    public void Open()
    {
        _capSequence.Kill();
        _capSequence = DOTween.Sequence();

        _capSequence.Append(transform.DOLocalRotate(new Vector3(0f, 0f, -70f), .5f));

    }
    
    public void Close()
    {
        _capSequence.Kill();
        _capSequence = DOTween.Sequence();
        
        _capSequence.Append(transform.DOLocalRotate(new Vector3(0f, 0f, 0f), .5f).
            From(new Vector3(0f, 0f, -70f)));
    }
}
