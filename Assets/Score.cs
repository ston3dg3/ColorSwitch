using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public Transform player;
    public Text scoreText;
    public float maxScore;
    public float currentScore;
    public Animator anim;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        scoreText = this.gameObject.GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = player.position.y;
        if(currentScore > maxScore)
        {
            maxScore = currentScore;
        }
        scoreText.text = maxScore.ToString("0");
        if(player.gameObject.GetComponent<Player>().isDead())
        {
            anim.SetTrigger("PlayScoreMove");
            if(PlayerPrefs.GetFloat("Endless", 0f) < currentScore)
            {
                PlayerPrefs.SetFloat("Endless", currentScore);
            }
        }
        // enable decreasing score below:
        //scoreText.text = currentScore.ToString("0");
    }
}
