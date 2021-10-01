using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private List<GameObject> listFood;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject itemSpawn;
    private SpriteRenderer _spriteRenderer;
    private const byte ConsumedFire = 116;
    private const byte Opacity = 255;
    private Collider2D _collider2D;
    
    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(GameConstants.PlayerTag))
            return;
        StartCoroutine(DelaySpawn());
    }

    private void SpawnFood()
    {
        var foodIndex = Random.Range(0, listFood.Count);
        Instantiate(listFood[foodIndex], itemSpawn.transform.position, Quaternion.identity);
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(0.3f);
        SpawnFood();
        _spriteRenderer.color = new Color32(ConsumedFire, ConsumedFire, ConsumedFire, Opacity);
        _collider2D.enabled = false;
        Destroy(_collider2D);
        Destroy(fire);
    }
}