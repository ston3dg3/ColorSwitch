using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {


	public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
		Time.timeScale = 1f;
    }

	public void LoadNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		Time.timeScale = 1f;
	}

	public GameObject blckSquare;
	public float fady = 0.85f;

	void Start()
	{
		if(GameObject.Find("ImageLose"))
		{
			blckSquare = GameObject.Find("ImageLose");
		}
		
	}

    public IEnumerator FadeToBlack(GameObject blackSquare, bool fadeToBlack = true, int fadeSpeed = 6)
	{
		Color objectColor;

		if(blackSquare == null) 
		{
			objectColor = blckSquare.GetComponent<Image>().color;	
		}
		else 
		{
			objectColor = blackSquare.GetComponent<Image>().color;
		}
		float fadeAmount;

		if (fadeToBlack)
		{
			while (blackSquare.GetComponent<Image>().color.a < fady)
			{
				fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

				objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
				blackSquare.GetComponent<Image>().color = objectColor;
				yield return null;
			}
		} else
		{
			while (blackSquare.GetComponent<Image>().color.a > 0)
			{
				fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

				objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
				blackSquare.GetComponent<Image>().color = objectColor;
				yield return null;
			}
		}

		yield return new WaitForEndOfFrame();
	}

}
