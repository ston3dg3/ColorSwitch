using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{

    public GameObject player; // set to player
	public SpriteRenderer sr; // set to player sprite
	public AudioManager audioManager;
	float timeLeft;
	List<Color> allColors = new List<Color>();
	List<Colorr> availableColors = new List<Colorr>();

	public string currentColor;
	public int index;

	public Color colorBlue;
	public Color colorYellow;
	public Color colorPink;
	public Color colorGreen;
	public Colorr[] colour = new Colorr[4];

    // spawn with a color randomly chosen from 4 colours 
	// define an object 'colour' with all color properties: Color, colorName, index
	// 1. define an array (not list) of size 4 
	// in Start() add all 4 colour objects to the array
	// make sure each colorr has assigned index 
	// ex. 0 is always pink, 1 always blue etc.
	
	// // choose the starting color
	
	// colors[index]
	// remove this colour from the array
	// Update function: resets array and always removes newly assigned color from it


    void Start ()
	{
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		player = GameObject.Find("Player");
		sr = player.GetComponent<SpriteRenderer>();
		resetColors();
		updateColors();
	}

    public void updateColors(int desiredColor = -999) // call this each time a colorChanger is picked up (on colission2D)
	{
        

        if(desiredColor <= 3 && desiredColor >= 0)
        {
            setColor(colour[desiredColor].index);
        }
        else
        {
            int randomIndex = Random.Range(0, colour.Length-1);
            for (int i = 0; i < colour.Length; i++)
            {
                if(colour[i].active) availableColors.Add(colour[i]);
                colour[i].active = true;
            }
            setColor(availableColors[randomIndex].index);
            availableColors.Clear();
        }
	}

    void setColor(int indx)
	{
        colour[indx].active = false;
        sr.color = colour[indx].colour;
        currentColor = colour[indx].colorName;
		player.GetComponent<Player>().currentColor = colour[indx].colorName;
	}

    void resetColors()
    {
        colour[0].colorName = "blue"; colour[0].index = 0; colour[0].colour = colorBlue; colour[0].active = true;
		colour[1].colorName = "yellow"; colour[1].index = 1; colour[1].colour = colorYellow; colour[1].active = true;
		colour[2].colorName = "pink"; colour[2].index = 2; colour[2].colour = colorPink; colour[2].active = true;
		colour[3].colorName = "green"; colour[3].index = 3; colour[3].colour = colorGreen; colour[3].active = true;
    }

	

    public string getCurrentColor()
	{
		return currentColor;
	}
}

[System.Serializable]
public class Colorr
{
    public string colorName;
    public int index;
    public Color colour;
	public bool active;
}
