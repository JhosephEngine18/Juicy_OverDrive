using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class frontWheelTurning : MonoBehaviour
{
    //Hacemos refencia al scriptable object que guarda las variables de nuestro carro
    [SerializeField] private Car carStats;
    private Rigidbody carRigidbody;

    //Referencia a nuestra clase de C# de nuestro sistema de inputs
    Car_Inputs car;
    //InputAction que va a ser el steer del jugador
    InputAction playerDirection;

    private Transform wheelTransform;

    private Vector2 steeringInput; //Vector2 que va a almacenar la direccion en la que gire el usuario
    private Vector3 steeringDirection; //La dirección en X del transform de nuestra rueda.
    private Vector3 tireWorldVelocity; //Variable que almacena la velocidad en Z de nuestra llanta
    
    private float currentRotation;

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
        //La dirección en X del transform, siempre queremos que al dar vuelta, nuestra llanta
        //ofrezca resistencia en X, de lo contrario el carro se va a deslizar como si estuviera
        //en hielo.
        steeringDirection = wheelTransform.right;
        //La velocidad en Z de la llanta.
        tireWorldVelocity = carRigidbody.GetPointVelocity(wheelTransform.position);
        //Dot product entre La direccion en X y la direccion en Z
        float steeringVelocity = Vector3.Dot(steeringDirection, tireWorldVelocity);
        //Queremos crear resistencia en X por lo que nuestro turningRate(Lo que tardamos en girar)
        //sea -steeringVelocity * el grip de la llanta delantera
        float turningRate = -steeringVelocity * carStats.frontTireGrip;
        
        //addforce at position (el transform de las llantas) usando la formula usada a continuación.
        //realmente no entiendo bien las matemáticas detrás de eso, perdon
        carRigidbody.AddForceAtPosition(steeringDirection * carStats.tireMass * turningRate, wheelTransform.position);
        
        //Guardamos la dirección de giro del jugador en un Vector2
        steeringInput = playerDirection.ReadValue<Vector2>();
        //Movemos la rotacion del transform de las ruedas hacia steeringInput en X multiplicado por TurnSpeed.
        wheelTransform.localRotation = Quaternion.Euler(0, steeringInput.x * carStats.turnSpeed, 0);
    }

    public float getSteeringInput()
    {
        return steeringInput.x;

    }
}
