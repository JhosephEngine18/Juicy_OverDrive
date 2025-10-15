using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class NPC : MonoBehaviour
{
    //Guarda los waypoints que tiene que cruzar el npc
    public GameObject[] _checkCheckpoints;
    private int index = 0;
    [SerializeField] private float TurningSpeed = 5f;
    [SerializeField] private float Movespeed = 5f; // Speed for wheel spinning
    private Vector3 direction;
    Quaternion rotation;
    Rigidbody rb;
    [SerializeField] private Transform WheelFR, WheelFL, WheelBR, WheelBL;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Quaternion currentRotation = gameObject.transform.rotation;
        Quaternion nextRotation = Quaternion.LookRotation(_checkCheckpoints[index].transform.position - gameObject.transform.position);
        gameObject.transform.rotation = new Quaternion(0f,Quaternion.RotateTowards(currentRotation, nextRotation, Time.deltaTime * TurningSpeed).y,0f,Quaternion.RotateTowards(currentRotation, nextRotation, Time.deltaTime * TurningSpeed).w);
        rotation = Quaternion.RotateTowards(currentRotation, nextRotation, Time.deltaTime * TurningSpeed);
        
        rb.AddForce(transform.forward * Movespeed);
        
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        //Itera el array de los checkpoints para determinar que checkpoint ira despues
        if (other.CompareTag("Checkpoints") && other.gameObject ==  _checkCheckpoints[index])
        {
            index++;
            Debug.Log("Found it");
            if (index >= _checkCheckpoints.Length)
            {
                index = 0;
            }
        }
    }
    
}