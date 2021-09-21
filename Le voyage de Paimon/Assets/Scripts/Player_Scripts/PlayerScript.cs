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
    

    private void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _playerTransform = transform;
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (Input.GetKey("a"))
        {
            var x = mSpeed * Time.deltaTime; 
            _playerTransform.position += Vector3.left * x;
            _playerTransform.localScale = new Vector3(-PlayerSize, PlayerSize, 0f);
            
        }

        if (Input.GetKey("d"))
        {
            var x = mSpeed * Time.deltaTime; 
            _playerTransform.position += Vector3.right * x;           
            _playerTransform.localScale = new Vector3(PlayerSize, PlayerSize, 0f);
            
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
    }

    private void FixedUpdate()
    {
        
    }
}