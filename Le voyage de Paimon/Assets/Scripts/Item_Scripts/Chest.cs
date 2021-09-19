using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    private const string PlayerTag = "Player";
    private const string AnimationBoolName = "isTouched";
    private const float SelfDestructDelay = 1f;
    [SerializeField] private List<GameObject> listPowerUps;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    private static readonly int IsTouched = Animator.StringToHash(AnimationBoolName);

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Equals(PlayerTag)) return;
        _animator.SetBool(IsTouched, true);
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(SelfDestructDelay);
        Destroy(_boxCollider2D);
        Destroy(_animator);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        throw new NotImplementedException();
    }
}
