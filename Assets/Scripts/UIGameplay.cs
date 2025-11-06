using TMPro;
using UnityEngine;

public class UIGameplay : MonoBehaviour
{
    public positionsManager posicionJugador;
    public Check_Checkpoints check;
    public TextMeshProUGUI Posicion, Laps;
    void Start()
    {
        posicionJugador = GameObject.Find("positionManager").GetComponent<positionsManager>();
        check = GameObject.Find("beetle").GetComponent<Check_Checkpoints>();
    }

    // Update is called once per frame
    void Update()
    {
        Posicion.text = posicionJugador.GetPosition().ToString();
        Laps.text = "Laps:" + check.GetLaps() + "/3";
    }
    
}
