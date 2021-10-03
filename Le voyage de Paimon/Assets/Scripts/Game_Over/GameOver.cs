using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private string returnSceneName;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ReturnToScene()
    {
        StopMusic();
        SceneManager.LoadSceneAsync(returnSceneName);
    }

    public void ReturnToMainMenu()
    {
        StopMusic();
        SceneManager.LoadSceneAsync(GameConstants.SceneMainMenu);
    }

    private void StopMusic()
    {
        _audioSource.enabled = false;
    }
}
