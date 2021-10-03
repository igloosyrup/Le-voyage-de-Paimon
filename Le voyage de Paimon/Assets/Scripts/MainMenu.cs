using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private List<AsyncOperation> _scenesLoading;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _scenesLoading = new List<AsyncOperation>();
    }
    

    public void StartGame()
    {
        // _scenesLoading.Add(SceneManager.LoadSceneAsync(GameConstants.SceneLose));
        _scenesLoading.Add(SceneManager.LoadSceneAsync(GameConstants.SceneWin));
    }
    
    public void QuitGame()
    {
        _audioSource.enabled = false;
        Application.Quit();
    }

    public void SettingsMenu()
    {
    }
}
