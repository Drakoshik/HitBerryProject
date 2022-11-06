using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class JumpAnimation : MonoBehaviour, IHitHendler
{
    public UnityEvent spawnEvent;
    
    [SerializeField] private Transform endJumpPoint;
    [SerializeField] private CapAnimation _cap;

    private new Rigidbody rigidbody;
    private Sequence jumpSequence;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FruitJump()
    {
        _cap.Open();
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
        spawnEvent?.Invoke();
    }

    public void OnCreate(Transform endPoint, CapAnimation cap)
    {
        endJumpPoint = endPoint;
        _cap = cap;
    }

}
