using System;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    // private static PauseGame _pauseGameInstance;
    // public static PauseGame GetInstancePauseGame => _pauseGameInstance;
    //
    [SerializeField] private GameObject pauseMenu;
    private bool _isGamePause;
    private GameManager _gameManager;
    
    // private void Awake()
    // {
    //     if (_pauseGameInstance != null && _pauseGameInstance != this)
    //         Destroy(gameObject);
    //     else
    //         _pauseGameInstance = this;
    // }

    private void Start()
    {
        _gameManager = GameManager.GetGameManagerInstance;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (_isGamePause)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isGamePause = false;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isGamePause = true;
    }

    public void MainMenu()
    {
        Resume();
        _gameManager.OnNextScene += GoToMainMenu;
    }

    private string GoToMainMenu()
    {
        return GameConstants.SceneMainMenu;
    }
    
    public void Settings()
    {
        
    }

    public bool GetGameStatus()
    {
        return _isGamePause;
    }

    private void OnDestroy()
    {
        _gameManager.OnNextScene -= GoToMainMenu;
    }
}