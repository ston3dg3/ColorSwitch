using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class tweenText : MonoBehaviour
{
    public float tweenTime;
    public float scale;
    public GameObject overText;
    public GameObject imageLose;
    public GameObject imageWin;
    public GameObject scoreText;
    public GameObject scoreName;
    public GameObject restartButton;
    public GameObject menuButton;
    public GameObject nextButton;
    public TMP_Text restartText;
    public TMP_Text menuText;
    public TMP_Text nextText;
    public int gameState;
    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject staticTextPanel;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {   
        scale = 0.1f;
        tweenTime = 5.0f;
        // TweenGameOver();
        // for testing

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        imageLose = GameObject.Find("ImageLose");
        imageWin = GameObject.Find("ImageWin");

        loseScreen = GameObject.Find("LoseScreen");
        winScreen = GameObject.Find("WinScreen");
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            loseScreen.SetActive(false);
            winScreen.SetActive(false);
        }
        else
        {
            staticTextPanel = GameObject.Find("StaticTextPanel");
            staticTextPanel.SetActive(false);
        }
    }

    public void TweenGameOver()
    {
        overText.GetComponent<Text>().color = Color.white;
        overText.transform.localScale = Vector3.one;
        LeanTween.cancel(overText);
        LeanTween.scale(overText, Vector3.one * scale, tweenTime).setEasePunch();
    }

    public void TweenScore()
    {
        float position = 268f; //(scoreText.transform.position.x);
        float delay = 0.75f;
        LeanTween.cancel(scoreText);
        LeanTween.moveX(scoreText, position+30f, delay).setEaseInCirc();
        LeanTween.moveX(scoreText, position-15f, 0.10f).setEaseInSine().setDelay(delay+0.15f);
    }

    public void TweenName()
    {
        scoreName.GetComponent<Text>().color = Color.white;
        // float position = 300.0f;
        float delay = 1.0f;
        scoreName.transform.localScale = Vector3.one;
        LeanTween.cancel(scoreName);
        LeanTween.scale(scoreName, Vector3.one * scale, 0.4f).setEasePunch().setDelay(delay+0.15f);
    }

    public void TweenButtons()
    {
        //restartText = GameObject.Find("restartingText").GetComponent<TMP_Text>();
        //menuText = GameObject.Find("menuingText").GetComponent<TMP_Text>();

        restartButton.SetActive(true);
        menuButton.SetActive(true);
        if (gameState == 2 && !checkIfLastLevel()) nextButton.SetActive(true);

        Color whitee = restartText.GetComponent<TMP_Text>().color;
        whitee.a = 0;
        menuText.GetComponent<TMP_Text>().color = whitee;
        restartText.GetComponent<TMP_Text>().color = whitee;
        if (gameState == 2 && !checkIfLastLevel()) nextText.GetComponent<TMP_Text>().color = whitee;


        float delay = 1.5f;
        float scala = 5.0f;
        restartButton.transform.localScale = Vector3.one / scala;
        menuButton.transform.localScale = Vector3.one / scala;
        LeanTween.cancel(restartButton);
        LeanTween.cancel(menuButton);
        if (gameState == 2 && !checkIfLastLevel()) LeanTween.cancel(nextButton);

        Invoke("makeVisible", 1.2f);

        LeanTween.scale(restartButton, Vector3.one * 2f, 0.3f).setDelay(delay+0f).setEaseInExpo();
        LeanTween.scale(menuButton, Vector3.one * 2f, 0.3f).setDelay(delay+0f).setEaseInExpo();
        if (gameState == 2 && !checkIfLastLevel()) LeanTween.scale(nextButton, Vector3.one * 2f, 0.3f).setDelay(delay+0f).setEaseInExpo();

    }

    public void makeVisible()
    {
        restartText.LeanAlphaText(1f,2f);
        menuText.LeanAlphaText(1f,2f);
        if (gameState == 2 && !checkIfLastLevel()) nextText.LeanAlphaText(1f,2f);
    }

    public void initialize(int state)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            staticTextPanel.SetActive(true);
            restartButton = GameObject.Find("RestartButton");
            menuButton = GameObject.Find("MainMenuButton");
            scoreName = GameObject.Find("scoreName");
            overText = GameObject.Find("gameOverText");
            scoreText = GameObject.Find("scoreText");
            overText.GetComponent<Text>().color = Color.clear;
            scoreName.GetComponent<Text>().color = Color.clear;
            restartButton.SetActive(false);
            menuButton.SetActive(false); 
        }
        else
        {
            if (gameState == 0) // if player lost initialize buttons from lost panel
            {
                loseScreen.SetActive(true);
                winScreen.SetActive(false);

                restartButton = GameObject.Find("RestartButton");
                menuButton = GameObject.Find("MainMenuButton");
                scoreName = GameObject.Find("scoreName");
                overText = GameObject.Find("gameOverText");
                scoreText = GameObject.Find("scoreText");
                restartText = GameObject.Find("restartingText").GetComponent<TextMeshProUGUI>();
                menuText = GameObject.Find("menuingText").GetComponent<TextMeshProUGUI>();
                overText.GetComponent<Text>().color = Color.clear;
                scoreName.GetComponent<Text>().color = Color.clear;
                restartButton.SetActive(false);
                menuButton.SetActive(false); 
            }

            if (gameState == 2) // if player won initialize buttons from win panel
            {
                winScreen.SetActive(true);
                loseScreen.SetActive(false);

                restartButton = GameObject.Find("RestartButtonWin");
                menuButton = GameObject.Find("MainMenuButtonWin");
                
                scoreName = GameObject.Find("scoreNameWin");
                overText = GameObject.Find("gameOverTextWin");
                scoreText = GameObject.Find("scoreText");
                if (!checkIfLastLevel())
                {
                    // do not load those on the last scene !!
                    nextText = GameObject.Find("nextText").GetComponent<TextMeshProUGUI>();
                    nextButton = GameObject.Find("NextLevelButtonWin");
                    nextButton.SetActive(false);
                } 
                restartText = GameObject.Find("restartingTextWin").GetComponent<TextMeshProUGUI>();
                menuText = GameObject.Find("menuingTextWin").GetComponent<TextMeshProUGUI>();
                overText.GetComponent<Text>().color = Color.clear;
                scoreName.GetComponent<Text>().color = Color.clear;
                restartButton.SetActive(false);
                menuButton.SetActive(false); 
            } 
        }
        
    }

    public void Tween()
    {
        gameState = gameManager.getState();
        initialize(gameState);
        TweenGameOver();
        // TweenScore();
        TweenName();
        TweenButtons();
    }

    private bool checkIfLastLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings-1) return true;
        else return false;
    }
    
}
