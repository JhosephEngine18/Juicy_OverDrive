using System;
using UnityEngine;
using static itemInventory;

public class itemInventory : MonoBehaviour
{

    [SerializeField] private Car carStats;

    public enum fruitType
    {
        Chile,
        Mora,
        Cereza

    }

    [SerializeField] private int inventorySlot = 0;

    [SerializeField] private fruitType[] powerList;
    public GameObject[] fruitInventory = new GameObject[2];
    private Rigidbody carRigidBody;
    private Collider carCollider;
    [SerializeField] private bool isChileActive = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        if (other.CompareTag("Fruta"))
        {
            Debug.Log(other.gameObject);
            fruitInventory[inventorySlot] = other.gameObject;
            other.gameObject.SetActive(false);
            powerList[inventorySlot] = other.GetComponent<fruitBehaviour>().fruit;
        }
        manageFruitInventory();
    }

    void manageFruitInventory() 
    {
        if (fruitInventory[inventorySlot] != null && inventorySlot != 1)
        {
            inventorySlot++;
        }
    }

    void managePowerUps() 
    {
        if (fruitInventory[0].CompareTag("Chile") && fruitInventory[1].CompareTag("Chile")) 
        {
            Debug.Log("chile");
        }
    }

    void Chile()
    {
        if (isChileActive == true) 
        {
            carStats.maxSpeed = 10;
        }
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
