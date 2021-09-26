using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfWeapon : MonoBehaviour
{
    private static WolfWeapon _wolfWeaponInstance;
    public WolfWeapon GetWolfWeaponInstance => _wolfWeaponInstance;

    [SerializeField] private GameObject wolfWeapon;
    private PolygonCollider2D _wolfCollider2D;
    private SpriteRenderer _wolfSpriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        if (_wolfWeaponInstance != null && _wolfWeaponInstance != this)
            Destroy(gameObject);
        else
            _wolfWeaponInstance = this;
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        wolfWeapon.SetActive(false);
        _wolfCollider2D = wolfWeapon.GetComponent<PolygonCollider2D>();
        _wolfSpriteRenderer = wolfWeapon.GetComponent<SpriteRenderer>();
        _wolfCollider2D.enabled = false;
        PlayerScript.GetPlayerPlayerInstance.ONActivated += ActivateWeapon;
    }

    private void Update()
    {
        
    }

    private void ActivateWeapon()
    {
        // PlayerScript.GetPlayerPlayerInstance.ONActivated -= ActivateWeapon;
        print("I AM AC");
        wolfWeapon.SetActive(true);
        _wolfCollider2D.enabled = true;
        _animator.SetBool(GameConstants.IsWolfAttackAnimBool, true);
        StartCoroutine(DisableAnimationDelay());
    }

    private void DeactivateWeapon()
    {
        print("I AM DC");
        _animator.SetBool(GameConstants.IsWolfAttackAnimBool, false);
        _wolfCollider2D.enabled = false;
        wolfWeapon.SetActive(false);
        transform.position = new Vector3(0.72f, 3.25f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private IEnumerator DisableAnimationDelay()
    {
        yield return new WaitForSeconds(GameConstants.UltAnimationDuration);
        DeactivateWeapon();
    }
}