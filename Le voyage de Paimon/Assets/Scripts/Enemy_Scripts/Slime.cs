using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Slime : MonoBehaviour
{
    [SerializeField] private GameObject slimeAI;
    [SerializeField] private GameObject meleeHitbox;
    [SerializeField] private GameObject shootSpawn;
    [SerializeField] private GameObject shootDetection;
    [SerializeField] private List<GameObject> listProjectiles;
    [SerializeField] private float rayDistance = 10f;
    private Animator _animator;
    private Vector3 _shootDetectionPosition;
    private AIPath _aiPath;
    private AIDestinationSetter _aiDestinationSetter;
    private float _slimeHp;
    private float _attackDelay;
    private Collider2D _meleeCollider2D;
    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private bool _isAttack;
    private bool _isAIPathNotNull;
    private int _isDeadAnimBoolHash;
    private void Start()
    {
        _isDeadAnimBoolHash = Animator.StringToHash(GameConstants.IsDeadAnimBool);
        _animator = GetComponent<Animator>();
        _aiPath = slimeAI.GetComponent<AIPath>();
        _isAIPathNotNull = _aiPath != null;
        _aiDestinationSetter = slimeAI.GetComponent<AIDestinationSetter>();
        _aiDestinationSetter.target = GameObject.FindWithTag(GameConstants.PlayerTag).transform;
        _slimeHp = GameConstants.DefaultSlimeHp;
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _attackDelay = meleeHitbox != null ? GameConstants.MeleeAttackDelay : GameConstants.RangeAttackDelay;

        // TODO maybe unnecessary
        if (_attackDelay.CompareTo(GameConstants.RangeAttackDelay) == 0)
            RangeSetup();
        else
            MeleeSetup();

        if (meleeHitbox == null) return;
        _meleeCollider2D = meleeHitbox.GetComponent<Collider2D>();
        _meleeCollider2D.enabled = false;
    }

    private void MeleeSetup()
    {
    }

    private void RangeSetup()
    {
        _shootDetectionPosition = shootDetection.transform.position;
    }

    private void Update()
    {
        if (_isAIPathNotNull)
            if (_aiPath.desiredVelocity.x >= 0.01f)
            {
                slimeAI.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (_aiPath.desiredVelocity.x <= -0.01f)
            {
                slimeAI.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

        if (_attackDelay.CompareTo(GameConstants.RangeAttackDelay) != 0)
        {
            print("inside the not range delay");
            return;
        }

        _shootDetectionPosition = shootDetection.transform.position;
        var hit = Physics2D.Raycast(_shootDetectionPosition, transform.TransformDirection(Vector2.left), rayDistance);
        Debug.DrawRay(_shootDetectionPosition, transform.TransformDirection(Vector2.left) * rayDistance, Color.white);

        // if(hit.collider && hit.collider.gameObject.CompareTag(GameConstants.PlayerTag))
        //     print("player");

        if (hit.collider == false || !hit.collider.gameObject.CompareTag(GameConstants.PlayerTag) || _isAttack)
        {
            return;
        }

        _animator.SetBool(GameConstants.IsAttackAnimBool, true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.WolfWeaponTag))
        {
            Destroy(_collider2D);
            Destroy(_rigidbody2D);
            _animator.SetBool(GameConstants.IsAttackAnimBool, false);
            _animator.SetBool(GameConstants.IsHitAnimBool, false);
            _animator.SetBool(_isDeadAnimBoolHash, true);
            // StartCoroutine(DelayDestroy());
        }
        else if (other.gameObject.CompareTag(GameConstants.PlayerProjectileTag))
        { 
            // TODO add future implementations for general player projectiles
            
        }
    }

    private void ShootProjectile()
    {
        if (listProjectiles.Count <= 0 || shootSpawn == false || _isAttack)
            return;
        Instantiate(listProjectiles[GameConstants.Projectile01Index], shootSpawn.transform.position,
            shootSpawn.transform.rotation);
        _isAttack = true;
        _animator.SetBool(GameConstants.IsAttackAnimBool, false);
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
    

    private IEnumerator StopAnimation()
    {
        _animator.SetBool(_isDeadAnimBoolHash, false);
        yield return new WaitForSeconds(GameConstants.MonsterDelay);
        DestroySlime();
    }

    private void DestroySlime()
    {
        print("destroyed");
        Destroy(_spriteRenderer);
        Destroy(slimeAI);
    }
}