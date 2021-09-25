using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float mSpeed = 7.5f;
    [SerializeField] private List<Animator> listAnimators;
    [SerializeField] private List<GameObject> listWeapons;
    [SerializeField] private GameObject _backWeapon;
    private Animator _playerAnimator;
    private const float PlayerSize = 0.4f;
    private const float DefaultHealth = 200f;
    private Rigidbody2D _rigidbody2D;
    private float _playerHp;
    private Transform _playerTransform;
    private bool _isRight = true;
    private bool _isWolfPickedUp = false;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerTransform = transform;
        if (!_isWolfPickedUp)
        {
            _backWeapon.SetActive(false);
        }
    }

    private void Update()
    {
        if (_rigidbody2D.velocity.x != 0f || _rigidbody2D.velocity.y != 0f)
        {
            _rigidbody2D.velocity = Vector2.zero;
        }

        MoveCharacter();
    }

    private void FixedUpdate()
    {
    }

    private void MoveCharacter()
    {
        if (Input.GetKey("w"))
        {
            var y = mSpeed * Time.deltaTime;
            _playerTransform.position += Vector3.up * y;
        }

        if (Input.GetKey("s"))
        {
            var y = mSpeed * Time.deltaTime;
            _playerTransform.position += Vector3.down * y;
        }

        if (Input.GetKey("a"))
        {
            var x = mSpeed * Time.deltaTime;
            _playerTransform.position += Vector3.left * x;
            if (_isRight)
            {
                _playerTransform.localScale = new Vector3(-PlayerSize, PlayerSize, 0f);
                _isRight = false;
            }
        }

        if (Input.GetKey("d"))
        {
            var x = mSpeed * Time.deltaTime;
            _playerTransform.position += Vector3.right * x;
            if (!_isRight)
            {
                _playerTransform.localScale = new Vector3(PlayerSize, PlayerSize, 0f);
                _isRight = true;
            }
        }

        if (Input.GetKeyDown(GameConstants.UltAttack))
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.BackWeaponTag) && !_isWolfPickedUp)
        {
            StartCoroutine(ShowBackWeapon(true));
        }
    }

    private IEnumerator ShowBackWeapon(bool state)
    {
        yield return new WaitForSeconds(GameConstants.ItemDelay);
        _backWeapon.SetActive(state);

    }
}