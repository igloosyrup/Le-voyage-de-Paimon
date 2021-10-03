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
    private List<AudioClip> _currentListAudiocClips;
    private int _activeAudioClipIndex;
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
        _currentListAudiocClips = level01AudioClips;
        _audioSource = GetComponent<AudioSource>();
        _scenesLoading = new List<AsyncOperation>();
        PlayBGM();
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

    private void PlayBGM()
    {
        if (_activeAudioClipIndex > _currentListAudiocClips.Count || _activeAudioClipIndex < 0)
            return;
        _audioSource.clip = _currentListAudiocClips[_activeAudioClipIndex];
        _audioSource.Play();
        StartCoroutine(PlayNext());
    }

    private IEnumerator PlayNext()
    {
        yield return new WaitForSeconds(_audioSource.clip.length);
        _activeAudioClipIndex =
            _activeAudioClipIndex + 1 >= _currentListAudiocClips.Count ? 0 : ++_activeAudioClipIndex;
        PlayBGM();
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