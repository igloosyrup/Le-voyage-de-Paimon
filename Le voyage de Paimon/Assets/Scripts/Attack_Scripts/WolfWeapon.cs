using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfWeapon : MonoBehaviour
{

    [SerializeField] private GameObject wolfWeapon;
    private PolygonCollider2D _wolfCollider2D;
    private SpriteRenderer _wolfSpriteRenderer;
    private Animator _animator;
    
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        wolfWeapon.SetActive(false);
        _wolfCollider2D = wolfWeapon.GetComponent<PolygonCollider2D>();
        _wolfSpriteRenderer = wolfWeapon.GetComponent<SpriteRenderer>();
        _wolfCollider2D.enabled = false;
    }

    private void Update()
    {
        
    }
}
