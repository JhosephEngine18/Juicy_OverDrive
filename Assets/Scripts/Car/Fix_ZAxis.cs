using System;
using UnityEngine;

public class Fix_ZAxis : MonoBehaviour
{
    private Rigidbody rb;
    private float lastPositionInX;
    private float lastPositionInY;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        lastPositionInX = rb.rotation.x;
        lastPositionInX = rb.rotation.y;
            
        if (rb.rotation.z > 0.1f || rb.rotation.z < -0.1f)
        {
            rb.MoveRotation(Quaternion.Euler(lastPositionInX, lastPositionInY, 0));
        }
    }
}
