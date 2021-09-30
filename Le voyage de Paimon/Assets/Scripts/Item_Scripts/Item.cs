using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Collider2D _collider2D;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(GameConstants.PlayerTag))
            return;
        Destroy(_collider2D);
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(GameConstants.ItemDelay);
        Destroy(_animator);
        Destroy(_spriteRenderer);
        Destroy(gameObject);
    }
}