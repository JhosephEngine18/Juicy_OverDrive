using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static itemInventory;

public class itemInventory : MonoBehaviour
{
    [Header("Car Components")]
    public Player_Input FRWheelControl;
    public Player_Input FLWheelControl;
    [SerializeField]private Car carStats;
    [SerializeField]private Transform FRWheelTransform;
    [SerializeField]private Transform FLWheelTransform;
    [SerializeField]private Transform ThrowPoint;
    [Header("Item Prefabs")]
    public GameObject moraSplotch;
    public GameObject _CherryBomb;

    
    Car_Inputs car;
    InputAction useItem;
    [Header("Cherry Bomb Stats")]
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float upwardsForce = 1f;
    private Rigidbody BombRb;
    private bool isBombReady = false;
    [Header("Splotch Stats")]
    public bool didSplotchHappen = false;

    private bool itemOnUse = false;
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
    private Transform carTransform;
    private Collider carCollider;

    private void Awake()
    {
        isBombReady=true;
        carTransform = GetComponent<Transform>();
        carRigidBody = GetComponent<Rigidbody>();
        carCollider = GetComponent<BoxCollider>();
        car = new Car_Inputs();
    }

    private void Start()
    {
        useItem = car.FindAction("Throw");
        
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

        if (didSplotchHappen)
        {
            StartCoroutine(returnToNormal());
        }


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

    void powerupManager () 
    {
        if (useItem.WasPressedThisFrame() && !itemOnUse) 
        {
            Debug.Log("Le picaste");
            MixFruits(powerList[0], powerList[1]);
        }
    }
    
    IEnumerator Chile()
    {
        carStats.speedLimit += 15;
        carStats.maxSpeed += 10;
        carRigidBody.AddForceAtPosition(Vector3.forward * (100 * Time.fixedDeltaTime), FRWheelTransform.position);
        carRigidBody.AddForceAtPosition(Vector3.forward * (100 * Time.fixedDeltaTime), FLWheelTransform.position);
        itemOnUse = true;
        yield return new WaitForSeconds(5f);
        itemOnUse = false;
        carStats.minSpeed = 1;
        carStats.maxSpeed -= 10;
        carStats.speedLimit -= 15;
        powerList[0] = 0;
        powerList[1] = 0;
        yield return null;
    }

    IEnumerator Mora(GameObject moraSplotch) 
    {
        Vector3 lastPosition = new Vector3(carRigidBody.position.x,carRigidBody.position.y+0.1f,carRigidBody.position.z-3f);
        Instantiate(moraSplotch,lastPosition, Quaternion.Euler(90,0,0));
        powerList[0] = 0;
        powerList[1] = 0;

        yield return null;
    }

    IEnumerator CherryBomb() 
    {
       
        GameObject Bomb = Instantiate(_CherryBomb, ThrowPoint.transform.position, Quaternion.identity);
        BombRb = Bomb.GetComponent<Rigidbody>();
        Vector3 ForceToAdd = ThrowPoint.forward*(FRWheelControl.currentSpeed*throwForce) + ThrowPoint.up*(FRWheelControl.currentSpeed* upwardsForce);
        BombRb.AddForce(ForceToAdd, ForceMode.Impulse);
        powerList[0] = 0;
        powerList[1] = 0;
        yield return new WaitForSeconds(1f);
        isBombReady = true;
    }

    IEnumerator returnToNormal()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Returning to normal");
        carStats.frontTireGrip = 1;
        carStats.backTireGrip = 1;
        FRWheelControl.carInputs.Enable();
        FLWheelControl.carInputs.Enable();
        didSplotchHappen = false;
        yield return null;
    }

    void MixFruits(fruitType fruitA, fruitType fruitB)
    {
        switch (fruitA, fruitB)
        {
            case (fruitType.Chile, fruitType.Chile):
               
                StartCoroutine(Chile());
                inventorySlot = 0;

                break;
            case (fruitType.Mora, fruitType.Mora):

                StartCoroutine(Mora(moraSplotch));
                inventorySlot = 0;
                
                break;
            case (fruitType.Cereza, fruitType.Cereza):
                
                if (isBombReady)
                {
                    isBombReady = false;
                    StartCoroutine(CherryBomb());
                    inventorySlot = 0;
                }

                break;
            default:
                Debug.Log("Si hace match con nada");
                break;
        }
    }
}
