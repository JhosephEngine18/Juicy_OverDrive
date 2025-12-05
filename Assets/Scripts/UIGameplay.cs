using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIGameplay : MonoBehaviour
{
    public positionsManager posicionJugador;
    public Check_Checkpoints check;
    public TextMeshProUGUI Posicion, Laps, Countdown;
    public LapsManager MaxLaps;
    public StartRace StartRace;

    void Start()
    {
        StartCoroutine(Startgame());
    }

    // Update is called once per frame
    void Update()
    {
        Posicion.text = posicionJugador.GetPosition().ToString();
        Laps.text = "Laps:" + check.GetLaps() + "/" + MaxLaps.MaxLaps;
        
    }
    
    IEnumerator Startgame()
    {
        yield return new WaitForEndOfFrame();
        Countdown.text = "1";
        yield return new WaitForSeconds(1f);
        Countdown.text = "2";
        yield return new WaitForSeconds(1f);
        Countdown.text = "3";
        yield return new WaitForSeconds(1f);
        Countdown.text = "YA";
        yield return new WaitForSeconds(1.5f);
        Countdown.enabled = false;
    }
    
}
