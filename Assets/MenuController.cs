using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public AudioManager audioManager;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (!GameSettings.DontPlayEntryMusic) GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("GameEntry");
        GameSettings.initializeFirst();
    }
    void OnDisable()
    {
        Invoke("playButton", 0.1f);
    }
    void OnEnable()
    {
        Invoke("playButton", 0.1f);
    }

    void playButton()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if(!audioManager.isPlaying("Button"))  audioManager.Play("Button");
    }
}
