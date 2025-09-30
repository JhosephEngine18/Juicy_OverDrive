using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class positionsManager : MonoBehaviour
{
    public carProgress player;           // Aqui se arrastra el script del jugadorrr
    public Transform startLine;          // Aqui se arrastra la linea de metaaa

    private List<GameObject> allCars = new List<GameObject>();
    private npcProgress[] npcs;          

    void Start()
    {
        // Aqui buscamos los npc con el tag de "npc" XD
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag("npc");
        npcs = npcObjects.Select(obj => obj.GetComponent<npcProgress>()).ToArray();

        // Aqui se agregan los npc y el jugador a la lista de posicionessss
        allCars.Clear();
        allCars.Add(player.gameObject);
        foreach (var npc in npcs)
        {
            allCars.Add(npc.gameObject);
        }
    }

    void Update()
    {
        // Esto ordena la prioridad de quien es el primer lugar, se basa en #1 las vueltas, #2 checkpoints para el jugador, #3 distanciaaa
        var orden = allCars
            .OrderByDescending(c =>
            {
                if (c == player.gameObject) return player.laps;
                else return c.GetComponent<npcProgress>().laps;
            })
            .ThenByDescending(c =>
            {
                if (c == player.gameObject) return player.currentCheckpoint;
                else return 0; 
            })
            .ThenBy(c =>
            {
                Transform target;

                if (c == player.gameObject)
                    target = player.NextCheckpoint; 
                else
                    target = startLine;             

                return Vector3.Distance(c.transform.position, target.position);
            })
            .ToList();

        // Posición del jugador
        int posicionJugador = orden.IndexOf(player.gameObject) + 1;
        Debug.Log("Posición del jugador: " + posicionJugador + " / " + allCars.Count);

        // Esto dibuja los raycast hacia los otros jugadores segun cuantos hayaaaaa
        foreach (var npc in npcs)
        {
            Vector3 start = player.transform.position + Vector3.up * 1f;
            Vector3 direction = npc.transform.position - player.transform.position;

            Debug.DrawRay(start, direction, Color.red);
        }
    }
}
