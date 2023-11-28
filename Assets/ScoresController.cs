using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoresController : MonoBehaviour
{

    public ScoresClass[] objects;
    public TMP_Text endlessScore;
    public AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {   
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        endlessScore = GameObject.Find("EndlessScore").GetComponent<TMP_Text>();
        if(PlayerPrefs.GetFloat("Endless", 0f) != 0f)
        {
            float endScore = PlayerPrefs.GetFloat("Endless");
            endlessScore.text = endScore.ToString("0");
        } else
        {
            endlessScore.text = "--";
        }


        int index = 4;
        foreach (ScoresClass lvl in objects)
        {
            lvl.levelBuildIndex = index;
            lvl.levelGameIndex = lvl.levelBuildIndex - 4;
            if(PlayerPrefs.GetFloat("Level"+lvl.levelGameIndex, 0f) != 0f)
            {
                float score = PlayerPrefs.GetFloat("Level"+lvl.levelGameIndex);
                string scoree = score.ToString("00.00");
                lvl.textToChange.text = scoree;
            } else
            {
                lvl.textToChange.text = "--:--";
            }

            index++;
        }
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

[System.Serializable]
public class ScoresClass
{
    public TMP_Text textToChange;
    public int levelBuildIndex = 0;
    public int levelGameIndex = 0;
}
