using System;
using UnityEngine;

public class itemInventory : MonoBehaviour
{
    Car carStats;
    Player_Input playerInput;

    public enum fruitType
    {
        Chile,
        Mora,

    }
    GameObject fruit;

    private fruitType[] powerList;
    public GameObject[] fruitInventory = new GameObject[2];
    private Rigidbody carRigidBody;
    private Collider carCollider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        carRigidBody = GetComponent<Rigidbody>();
        carCollider = GetComponent<BoxCollider>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        int inventorySlot = 0;
        if (collision.collider.CompareTag("Fruta"))
        {
            Debug.Log(collision.gameObject);
            fruitInventory[inventorySlot] = collision.gameObject;
            inventorySlot++;
        }
    }

    void Chile() 
    {
        
        carStats.maxSpeed = 7;
        
    }

    void MixFruits(fruitType fruitType) 
    {
        switch (fruitType)
        { 
            case fruitType.Chile:

                break;
            case fruitType.Mora:

                break;
            default:

                break;
        }
    }

}
