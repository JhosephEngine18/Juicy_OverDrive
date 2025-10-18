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
    float timer = 1;
    public Transform[] Wheels = new Transform[4];
    private float currentSpeed;
    Vector3 initialRotation;
    float currentwheelrotation;

    
    void Start()
    {
        initialRotation = new Vector3(1f, 1f,1f);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Quaternion currentRotation = gameObject.transform.rotation;
        Quaternion nextRotation = Quaternion.LookRotation(_checkCheckpoints[index].transform.position - gameObject.transform.position);
        gameObject.transform.rotation = new Quaternion(0f,Quaternion.RotateTowards(currentRotation, nextRotation, Time.deltaTime * TurningSpeed).y,0f,Quaternion.RotateTowards(currentRotation, nextRotation, Time.deltaTime * TurningSpeed).w);
        rotation = Quaternion.RotateTowards(currentRotation, nextRotation, Time.deltaTime * TurningSpeed);
        
        isStuck();
        WheelsAnimation();
        currentSpeed = rb.linearVelocity.magnitude;
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * Movespeed);
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


    void isStuck()
    {
        if (rb.linearVelocity.magnitude <= 0.5)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                rb.AddForce(transform.up * 2f, ForceMode.Impulse);
            }
        }
        else
        {
            timer = 1;
        }
    }

    void WheelsAnimation()
    {
        Wheels[0].Rotate(1f * currentSpeed, 0f, 0f);
        Wheels[1].Rotate(1f * currentSpeed,0f, 0f);
        Wheels[2].Rotate(1f * currentSpeed, 0f, 0f);
        Wheels[3].Rotate(1f *currentSpeed, 0f, 0f);
        currentwheelrotation = Wheels[2].rotation.y;
    }
}