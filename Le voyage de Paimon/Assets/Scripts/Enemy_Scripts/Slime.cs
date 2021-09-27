using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Slime : MonoBehaviour
{

    [SerializeField] private AIPath aiPath;
    [SerializeField] private GameObject meleeHitbox;
    [SerializeField] private List<GameObject> listProjectiles;
    private float _slimeHp;
    private float _attackDelay;
    private Collider2D _meleeCollider2D;
    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
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


    // Update is called once per frame
    private void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.WolfWeaponTag))
        {
            StartCoroutine(DelayDestroy());
        }
    }
    
    private void ActivateMeleeCollider()
    {
        if (_meleeCollider2D != null && _meleeCollider2D.enabled == false)
            _meleeCollider2D.enabled = true;
    }

    private void DisableMeleeCollider()
    {
        if (_meleeCollider2D != null && _meleeCollider2D.enabled)
        {
            _meleeCollider2D.enabled = false;
        }
    }

    private IEnumerator DelayNextAttack()
    {
        yield return new WaitForSeconds(_attackDelay);
        
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
