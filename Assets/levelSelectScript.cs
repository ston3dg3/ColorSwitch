using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelectScript : MonoBehaviour
{
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(!audioManager.isPlaying("Button")) audioManager.Play("Button");
    }
}
