using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    private const float SelfDestructDelay = 1f;
    private const int WolfIndex = 0;
    [SerializeField] private List<GameObject> listPowerUps;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    private static readonly int IsTouched = Animator.StringToHash(GameConstants.IsTouched);

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Equals(GameConstants.PlayerTag)) return;
        _animator.SetBool(IsTouched, true);
        var chest = transform.position;
        var posX = chest.x;
        var posY = chest.y + 2;
        Instantiate(listPowerUps[WolfIndex], new Vector3(posX, posY, 0f), Quaternion.Euler(0f,0f,-90f));
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(SelfDestructDelay);
        Destroy(_boxCollider2D);
        Destroy(_animator);
        Destroy(gameObject);
    }
}
