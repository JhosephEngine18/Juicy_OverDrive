using UnityEngine;

public class camera_FollowPlayer : MonoBehaviour
{

    public Vector3 offset = new Vector3(0, 0, 5);
    public Transform playerTransform;

    private Vector3 playerPosition;
    
    void Update()
    {
        playerPosition = playerTransform.position;
       Camera.main.transform.position = Vector3.MoveTowards(playerPosition - offset, playerPosition, 10 * Time.deltaTime); 
    }
}
