using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float mSpeed = 7.5f;
    private const float PlayerSize = 0.4f;
    private const float DefaultHealth = 200f;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _mvmnt;
    private float _playerHp;
    private Transform _playerTransform;
    private bool _isRight = true;

    private void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _playerTransform = transform;
    }

    private void Update()
    {
        if (_rigidbody2D.velocity.x != 0f || _rigidbody2D.velocity.y != 0f)
        {
            print(_rigidbody2D.velocity.ToString());
            _rigidbody2D.velocity = Vector2.zero;
        }
        
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

    private void FixedUpdate()
    {
        
  
    }
}