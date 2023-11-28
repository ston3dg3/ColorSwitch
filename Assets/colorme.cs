using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class colorme : MonoBehaviour {

	public GameObject player; // set to player
	public AudioManager audioManager;
	public ColorManager colorManager;
	float timeLeft;
	List<Color> allColors = new List<Color>();

	public bool restrictToColor;
	public int restrictToColorIndex;
	private int whichColor = 0;
	public Color colorBlue;
	public Color colorYellow;
	public Color colorPink;
	public Color colorGreen;
	public Color targetColor;
	private bool called;
	


	// Use this for initialization
	void Start ()
	{
		colorManager = GameObject.Find("ColorManager").GetComponent<ColorManager>();
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		player = GameObject.Find("Player");
		initColorList();
	}
	

	void Update ()
	{
		
		if (timeLeft <= Time.deltaTime)
		{
			// transition complete
			// start a new transition
			if(!called)
			{
				targetColor = allColors[whichColor];
				whichColor++;
				timeLeft = 1.0f;
				called = true;
			}
			
		}
		else
		{
			called = false;
			// calculate interpolated color
			GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color, targetColor, Time.deltaTime / timeLeft);

			if(whichColor >= allColors.Count)
			{
				whichColor = 0;
			}

			// update the timer
			timeLeft -= Time.deltaTime;
			
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player")
		{
			audioManager.Play("ColorChange");
			if(restrictToColor) colorManager.updateColors(restrictToColorIndex);
			else colorManager.updateColors(); // new funct
			player.GetComponent<Player>().FillHealth();
			Destroy(gameObject);
			return;
		}
	}

	void initColorList()
	{
		allColors.Add(colorBlue);
		allColors.Add(colorYellow);
		allColors.Add(colorPink);
		allColors.Add(colorGreen);
	}
}
