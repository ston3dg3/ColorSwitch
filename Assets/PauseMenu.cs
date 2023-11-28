using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Player player;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pauseMenuUI = gameObject;
        player.pauseMenu = this;
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            determineState();
        }
    }

    public void determineState()
    {
        if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        gameManager.setState(1);
        player.enabled = true;
    }
    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        gameManager.setState(3);
        player.enabled = false;
    }
}
