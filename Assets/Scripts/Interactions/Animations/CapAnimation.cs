using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CapAnimation : MonoBehaviour
{
    public bool IsClosed
    {
        get;
        private set;
    } = true;

    private Sequence _capSequence;
    
    
    public void Open()
    {
        if (IsClosed)
        {
            _capSequence.Kill();
            _capSequence = DOTween.Sequence();

            _capSequence.Append(transform.DOLocalRotate(new Vector3(0f, 0f, -70f), .5f));
            IsClosed = false;
        } 
        
        StopAllCoroutines();
        StartCoroutine(CR_Close());
    }
    
    public void Close()
    {
        _capSequence.Kill();
        _capSequence = DOTween.Sequence();

        _capSequence.Append(transform.DOLocalRotate(new Vector3(0f, 0f, 0f), .5f).From(new Vector3(0f, 0f, -70f)))
            .OnComplete(() => { IsClosed = true; });

    }

    private IEnumerator CR_Close()
    {
        yield return new WaitForSeconds(2f);
        Close();
    }
}
