using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Slime : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    [SerializeField] private GameObject meleeHitbox;
    [SerializeField] private GameObject shootSpawn;
    [SerializeField] private List<GameObject> listProjectiles;
    private float _slimeHp;
    private float _attackDelay;
    private Collider2D _meleeCollider2D;
    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private bool _isAttack;

    private void Start()
    {
        _slimeHp = GameConstants.DefaultSlimeHp;
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _attackDelay = meleeHitbox != null ? GameConstants.MeleeAttackDelay : GameConstants.RangeAttackDelay;
        if (meleeHitbox == null) return;
        _meleeCollider2D = meleeHitbox.GetComponent<Collider2D>();
        _meleeCollider2D.enabled = false;
    }

    private void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        ShootProjectile();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.WolfWeaponTag))
        {
            StartCoroutine(DelayDestroy());
        }
    }

    private void ShootProjectile()
    {
        if (listProjectiles.Count <= 0 || shootSpawn == null || _isAttack)
            return;
        Instantiate(listProjectiles[GameConstants.Projectile01Index], shootSpawn.transform.position,
            shootSpawn.transform.rotation);
        _isAttack = true;
        StartCoroutine(DelayNextAttack());
    }

    private void ActivateMeleeCollider()
    {
        if (_meleeCollider2D == null || _meleeCollider2D.enabled)
            return;
        _meleeCollider2D.enabled = true;
    }

    private void DisableMeleeCollider()
    {
        if (_meleeCollider2D == null || _meleeCollider2D.enabled == false)
            return;
        _meleeCollider2D.enabled = false;
    }

    private IEnumerator DelayNextAttack()
    {
        yield return new WaitForSeconds(_attackDelay);
        _isAttack = false;
    }

    private IEnumerator DelayDestroy()
    {
        Destroy(_collider2D);
        Destroy(_rigidbody2D);
        yield return new WaitForSeconds(GameConstants.MonsterDelay);
        Destroy(_spriteRenderer);
        Destroy(gameObject);
    }
}