using System;
using Unity.Mathematics;
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
    private Quaternion minDriftAngle = new Quaternion(0,0,0,1);
    private Quaternion maxDriftAngle = new Quaternion(0,-1,0,1);
    
    Car_Inputs car;
    private InputAction playerDirection;
    private InputAction driftInput;

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
        driftInput = car.FindAction("Drift");
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
        manageDriftInput(minDriftAngle,maxDriftAngle);

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

    void manageDriftInput(Quaternion minDriftAngle, Quaternion maxDriftAngle)
    {
        Quaternion currentRotation = carRigidbody.rotation;
        if (driftInput.IsPressed())
        {
           Drift(currentRotation, minDriftAngle, maxDriftAngle);
        }
    }

    void Drift(Quaternion currentRotation, Quaternion minDriftAngle, Quaternion maxDriftAngle)
    {
        Debug.Log("current Rotation: " + currentRotation.z);
        if (carRigidbody.rotation.y < maxDriftAngle.y)
        {
            carRigidbody.rotation = currentRotation * Quaternion.Lerp(minDriftAngle, maxDriftAngle, 0.1f * Time.fixedDeltaTime);
        }
    }
}
