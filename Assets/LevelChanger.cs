using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private int LevelToLoad;
    public static LevelChanger instance;
    public Button[] lvlButtons;
    public AudioManager audioManager;

    // void Awake()
    // {
    //     // if the object doesnt exist as a static instance plz make one
    //     // otherwise plz destroy cause we dont wan 2 LevelChangers :(
    //     if (instance == null)
    //     { 
    //         instance = this;
    //     } else
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }

    //     DontDestroyOnLoad(gameObject);
    // }

    void Update()
    {
        
    }

    public void LoadLevelAt(int index)
	{
		SceneManager.LoadScene(index);
		Time.timeScale = 1f;
	}

    public void FadeToLevel (int levelIndex)
    {
        LevelToLoad = levelIndex;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        animator.SetTrigger("FadeOut");
        Time.timeScale = 1f;
    }

    public void FadeToNextLevel ()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        FadeToLevel (SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    public void FadeReplayLevel ()
    {
        LevelToLoad = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        animator.SetTrigger("FadeOut");
    }

    public void FadeToPreviousLevel ()
    {
        FadeToLevel (SceneManager.GetActiveScene().buildIndex + -1);
        Time.timeScale = 1f;
    }

    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(LevelToLoad);
        animator.SetTrigger("FadeIn");
    }


    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        animator = gameObject.GetComponent<Animator>();
        Debug.LogWarning("Current unlocked lvl:   " + PlayerPrefs.GetInt("levelReached"));

        int levelReached = PlayerPrefs.GetInt("levelReached", 0);
        
        for (int i=0; i < lvlButtons.Length; i++)
        {
            if (i > levelReached)
                lvlButtons[i].interactable = false;
        }
    }
}
