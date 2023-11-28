using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator CamShake(float duration, float magnitude)
    {
        float originalZ = transform.position.z;
        originalZ = -10f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
                float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
                float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

                transform.localPosition += new  Vector3(xOffset, yOffset, originalZ);

                elapsedTime += Time.deltaTime * 10;

                // wait one frame
                yield return null;
            
        }
    }
}


// DEStory objects out of camera view - optimise (if BElOW CAMERA and OUTSIDE CAMERA)
// call another object from another:
// myObject.GetComponent<MyScript>().MyFunction();
