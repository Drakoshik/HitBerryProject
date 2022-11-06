using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FruitMixCylinderAnimation : MonoBehaviour
{
    private Sequence _cylinderSequence;
    
    public void Shake()
    {
        _cylinderSequence.Kill();
        _cylinderSequence = DOTween.Sequence();

        _cylinderSequence.Append(transform.DOShakeScale(2f, new Vector3(0, .025f, 0),
            randomness:.5f).SetLoops(1));
    }
}
