using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfView : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform player; // set the player in unity GUI
    public GameObject Plyer;
    private float distance;
    public bool outOfCamera = false;
    
    void Start()
    {
        Plyer = GameObject.Find("Player");
        player = Plyer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(gameObject.GetComponent<Transform>().position, player.position);
        Debug.LogWarning("distance: "+distance);
        if(distance > 5f && outOfCamera) Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        outOfCamera = true;
    }
}
