using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject trapProjectile;
    [SerializeField] private GameObject shootSpawn;
    [SerializeField] private float rayDistance = 5f;
    private Collider2D _trapCollider2D;
    private bool _isShot;

    private void Start()
    {
        // if (trapProjectile != null && shootSpawn != null)
        //     InvokeRepeating(nameof(Shoot), 0f, 1f);
    }

    private void Update()
    {
        if (trapProjectile == null || shootSpawn == null)
            return;
        var hit = Physics2D.Raycast(shootSpawn.transform.position, transform.TransformDirection(Vector2.up), 10f);
        Debug.DrawRay(shootSpawn.transform.position, transform.TransformDirection(Vector2.up) * 10f, Color.white);
        if (hit.collider == null) return;

        if (hit.collider.gameObject.CompareTag(GameConstants.PlayerTag))
        {
            if (_isShot)
                return;
            StartCoroutine(DelayAttack());
        }
    }

    private void Shoot()
    {
        Instantiate(trapProjectile, shootSpawn.transform.position, shootSpawn.transform.rotation);
        _isShot = true;
    }

    private IEnumerator DelayAttack()
    {
        Shoot();
        yield return new WaitForSeconds(GameConstants.RangeAttackDelay);
        _isShot = false;
    }
}