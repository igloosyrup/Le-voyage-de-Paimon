using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManagerInstance;
    public static GameManager GetGameManagerInstance => _gameManagerInstance;

    [SerializeField] private List<AudioClip> level01AudioClips;
    [SerializeField] private List<AudioClip> level02AudioClips;
    [SerializeField] private List<AudioClip> level03AudioClips;

    [SerializeField] private List<AudioClip> otherAudioClips;

    // private GameObject _player;
    private List<AsyncOperation> _scenesLoading;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (_gameManagerInstance != null && _gameManagerInstance != this)
            Destroy(gameObject);
        else
            _gameManagerInstance = this;

    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
        _scenesLoading = new List<AsyncOperation>();
        
    }

    private void Update()
    {
        
    }

    public void NextLevel()
    {
        // _scenesLoading.Add(SceneManager.UnloadSceneAsync(GameConstants.SceneMainMenu));
        // TODO change scene name later
        // _scenesLoading.Add(SceneManager.LoadSceneAsync(GameConstants.SceneLose));
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PauseGame()
    {
    }

    public void SettingsMenu()
    {
    }

    private void PlayBGM(List<AudioClip> audioClips)
    {
        
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
    {
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Equals(GameConstants.SceneLose) || sceneName.Equals(GameConstants.SceneWin) ||
            sceneName.Equals(GameConstants.SceneMainMenu))
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}