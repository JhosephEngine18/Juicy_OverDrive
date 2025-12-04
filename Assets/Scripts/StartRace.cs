using System;
using System.Collections;
using UnityEngine;

public class StartRace : MonoBehaviour
{
    private float counter = 0;
    public bool Race = false;
    public Player_Input[] playerInput;

    private void Awake()
    {
        StartCoroutine(Startgame());
    }

    void Start()
    {
        playerInput[0].enabled = false;
        playerInput[1].enabled = false;
    }

    IEnumerator Startgame()
    {
        yield return new WaitForEndOfFrame();
        counter = 1;
        yield return new WaitForSeconds(1.5f);
        counter = 2;
        yield return new WaitForSeconds(1.5f);
        counter = 3;
        yield return new WaitForSeconds(1f);
        Race = true;
        playerInput[0].enabled = true;
        playerInput[1].enabled = true;
    }

    public float GetCounter()
    {
        return counter;
    }
    
}
