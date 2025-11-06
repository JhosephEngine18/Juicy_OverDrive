using System;
using TMPro;
using UnityEngine;

public class LapsManager : MonoBehaviour
{
    public Check_Checkpoints LapsScript;
    public positionsManager positionsScript;
    public int MaxLaps;
    public TextMeshProUGUI StateOfGame;

    void Start()
    {
        LapsScript = GameObject.Find("beetle").GetComponent<Check_Checkpoints>();
        positionsScript = GameObject.Find("beetle").GetComponent<positionsManager>();

        if (MaxLaps == 0)
        {
            MaxLaps = 1;
        }
        StateOfGame.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (LapsScript.GetLaps() == MaxLaps)
        {
            if (positionsScript.GetPosition() == 1)
            {
                StateOfGame.text = "You Win";
            }
            else
            {
                StateOfGame.text = "You Lose";
            }
            StateOfGame.enabled = true;
        }
    }
}
