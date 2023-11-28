using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public bool ignoreMovement;
    public float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if(movementSpeed == null || movementSpeed == 0f) movementSpeed = 10f;
        
        if(!ignoreMovement)
        {
            transform.Rotate(0, 0, 90);
            Destroy(gameObject, 5);
        } 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!ignoreMovement)
        transform.Translate(Vector3.down * Time.deltaTime * movementSpeed);
    }
}
