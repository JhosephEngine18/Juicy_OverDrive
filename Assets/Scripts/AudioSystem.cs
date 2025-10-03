using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocity == Vector3.zero)
        {
            
        }
    }
}
