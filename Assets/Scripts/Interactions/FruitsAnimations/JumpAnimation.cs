using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class JumpAnimation : MonoBehaviour, IHitHendler
{
    [SerializeField] private Transform endJumpPoint;

    private new Rigidbody rigidbody;
    private Sequence jumpSequence;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FruitJump()
    {
        jumpSequence = DOTween.Sequence();
        rigidbody.useGravity = false;
        jumpSequence.Append(rigidbody.DOJump(endJumpPoint.position+ new Vector3(0f,.1f,0f),
            .3f, 1, 1f)).OnComplete(()=>{rigidbody.useGravity = true;}).
            SetEase(Ease.InSine);
        jumpSequence.Join(transform.DORotate( new Vector3(45f, 0f, 0f), .7f));
    }

    public void OnRaycastReceived()
    {
        FruitJump();
    }
    
}
