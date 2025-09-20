using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class frontWheelTurning : MonoBehaviour
{
    [SerializeField] private Car carStats;
    private Rigidbody carRigidbody;

    Car_Inputs car;
    InputAction playerDirection;

    private Transform wheelTransform;

    private Vector2 steeringInput;
    private Vector3 steeringDirection;
    private Vector3 tireWorldVelocity;
    private void Start()
    {
        wheelTransform = gameObject.GetComponent<Transform>();
       carRigidbody = GetComponentInParent<Rigidbody>();
    }
    private void Awake()
    {
        car = new Car_Inputs();
        playerDirection = car.FindAction("Turning");
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
        wheelControl();
    }

    void wheelControl()
    {
        steeringDirection = wheelTransform.right;
        tireWorldVelocity = carRigidbody.GetPointVelocity(wheelTransform.position);
        float steeringVelocity = Vector3.Dot(steeringDirection, tireWorldVelocity);
        float turningRate = -steeringVelocity * carStats.frontTireGrip;
        float AccelerationRate = turningRate / Time.fixedDeltaTime;
        
        carRigidbody.AddForceAtPosition(steeringDirection * carStats.tireMass * turningRate, wheelTransform.position);
        
        steeringInput = playerDirection.ReadValue<Vector2>();
        wheelTransform.localRotation = Quaternion.Euler(0,steeringInput.x * carStats.turnSpeed , 0);
    }
}
