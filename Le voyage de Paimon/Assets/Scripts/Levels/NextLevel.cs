using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private int targetSceneIndex;
    
    private const string Lvl01 = "Level-01";
    private const string Lvl02 = "Level-02";
    private const string Lvl03 = "Level-03";
    private Dictionary<int, string> _levels;
    private GameManager _gameManager;
    
    private void Start()
    {
        _levels = new Dictionary<int, string> {{1, Lvl01}, {2, Lvl02}, {3, Lvl03}};
        _gameManager = GameManager.GetGameManagerInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag(GameConstants.PlayerTag))
            return;
        _gameManager.OnNextScene += GetNextScene;
        
    }

    private string GetNextScene()
    {
        return _levels[targetSceneIndex];
    }

}
