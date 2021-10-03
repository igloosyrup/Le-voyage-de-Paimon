using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI returnButtonTMP;
    [SerializeField] private TextMeshProUGUI menuButtonTMP;
    [SerializeField] private string returnSceneName;
    [SerializeField] private string returnButtonText;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        returnButtonTMP.text = returnButtonText;
        menuButtonTMP.text = "Menu d'accueil";
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
        SceneManager.LoadSceneAsync("Main-Menu");
    }

    private void StopMusic()
    {
        _audioSource.enabled = false;
    }
}
