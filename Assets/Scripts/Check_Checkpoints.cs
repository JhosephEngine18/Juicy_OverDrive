using UnityEngine;

public class Check_Checkpoints : MonoBehaviour
{
    int CheckpointsPassed;
    private void OnTriggerEnter(Collider other)
    {
        Checkpoints(other);
    }

    void Checkpoints(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint_0"))
        {
            other.gameObject.SetActive(false);
            CheckpointsPassed++;
            print(CheckpointsPassed);
        }

        if (other.gameObject.CompareTag("CheckPoint_1") && CheckpointsPassed == 1)
        {
            other.gameObject.SetActive(false);
            CheckpointsPassed++;
            print(CheckpointsPassed);
        }
        else if (other.gameObject.CompareTag("CheckPoint_1") && CheckpointsPassed != 1)
        {
            print("WrongCheckpoint");
        }

        if (other.gameObject.CompareTag("CheckPoint_2") && CheckpointsPassed == 2)
        {
            other.gameObject.SetActive(false);
            CheckpointsPassed++;
            print(CheckpointsPassed);
        }
        else if (other.gameObject.CompareTag("CheckPoint_2") && CheckpointsPassed != 2)
        {
            print("WrongCheckpoint");
        }

        if (other.gameObject.CompareTag("CheckPoint_3") && CheckpointsPassed == 3)
        {
            other.gameObject.SetActive(false);
            CheckpointsPassed++;
            print(CheckpointsPassed);
        }
        else if (other.gameObject.CompareTag("CheckPoint_3") && CheckpointsPassed != 3)
        {
            print("WrongCheckpoint");
        }

        if (other.gameObject.CompareTag("CheckPoint_3") && CheckpointsPassed == 4)
        {
            other.gameObject.SetActive(false);
            CheckpointsPassed++;
            print(CheckpointsPassed);
        }
        else if (other.gameObject.CompareTag("CheckPoint_3") && CheckpointsPassed != 4)
        {
            print("WrongCheckpoint");
        }

    }
}
