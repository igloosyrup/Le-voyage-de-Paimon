using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> mainMenuAudioClips;
    [SerializeField] private List<AudioClip> levelBGMAudioClips;
    [SerializeField] private List<AudioClip> gameOverAudioClips;
    [SerializeField] private List<AudioClip> otherAudioClips;
    // private GameObject _player;
    private AudioSource _audioSource;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
            
    }
    
    
}
