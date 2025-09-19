using UnityEngine;

public class ResetCheckPoints : MonoBehaviour
{
    public GameObject[] Checkpoints;
    
    private void OnEnable()
    {
        //Check_Checkpoints.Reseting += ResetCheckPoints;
    }

    private void OnDisable()
    {
        //Check_Checkpoints.Reseting -= ResetCheckPoints;
    }
    void ResetCheckpoints(bool Reset)
    {
        if (Reset)
        {
            for (int i = 0; i < Checkpoints.Length; i++)
            {
                Checkpoints[i].SetActive(true);
            }
        }
    }
}
