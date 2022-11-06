using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BlenderGlassAnimation : MonoBehaviour
{
    private Sequence _glassSequence;
    
    public void Shake()
    {
        _glassSequence.Kill();
        _glassSequence = DOTween.Sequence();

        _glassSequence.Append(transform.DOShakeRotation(2, new Vector3(0, 0, 3f),
            randomness:20).SetLoops(1));
        // _glassSequence.Join(transform.DOShakeRotation(1, new Vector3(.3f, 0, 0),
        //     randomness:45).SetLoops(2));
        _glassSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, 0f), .3f));
    }
    public void PutInShake()
    {
        _glassSequence.Kill();
        _glassSequence = DOTween.Sequence();

        _glassSequence.Append(transform.DOShakeRotation(.5f, new Vector3(0, 0, 3f),
            randomness:35).SetLoops(1));
        _glassSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, 0f), .3f));
        // _glassSequence.Join(transform.DOShakeRotation(1, new Vector3(.3f, 0, 0),
        //     randomness:45).SetLoops(2));
    }
}
