using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private static PlayerScript _playerInstance;
    public static PlayerScript GetPlayerPlayerInstance => _playerInstance;

    [SerializeField] private float mSpeed = 7.5f;
    [SerializeField] private List<Animator> listAnimators;
    [SerializeField] private List<GameObject> listWeapons;
    [SerializeField] private GameObject _backWeapon;
    [SerializeField] private GameObject _ultWeapon;
    [SerializeField] private List<AudioClip> pickupItemClips;
    [SerializeField] private List<AudioClip> pickupPowerUpClips;
    [SerializeField] private List<AudioClip> hurtClips;
    [SerializeField] private List<AudioClip> deathClips;
    [SerializeField] private List<AudioClip> attackClips;

    public delegate void OnUltPressed();

    public event OnUltPressed OnActivated;

    private Animator _playerAnimator;
    private const float PlayerSize = 0.4f;
    private const float DefaultHealth = 200f;
    private Rigidbody2D _playerRigidbody2D;
    private Transform _playerTransform;
    private float _playerHp;
    private int _wolfCount;
    private bool _isRight = true;
    private bool _isWolfPickedUp;
    private bool _isWolfAttacking;

    private void Awake()
    {
        if (_playerInstance != null && _playerInstance != this)
            Destroy(gameObject);
        else
            _playerInstance = this;
    }

    private void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerTransform = transform;
        if (!_isWolfPickedUp)
        {
            _backWeapon.SetActive(false);
        }
    }

    private void Update()
    {
        if (_playerRigidbody2D.velocity.x != 0f || _playerRigidbody2D.velocity.y != 0f)
        {
            _playerRigidbody2D.velocity = Vector2.zero;
        }

        MoveCharacter();

        if (Input.GetKeyDown(GameConstants.UltAttack) && _isWolfPickedUp && _wolfCount > 0)
        {
            StartCoroutine(ShowBackWeapon(false, GameConstants.NoDelay));
            OnActivated?.Invoke();
            _isWolfAttacking = true;
            _wolfCount--;
            StartCoroutine(DisableWolfWeaponAttackDelay());
        }
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.BackWeaponTag) && _wolfCount < GameConstants.MaxWolfCount)
        {
            _wolfCount++;
            if (!_isWolfPickedUp && !_isWolfAttacking)
                StartCoroutine(ShowBackWeapon(true, GameConstants.ItemDelay));
            print("weapon status "+ _isWolfPickedUp);
        }
    }

    private IEnumerator ShowBackWeapon(bool state, float delay)
    {
        yield return new WaitForSeconds(delay);
        _backWeapon.SetActive(state);
        _isWolfPickedUp = state;
    }

    private IEnumerator DisableWolfWeaponAttackDelay()
    {
        yield return new WaitForSeconds(GameConstants.UltAnimationDuration);
        _isWolfAttacking = false;
        print("count : " +_wolfCount);
        if (_wolfCount > 0)
        {
            StartCoroutine(ShowBackWeapon(true, GameConstants.NoDelay));
        }
        else
        {
            StartCoroutine(ShowBackWeapon(false, GameConstants.NoDelay));
        }
    }
}