using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject Player;
    public AudioManager audioManager;
    public UIController userInterface;
    public Canvas canvas;
    public LevelChanger lvlChanger;
    public GameObject VolumeFast;
    public GameObject VolumeSlow;
    bool run;
    int state; // 0 means dead, 1 means alive and 2 means won 3 means paused


    // Start is called before the first frame update
    void Start()
    {
        state = 1;
        Player = GameObject.Find("Player");
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>(); 
        userInterface = GameObject.Find("Canvas").GetComponent<UIController>();
        // StartCoroutine(onCoroutine());
        

    }

    public void setVolumeSlow(GameObject obj)
    {
        // VolumeSlow = obj;
    }
    public void setVolumeFast(GameObject obj)
    {
        VolumeFast = obj;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            // PlayerPrefs.DeleteAll();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            // PlayerPrefs.SetInt("levelReached", 8);
        }
    }

    public void gameOver()
    {
            Time.timeScale = 1f;
            state = 0;
            Debug.Log("YOU LOST U FOOL! over.");
            audioManager.Play("PlayerDeath");
            if((state != 3) && (SceneManager.GetActiveScene().buildIndex != 0)) fadeBlack(true);
            canvas.GetComponent<tweenText>().Tween();
            audioManager.PitchBackToNormalAll();

    }

    public int getState()
    {
        return state;
    }

    public void setState(int newState)
    {
        state = newState;
    }

    public void CompleteLevel ()
    {
        Time.timeScale = 1f;
        if(SceneManager.GetActiveScene().buildIndex >= 7)
        {
            
            // if(VolumeSlow != null) VolumeSlow.SetActive(false);
        }
        
        state = 2;
        Debug.LogWarning("Level COMOESTE!");

        if (SceneManager.GetActiveScene().buildIndex >= 4) // && SceneManager.GetActiveScene().buildIndex < lvlChanger.lvlButtons.Length+4)
        {
            Debug.LogWarning("Got thru to next lvl!!!");
            PlayerPrefs.SetInt("levelReached", SceneManager.GetActiveScene().buildIndex -4 + 1);
        }

        if((state != 3) && (SceneManager.GetActiveScene().buildIndex != 0)) fadeBlack(false);
        audioManager.PitchBackToNormalAll();
        canvas.GetComponent<tweenText>().Tween();


        
            
    }

    void fadeBlack(bool fade)
	{
        GameObject panel;
    
            if(fade)
            {
                panel = GameObject.Find("ImageLose");
                StartCoroutine(userInterface.FadeToBlack(panel, true, 5));
            } else
            {
                panel = GameObject.Find("ImageWin");
                StartCoroutine(userInterface.FadeToBlack(panel, true, 5));
            }
	}

    IEnumerator onCoroutine()
     {
        while(true) 
        {
            audioManager.Play("Music");  
            yield return new WaitForSeconds(47.981f);
        }
     }
}
