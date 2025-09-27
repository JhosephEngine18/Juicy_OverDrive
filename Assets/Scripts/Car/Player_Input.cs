using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input : MonoBehaviour
{
    [SerializeField] private Car carStats;

    private float T =0.0f;
    private float Acceleration;
    private float Decceleration;
    private float speedLimit = 30;
    private float currentSpeed;
    
    Car_Inputs car;
    private InputAction playerDirection;

    Vector3 accelerationDirection = Vector3.zero;
    private Vector3 moveDirection;
    Vector2 movementDirection;
    
    private Transform wheelTransform;
    private Rigidbody carRigidbody;
    
    private void Start()
    {
        wheelTransform = gameObject.GetComponent<Transform>();
        carRigidbody = gameObject.GetComponentInParent<Rigidbody>();
    }
    private void Awake()
    {
        car = new Car_Inputs();
        playerDirection = car.FindAction("Forward/Backward");
    }
    
    private void OnEnable()
    {
        car.Enable();
    }

    private void OnDisable()
    {
        car.Disable();
    }

    private void FixedUpdate()
    {
        moveDirection = wheelTransform.forward;
        Accelerate(moveDirection);
        Debug.Log(accelerationDirection);
        
    }

    void Accelerate(Vector3 direction)
    {
        accelerationDirection = playerDirection.ReadValue<Vector3>();
        if (accelerationDirection.z == 1)
        {
            Acceleration = Mathf.Lerp(carStats.minSpeed, carStats.maxSpeed, T);
            T += 0.1f * Time.deltaTime;
            carRigidbody.AddForceAtPosition(moveDirection * Acceleration, wheelTransform.position);
            checkVelocity();
        }else if (accelerationDirection.z == -1)
        {
            Acceleration = Mathf.Lerp(carStats.minSpeed, carStats.maxSpeed, T);
            T += 0.1f * Time.deltaTime* Acceleration;
            carRigidbody.AddForceAtPosition(-moveDirection * Acceleration, wheelTransform.position);
            checkVelocity();
        }
        
    }

    void checkVelocity()
    {
        currentSpeed = carRigidbody.linearVelocity.magnitude;
        float offset = currentSpeed - speedLimit;
        if (currentSpeed > speedLimit)
        {
            carRigidbody.AddForceAtPosition(-moveDirection * offset, wheelTransform.position);
            Debug.Log("car speed: " + carRigidbody.linearVelocity.magnitude);
        }
    }
}
