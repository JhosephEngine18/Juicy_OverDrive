using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Check_Checkpoints : MonoBehaviour
{
    public static event Action<bool> Reseting;
    public GameObject[] CheckpointsList;
    int index = 0;
    int MaxCheckpoints;

    int CheckpointsPassed;
    private void Start()
    {
        for (int i = 1; i < CheckpointsList.Length; i++)
        {
            CheckpointsList[i].SetActive(false);
        }
        for (int i = 0; i < CheckpointsList.Length; i++)
        {
           MaxCheckpoints = i + 1;
        }

        print(MaxCheckpoints);
    }
    private void Update()
    {
        if (index == MaxCheckpoints)
        {
            CheckpointsList[index].SetActive(false);
            index = 0;
            CheckpointsList[0].SetActive(true);
            print("reseted");
        }

        if (index >  0)
        {
            CheckpointsList[index-1].SetActive(false);
            CheckpointsList[index].SetActive(true);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        index++;
        //Checkpoints(other);
    }

    void Checkpoints(Collider other)
    {
        /*for (int i = 0; i < CheckpointsList.Length; i++)
        {
            if (CheckpointsList[i] == other.gameObject)
            {
                other.gameObject.SetActive(false);
            }
        }


        /*if (other.gameObject.CompareTag("CheckPoint_0"))
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

        IEnumerator DisableReset()
        {
            yield return new WaitForSeconds(0.5f);
            Reseting(false);
        }

        if (CheckpointsPassed == 4)
        {
            Reseting(true);
            StartCoroutine(DisableReset());
        }*/
    }
}
