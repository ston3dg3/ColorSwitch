using UnityEngine;

public class killerScript : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.GameOver();
            Destroy(gameObject);
        }
    }
}
