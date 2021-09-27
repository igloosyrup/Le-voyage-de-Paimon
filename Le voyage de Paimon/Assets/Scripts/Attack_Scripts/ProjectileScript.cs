using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> projectiles;
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
        _rigidbody2D = projectile.GetComponent<Rigidbody2D>();
        _collider2D = projectile.GetComponent<Collider2D>();
        _animator = projectile.GetComponent<Animator>();
    }

    private void Update()
    {
           
    }

    public void SetProjectileVelocity()
    {
        
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(GameConstants.ProjectileDuration);
        // _animator.
        // Destroy();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
