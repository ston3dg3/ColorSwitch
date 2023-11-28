using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalerScript : MonoBehaviour
{
    public bool shouldScale;
    public float timeToComplete;
    public float smallestBoundry;
    public float biggestBoundry;
    public float timeDelay;
    private float moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        if (timeToComplete == null || timeToComplete == 0) timeToComplete = 1f;
        if (smallestBoundry == null || smallestBoundry == 0) smallestBoundry = 0.8f;
        if (biggestBoundry == null || biggestBoundry == 0) biggestBoundry = 2.5f;
        if (timeDelay == null) timeDelay = 0f;
        moveSpeed = (Mathf.Abs(smallestBoundry)+Mathf.Abs(biggestBoundry)) / timeToComplete;
        if(timeDelay != null && timeDelay != 0)
        {
            shouldScale = false;
            Invoke("delayScaling", timeDelay);
        }
    }

    // Update is called once per frames
    void Update()
    {
        if (shouldScale) 
        {
            float calculatedSizeTransform = moveSpeed * Time.deltaTime;
            Vector3 wantedScale = new Vector3(calculatedSizeTransform, calculatedSizeTransform , 0);
            transform.localScale += wantedScale;
        }

        if (transform.localScale.x > biggestBoundry || transform.localScale.x < smallestBoundry)
        { 
            moveSpeed = -moveSpeed;
        }
    }
    
    void delayScaling()
    {
        shouldScale = true;
    }
}
