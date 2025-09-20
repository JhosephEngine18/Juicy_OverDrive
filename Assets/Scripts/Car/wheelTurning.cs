using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class wheelTurning : MonoBehaviour
{
    [SerializeField] private Car carStats;

    Car_Inputs car;
    InputAction playerDirection;

    Vector2 turnDirection;

    private Transform wheelTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
       wheelTransform = gameObject.GetComponent<Transform>();
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
        turnDirection = playerDirection.ReadValue<Vector2>();
        Quaternion rotationAmount = Quaternion.Euler(0, turnDirection.x * carStats.turnSpeed, 0);

        wheelTransform.rotation = rotationAmount;
    }
}
