using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static itemInventory;

public class itemInventory : MonoBehaviour
{

    [SerializeField] private Car carStats;

    Player_Input playerInput;
    Car_Inputs car;
    InputAction useItem;

    public enum fruitType
    {
        None,
        Chile,
        Mora,
        Cereza

    }

    [SerializeField] private int inventorySlot;

    [SerializeField] private fruitType[] powerList = new fruitType[2];
    private Rigidbody carRigidBody;
    private Collider carCollider;

    private void Awake()
    {
        carRigidBody = GetComponent<Rigidbody>();
        carCollider = GetComponent<BoxCollider>();
        car = new Car_Inputs();
    }

    private void Start()
    {
        useItem = car.FindAction("Throw");
        carStats.maxSpeed = 5;
    }

    private void OnEnable()
    {
        car.Enable();
    }

    private void OnDisable()
    {
        car.Disable();
    }
   
    void Update()
    {

        powerupManager();
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruta") && inventorySlot < 2)
        {
            Debug.Log(other.gameObject);
            other.gameObject.SetActive(false);
            powerList[inventorySlot] = other.GetComponent<fruitBehaviour>().fruit;
            inventorySlot += 1;
        }
    }


    IEnumerator Chile()
    {
        playerInput.speedLimit = 40;
        carStats.minSpeed = 2;
        carStats.maxSpeed = 10;
        yield return new WaitForSeconds(5f);
        carStats.minSpeed = 1;
        carStats.maxSpeed = 5;
        playerInput.speedLimit = 40;
        yield return null;
    }

    void powerupManager () 
    {
        if (useItem.IsPressed()) 
        {
            Debug.Log("Le picaste");
            MixFruits(powerList[0], powerList[1]);
        }
    }

    void MixFruits(fruitType fruitA, fruitType fruitB)
    {
        switch (fruitA, fruitB)
        {
            case (fruitType.Chile, fruitType.Chile):
               
                StartCoroutine(Chile());

                break;
            case (fruitType.Mora, fruitType.Mora):
                Debug.Log("Power Up X");
                break;



            default:
                Debug.Log("Si hace match con nada");
                break;
        }
    }
}
