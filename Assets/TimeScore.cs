using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeScore : MonoBehaviour
{
    public GameObject Player;
    public Transform player;
    public Text scoreText;
    public float currentScore;
    public float timeToStart;
    public Animator anim;
    bool run;


    // Start is called before the first frame update
    void Start()
    {
        run = true;
        Player = GameObject.Find("Player");
        player = Player.GetComponent<Transform>();
        scoreText = this.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<Player>().isDead() || Player.GetComponent<Player>().isWon())
        {
            anim.SetTrigger("PlayScoreMove");
        }
        if((Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)) && run)
        {
            timeToStart = Time.timeSinceLevelLoad;
            run = false;
        }
        if (!Player.GetComponent<Player>().isDead() && !Player.GetComponent<Player>().isWon() && !run)
        {
            currentScore = Time.timeSinceLevelLoad - timeToStart;
            scoreText.text = currentScore.ToString("00.00");
        }
        if(Player.GetComponent<Player>().isWon())
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex-4;
            if(PlayerPrefs.GetFloat("Level"+currentIndex, 0f) < currentScore)
            {
                PlayerPrefs.SetFloat("Level"+currentIndex, currentScore);
            }
        }
        
    }
}
