using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float mSpeed = 5f;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _mvmnt;
    

    private void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey("a"))
        {
            var x = mSpeed * Time.deltaTime; 
            transform.position += Vector3.left * x;           
        }

        if (Input.GetKey("d"))
        {
            var x = mSpeed * Time.deltaTime; 
            transform.position += Vector3.right * x;           
            
        }

        if (Input.GetKey("w"))
        {
            var y = mSpeed * Time.deltaTime; 
            transform.position += Vector3.up * y;
        }

        if (Input.GetKey("s"))
        {
            var y = mSpeed * Time.deltaTime; 
            transform.position += Vector3.down * y;
        }
    }

    private void FixedUpdate()
    {
        
    }
}