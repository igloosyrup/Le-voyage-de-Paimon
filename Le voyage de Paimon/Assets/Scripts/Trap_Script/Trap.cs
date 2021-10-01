using System;
using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject trapProjectile;
    [SerializeField] private GameObject shootSpawn;
    [SerializeField] private float rayDistance = 10f;
    private Vector3 _shootSpawnPosition;
    private Collider2D _trapCollider2D;
    private bool _isShot;

    private void Start()
    {
        _trapCollider2D = GetComponent<Collider2D>();
        if (trapProjectile != null && shootSpawn != null)
            _shootSpawnPosition = shootSpawn.transform.position;
        //     InvokeRepeating(nameof(Shoot), 0f, 1f);
    }

    private void Update()
    {
        if (trapProjectile == null || shootSpawn == null)
            return;
        var hit = Physics2D.Raycast(_shootSpawnPosition, transform.TransformDirection(Vector2.up), rayDistance);
        Debug.DrawRay(_shootSpawnPosition, transform.TransformDirection(Vector2.up) * rayDistance, Color.white);
        if (hit.collider == null || !hit.collider.gameObject.CompareTag(GameConstants.PlayerTag) || _isShot) return;
        _isShot = true;
        StartCoroutine(DelayAttack());
    }

    private void Shoot()
    {
        Instantiate(trapProjectile, shootSpawn.transform.position, shootSpawn.transform.rotation);
    }

    private IEnumerator DelayAttack()
    {
        Invoke(nameof(Shoot), 0.2f);
        yield return new WaitForSeconds(GameConstants.RangeAttackDelay);
        _isShot = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameConstants.PlayerTag))
        {
        }
    }
}