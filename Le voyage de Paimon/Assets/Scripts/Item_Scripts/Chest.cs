using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    private const float SelfDestructDelay = 1f;
    private const int WolfIndex = 0;
    [SerializeField] private List<GameObject> listPowerUps;
    [SerializeField] private GameObject itemSpawn;
    private Vector3 itemSpawnTrfPosition;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    private static readonly int IsTouched = Animator.StringToHash(GameConstants.IsTouched);

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        itemSpawnTrfPosition = itemSpawn.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Equals(GameConstants.PlayerTag)) return;
        _animator.SetBool(IsTouched, true);
        
        Instantiate(listPowerUps[WolfIndex], itemSpawnTrfPosition, Quaternion.Euler(0f,0f,-90f));
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        Destroy(_boxCollider2D);
        yield return new WaitForSeconds(SelfDestructDelay);
        Destroy(_animator);
        Destroy(gameObject);
    }
}
