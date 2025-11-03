using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static itemInventory;

public class itemInventory : MonoBehaviour
{

    [SerializeField] private Car carStats;

    Car_Inputs car;
    InputAction useItem;

    public enum fruitType
    {
        None,
        Chile,
        Mora,
        Cereza

    }

    [SerializeField] private int inventorySlot = 0;

    [SerializeField] private fruitType[] powerList = new fruitType[2];
    //public GameObject[] fruitInventory = new GameObject[2];
    private Rigidbody carRigidBody;
    private Collider carCollider;
    [SerializeField] private bool isChileActive = false;


    private void Awake()
    {
        carRigidBody = GetComponent<Rigidbody>();
        carCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        Chile();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruta") && inventorySlot != 1)
        {
            Debug.Log(other.gameObject);
            other.gameObject.SetActive(false);
            powerList[inventorySlot] = other.GetComponent<fruitBehaviour>().fruit;
            if (inventorySlot != 1)
            {
                inventorySlot += 1;
            }
        }
    }


    void Chile()
    {
        if (isChileActive == true) 
        {
            carStats.maxSpeed = 10;
        }
    }

    void powerupInput() 
    {
        
    }

    void MixFruits(fruitType fruitA, fruitType fruitB)
    {
        switch (fruitA, fruitB)
        {
            case (fruitType.Chile, fruitType.Chile):
                

                break;
            case (fruitType.Mora, fruitType.Chile):
                Debug.Log("Power Up X");
                break;



            default:
                Debug.Log("Si hace match con nada");
                break;
        }
    }
}
