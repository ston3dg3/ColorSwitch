using UnityEngine;

public class rotator : MonoBehaviour {

	[SerializeField] int speed = 100;

	// Update is called once per frame
	void Update () {
		transform.Rotate(0f, 0f, speed * Time.deltaTime);
	
	}
	public void setSpeed(int sped)
	{
		speed = sped;
	}
}
