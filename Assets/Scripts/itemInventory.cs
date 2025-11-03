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

    }
    GameObject fruit;

    private fruitType[] powerList = new fruitType[2];
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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Chile();
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
        if (isChileActive == true) 
        {
            carStats.maxSpeed = 10;
        }
    }

    void MixFruits(fruitType fruitA, fruitType fruitB)
    {
        switch (fruitA, fruitB)
        {
            case (fruitType.Chile, fruitType.Mora):
            case (fruitType.Mora, fruitType.Chile):
                Debug.Log("Power Up X");
                break;



            default:
                Debug.Log("Si hace match con nada");
                break;
        }
    }
}
