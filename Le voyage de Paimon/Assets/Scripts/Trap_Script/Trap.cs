using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject trapProjectile;
    [SerializeField] private GameObject shootSpawn;
    private Collider2D _trapCollider2D;

    private void Start()
    {
        if (trapProjectile != null && shootSpawn != null)
            InvokeRepeating(nameof(Shoot), 0f, 1f);
    }

    private void Update()
    {
    }

    private void Shoot()
    {
        Instantiate(trapProjectile, shootSpawn.transform.position, shootSpawn.transform.rotation);

    }
}