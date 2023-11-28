using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverScript : MonoBehaviour
{
    public bool shouldMove;
    public float timeToComplete;
    public float leftBoundry;
    public float rightBoundry;
    public float timeDelay;
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (timeToComplete == null || timeToComplete == 0) timeToComplete = 1f;
        if (leftBoundry == null || leftBoundry == 0) leftBoundry = -5f;
        if (rightBoundry == null || rightBoundry == 0) rightBoundry = 5f;
        if (timeDelay != null && timeDelay != 0)
        {
            shouldMove = false;
            Invoke("delayScaling", timeDelay);
        }
        float suppDiv = 1f;
        moveSpeed = (Mathf.Abs(leftBoundry)+Mathf.Abs(rightBoundry)) / (timeToComplete * suppDiv);
    }


    // IEnumerator MoveObject(float leftPos, float rightPos, float inTime)
    // {
    //     // calculate moving speed
    //     float moveSpeed = (Mathf.Abs(leftPos)+Mathf.Abs(rightPos)) / inTime;

    //     // save starting position
    //     Vector3 startPosition = transform.position;
 
    //     while (true)
    //     {
    //         if (transform.position.x == rightPos)
    //         // rotate until reaching angle
    //         while (transform.position.x < rightPos)
    //         {
    //             transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);
    //             yield return null;
    //         }
    //         while (transform.position.x < 0f)
    //         {
    //             this.gameObject.GetComponent<Transform>().Translate(moveSpeed * Time.deltaTime, 0f, 0f);
    //             yield return null;
    //         }
                
    //         // delay here
    //         yield return new WaitForSeconds(timeDelay);
    //     }
    // }

    void Update()
    {
        if (shouldMove) 
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f, Space.World);
        }

        if (transform.position.x > rightBoundry || transform.position.x < leftBoundry)
        { 
            moveSpeed = -moveSpeed;
        }
    }

    void delayScaling()
    {
        shouldMove = true;
    }
    
}
