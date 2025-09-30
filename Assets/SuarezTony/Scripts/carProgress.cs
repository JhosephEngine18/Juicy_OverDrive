using UnityEngine;

public class carProgress : MonoBehaviour
{
    public Check_Checkpoints trackCheckpoints; // Esto referencia al script de los checkpointssss :D
    public int currentCheckpoint = 0;   
    public int laps = 0;


    public Transform NextCheckpoint
    {
        get
        {
            if (trackCheckpoints == null || trackCheckpoints.CheckpointsList.Length == 0)
                return null;

            int index = currentCheckpoint % trackCheckpoints.CheckpointsList.Length;
            return trackCheckpoints.CheckpointsList[index].transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {


        int checkpointIndex = other.GetComponent<Checkpoint>().index;

        if (checkpointIndex == currentCheckpoint)
        {
            currentCheckpoint++;

            if (currentCheckpoint >= trackCheckpoints.CheckpointsList.Length)
            {
                laps++;
                currentCheckpoint = 0;
            }
        }

    }
}
