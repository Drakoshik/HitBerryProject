using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
public class JumpAnimation : MonoBehaviour
{
    [SerializeField] private Transform endJumpPoint;

    private new Rigidbody rigidbody;
    private Sequence jumpSequence;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(.7f);
        FruitJump();
    }

    private void FruitJump()
    {
        jumpSequence = DOTween.Sequence();

        jumpSequence.Append(rigidbody.DOJump(endJumpPoint.position + new Vector3(0f,.3f,0f),
            .5f, 1, 1.3f));
    }
}
