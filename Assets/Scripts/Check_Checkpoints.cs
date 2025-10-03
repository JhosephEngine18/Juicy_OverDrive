using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Check_Checkpoints : MonoBehaviour
{
    //public static event Action<bool> Reseting;
    public GameObject[] CheckpointsList;
    public GameObject Goal;
    int Laps = 0;
    int index = 0;
    int MaxCheckpoints;
    //public static event Action<int> OnLapChecked;

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
        Goal.SetActive(false);
        print(MaxCheckpoints);
    }
    private void Update()
    {

        if (index > 0)
        {
            CheckpointsList[index - 1].SetActive(false);
            if (CheckpointsList.Length > index)
            {
                CheckpointsList[index].SetActive(true);
            }
            if (CheckpointsList.Length == index)
            {
                CheckpointsList[0].SetActive(true);
                index = 0;
                Goal.SetActive(true);
            }

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (index < MaxCheckpoints && !other.CompareTag("Goal"))
        {
            index++;
        }

        if (other.CompareTag("Goal") == other)
        {
            Laps++;
            //OnLapChecked(Laps);
            Goal.SetActive(false);
        }

    }
}
