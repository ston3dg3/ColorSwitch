using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stripeScript2 : MonoBehaviour
{
    public bool invisible;

    void Awake()
    {
        invisible = false;
    }

    void OnBecameInvisible()
    {
        invisible = true;
    }
}
