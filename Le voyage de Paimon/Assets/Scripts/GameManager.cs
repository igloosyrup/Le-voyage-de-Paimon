using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManagerInstance;
    public static GameManager GetGameManagerInstance => _gameManagerInstance;

    public delegate string OnChangeScene();

    public event OnChangeScene OnNextScene;

    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private List<AudioClip> level01AudioClips;
    [SerializeField] private List<AudioClip> level02AudioClips;
    [SerializeField] private List<AudioClip> level03AudioClips;
    [SerializeField] private List<AudioClip> otherAudioClips;

    // private GameObject _player;
    private List<AsyncOperation> _scenesLoading;
    private List<AudioClip> _currentListAudiocClips;
    private int _activeAudioClipIndex;
    private AudioSource _audioSource;
    private PlayerScript _player;
    private Camera _camera;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private IEnumerator _PlayNextBGMCoroutine;

    private void Awake()
    {
        if (_gameManagerInstance != null && _gameManagerInstance != this)
            Destroy(gameObject);
        else
            _gameManagerInstance = this;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _player = PlayerScript.GetPlayerPlayerInstance;
        _camera = FindObjectOfType<Camera>();
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        DontDestroyOnLoad(_camera);
        DontDestroyOnLoad(_cinemachineVirtualCamera);
        DontDestroyOnLoad(_player);
        DontDestroyOnLoad(eventSystem);
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(pauseCanvas);

        _currentListAudiocClips = level01AudioClips;
        _audioSource = GetComponent<AudioSource>();
        _scenesLoading = new List<AsyncOperation>();
        _PlayNextBGMCoroutine = PlayNext();
        PlayBGM();
    }

    private void Update()
    {
        if (OnNextScene != null)
            NextLevel();
    }

    private void NextLevel()
    {
        _audioSource.Stop();
        StopCoroutine(_PlayNextBGMCoroutine);
        var sceneName = OnNextScene?.Invoke().Clone().ToString();
        OnNextScene = null;
        _scenesLoading.Add(SceneManager.LoadSceneAsync(sceneName));

    }
    
    private void PlayBGM()
    {
        if (_activeAudioClipIndex > _currentListAudiocClips.Count || _activeAudioClipIndex < 0)
        {
            return;
        }

        _audioSource.clip = _currentListAudiocClips[_activeAudioClipIndex];
        _audioSource.Play();
        if (_currentListAudiocClips.Count == 1)
        {
            _audioSource.loop = true;
            return;
        }

        StartCoroutine(_PlayNextBGMCoroutine);
    }

    private IEnumerator PlayNext()
    {
        yield return new WaitForSeconds(_audioSource.clip.length);
        if (_activeAudioClipIndex > _currentListAudiocClips.Count || _activeAudioClipIndex < 0 ||
            _currentListAudiocClips.Count <= 0)
            yield return null;
        _activeAudioClipIndex =
            _activeAudioClipIndex + 1 >= _currentListAudiocClips.Count ? 0 : ++_activeAudioClipIndex;
        PlayBGM();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Equals(GameConstants.SceneLose) && sceneName.Equals(GameConstants.SceneWin) ||
            sceneName.Equals(GameConstants.SceneMainMenu))
        {
            _cinemachineVirtualCamera.enabled = false;
            pauseCanvas.enabled = false;
            eventSystem.enabled = false;
            Destroy(pauseCanvas.gameObject);
            Destroy(eventSystem.gameObject);
            Destroy(_cinemachineVirtualCamera.gameObject);
            Destroy(_camera.gameObject);
            Destroy(_player.gameObject);
            Destroy(gameObject);
            return;
        }
        ChangeLevelBGM();
        var spawn = GameObject.FindWithTag(GameConstants.PlayerSpawnTag).transform;
        _player.transform.position = spawn.position;
    }

    private void ChangeLevelBGM()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        _currentListAudiocClips = sceneName switch
        {
            GameConstants.SceneLvl01 => level01AudioClips,
            GameConstants.SceneLvl02 => level02AudioClips,
            GameConstants.SceneLvl03 => level03AudioClips,
            _ => _currentListAudiocClips
        };
        _activeAudioClipIndex = 0;
        PlayBGM();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}