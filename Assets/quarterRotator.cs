using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quarterRotator : MonoBehaviour
{
    public float frequency;
    [SerializeField] int speed = 100;
    public int sign = 1;
    public float timeDelay;
    public int rotationSign;
    public bool shouldChangeRotationRandomly = false;
    
     void Start()
     {
        if (frequency == null || frequency == 0) frequency = 0.5f;
        if (timeDelay == null || timeDelay == 0) timeDelay = 1f;
        StartCoroutine(RotateObject(90, Vector3.forward * sign, frequency));
        // 0.3 to 0.6 frequency is okay
     }
 
     IEnumerator RotateObject(float angle, Vector3 axis, float inTime)
     {
        if(randomize()) { sign = -sign; }

        // calculate rotation speed
        float rotationSpeed = angle / inTime;
 
        while (true)
        {
            // save starting rotation position
            Quaternion startRotation = transform.rotation;
 
            float deltaAngle = 0;
            
            

            // rotate until reaching angle
            if(angle > 0)
            {
                while (deltaAngle < angle)
                {
                    deltaAngle += rotationSpeed * Time.deltaTime;
                    deltaAngle = Mathf.Min(deltaAngle, angle); 
    
                    transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

                    // if should change rotation randomly then do so after rotation is complete
                    if(shouldChangeRotationRandomly && deltaAngle == angle && randomize()) angle = -angle;
    
                    yield return null;
                }
            } else if (angle < 0)
            {
                while (deltaAngle > angle)
                {
                    deltaAngle += rotationSpeed * Time.deltaTime;
                    deltaAngle = Mathf.Max(deltaAngle, angle); 
    
                    transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

                    // if should change rotation randomly then do so after rotation is complete
                    if(shouldChangeRotationRandomly && deltaAngle == angle && randomize()) angle = -angle;
    
                    yield return null;
                }
            }
            
            // delay here
            yield return new WaitForSeconds(timeDelay);
        }
    }

    public void setFrequency(float freq)
    {
        frequency = freq;
    }

    bool randomize()
    {
        if(Random.Range(0,2) >= 1)
        { 
            return true;
        }
        else return false;
    }
}
