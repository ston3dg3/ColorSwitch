using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsVisualizer : MonoBehaviour
{

    public Slider sliderPower;
    public Slider sliderVolume;
    public Slider sliderMusic;
    // private SettingsScript settings;
    public float jumpForce;
    public float volume;
    public float music;
    public TMP_Text jumpPowerText;
    public AudioManager audioManager;
    public AudioMixer masterMixer;
    public AudioMixer volumeMixer;
    public AudioMixer musicMixer;
    

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        jumpForce = GameSettings.jumpForce;
        volume = GameSettings.volume;
        music =  GameSettings.music;
        sliderPower = GameObject.Find("SliderPower1").GetComponent<Slider>();
        sliderVolume = GameObject.Find("VolumeSlider1").GetComponent<Slider>();
        sliderMusic = GameObject.Find("MusicSlider1").GetComponent<Slider>();
        sliderPower.value = jumpForce;
        sliderVolume.value = volume;
        sliderMusic.value = music;

        if (PlayerPrefs.GetInt("levelReached", 0) < 8)
        {
            sliderPower.interactable = false;
            GameObject.Find("Fill").GetComponent<Image>().enabled = false;
            jumpPowerText.alpha = 0.4f;
        } else
        {
            sliderPower.interactable = true;
            GameObject.Find("Fill").GetComponent<Image>().enabled = true;
            jumpPowerText.alpha = 1f;
        }
    }

    public void setJumpForce (float value)
    {
        GameSettings.jumpForce = value;
    }

    public void SetVolume (float volume)
    {
        volumeMixer.SetFloat("volumeVolume", volume);
        GameSettings.volume = volume;
    }

    public void setMusic (float volume)
    {
        GameSettings.music = volume;
        musicMixer.SetFloat("musicVolume", volume);
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
