using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManagerInstance;
    public static GameManager getGameManagerInstance => _gameManagerInstance;
    
    [SerializeField] private List<AudioClip> mainMenuAudioClips;
    [SerializeField] private List<AudioClip> levelBGMAudioClips;
    [SerializeField] private List<AudioClip> otherAudioClips;
    // private GameObject _player;
    private List<AsyncOperation> _scenesLoading;
    private AudioSource _audioSource;
    private EventSystem _eventSystem;

    private void Awake()
    {
        if (_gameManagerInstance != null && _gameManagerInstance != this)
            Destroy(gameObject);
        else
            _gameManagerInstance = this;

        _eventSystem = FindObjectOfType<EventSystem>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
        _scenesLoading = new List<AsyncOperation>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals(GameConstants.SceneMainMenu))
        {
            
        }
    }

    public void DeselectButton()
    {
        if (_eventSystem == null)
            return;
        _eventSystem.SetSelectedGameObject(null);
    }

    public void QuitGame()
    {
        _audioSource.enabled = false;
        Application.Quit();
    }

    public void StartGame()
    {
        var lvl01AudioClipIndex = Random.Range(0, GameConstants.Level01AudioClipLength);
        _audioSource.clip = levelBGMAudioClips[lvl01AudioClipIndex];
        _audioSource.enabled = true;
        
        // _scenesLoading.Add(SceneManager.UnloadSceneAsync(GameConstants.SceneMainMenu));
        // TODO change scene name later
        _scenesLoading.Add(SceneManager.LoadSceneAsync(GameConstants.SceneLose));
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PauseGame()
    {
        
    }

    public void SettingsMenu()
    {
        
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
    {
        var sceneName = SceneManager.GetActiveScene().name;
        if(sceneName.Equals(GameConstants.SceneLose) || sceneName.Equals(GameConstants.SceneWin))
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
