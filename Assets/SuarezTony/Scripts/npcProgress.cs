using UnityEngine;

public class npcProgress : MonoBehaviour
{
    public int laps = 0;                  
    public Transform startLine;           
    private Vector3 lastPosition;         
    public float lapThreshold = 1f;      

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        
        Vector3 toStart = startLine.position - transform.position;

        
        if (Vector3.Distance(transform.position, startLine.position) < lapThreshold &&
            Vector3.Dot(transform.forward, toStart) < 0) 
        {
            laps++;
        }

        lastPosition = transform.position;
    }
}
