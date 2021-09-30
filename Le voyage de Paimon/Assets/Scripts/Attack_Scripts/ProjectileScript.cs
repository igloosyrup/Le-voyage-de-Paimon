using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> projectiles;
    [SerializeField] private float projectileSpeed = 6f;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private Animator _animator;

    private void Start()
    {
        if (projectiles.Count <= 0)
            return;
        var projectile = projectiles[GameConstants.Projectile01Index];
        _spriteRenderer = projectile.GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _animator = projectile.GetComponent<Animator>();
        _rigidbody2D.velocity = transform.right * projectileSpeed;
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(GameConstants.ProjectileDuration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(GameConstants.PlayerTag) &&
            !other.gameObject.CompareTag(GameConstants.ObstacleTag))
            return;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag(GameConstants.PlayerTag))
            return;
        Destroy(gameObject);
    }
}