using TMPro;
using UnityEngine;

public class UIGameplay : MonoBehaviour
{
    public positionsManager posicionJugador;
    public Check_Checkpoints check;
    public TextMeshProUGUI Posicion, Laps;
    public LapsManager MaxLaps;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Posicion.text = posicionJugador.GetPosition().ToString();
        Laps.text = "Laps:" + check.GetLaps() + "/" + MaxLaps.MaxLaps;
    }
    
}
