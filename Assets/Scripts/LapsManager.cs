using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LapsManager : MonoBehaviour
{
    public Check_Checkpoints LapsScript;
    public positionsManager positionsScript;
    public int MaxLaps;
    public TextMeshProUGUI StateOfGame;
    public GameObject StateGameObject;
    public UnityEvent releaseAudio;

    void Start()
    {
        LapsScript = GameObject.Find("FruitCar").GetComponent<Check_Checkpoints>();
        if (MaxLaps == 0)
        {
            MaxLaps = 1;
        }
        StateGameObject.SetActive(false);
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
            StateGameObject.SetActive(true);
            StartCoroutine(RestartGame());
        }
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5);
        //
        releaseAudio.Invoke();
        SceneManager.LoadScene("Menu");
    }
}