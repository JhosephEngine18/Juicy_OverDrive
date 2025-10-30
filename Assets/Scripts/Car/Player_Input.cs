using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input : MonoBehaviour
{
    //Referencia al ScriptableObject que contiene las stas de nuestro carro.
    [SerializeField] private Car carStats;
    //Referencia a nuestro script "Frontwheelturning"
    //Lo necesitamos para llamar el método getSteeringInput
    frontWheelTurning wheelTurning;

    private float T;
    private float Acceleration;
    private float Decceleration;
    private float speedLimit = 30f;
    private float currentSpeed;
    private float baseFrontTireGrip = 1f;
    private float baseBackTireGrip = 1;
    private float baseTireMass = 5f;
    private float driftFrontTireGrip = 0.1f;
    private float driftBackTireGrip = 0.1f;
    private float driftTireMass = 0f;
    [SerializeField] private bool isChileActive = false;
    [SerializeField] private bool hasRun = false;
    [SerializeField] private float turnSpeed = 65f;
    private Quaternion currentRotation;

    //Referencia a la clase de C# de nuestros Inputs
    Car_Inputs carInputs;

    //Declaramos las inputActions que vamos a usar en el código
    private InputAction playerDirection;
    private InputAction driftInput;

    //Vector3 que almacena el playerInput de la direccion a la que 
    //quiere acelarar el usuario (atrás o adelante)
    Vector3 accelerationDirection = Vector3.zero;

    //La direccion hacia la que quiero aplicar fuerza para acelerar
    //osea wheelTransform.forward
    public Vector3 rightMoveDirection;
    public Vector3 leftMoveDirection;

    [SerializeField] private Transform rightWheelTransform;
    [SerializeField] private Transform leftWheelTransform;
    private Rigidbody carRigidbody;

    private void Awake()
    {
        carStats.frontTireGrip = baseFrontTireGrip;
        carStats.backTireGrip = baseBackTireGrip;
        carStats.tireMass = baseTireMass;
        carInputs = new Car_Inputs();
        wheelTurning = GetComponent<frontWheelTurning>();
    }

    private void Start()
    {
        carRigidbody = gameObject.GetComponent<Rigidbody>();
        driftInput = carInputs.FindAction("Drift");
        playerDirection = carInputs.FindAction("Forward/Backward");
    }

    private void OnEnable()
    {
        carInputs.Enable();
    }

    private void OnDisable()
    {
        carInputs.Disable();
    }

    private void FixedUpdate()
    {
        
        Accelerate(rightMoveDirection,leftMoveDirection);
        manageDriftInput();
        Chile();
        
    }

    void getAccelerationDirection()
    {
        //Obtenemos la direccion de aceleracion del jugador (adelante/atrás)
        accelerationDirection = playerDirection.ReadValue<Vector3>();
        Debug.Log(accelerationDirection);
    }

    void Accelerate(Vector3 rightDirection, Vector3 leftMoveDirection)
    {
        //Debug.Log("Speed: "+carRigidbody.linearVelocity.magnitude);
        rightMoveDirection = rightWheelTransform.forward;
        leftMoveDirection = leftWheelTransform.forward;
        getAccelerationDirection();
        //Si la dirección en Z es == 1 nos movemos para delante, si es == -1 nos movemos para atrás
        if (accelerationDirection.z == 1)
        {
            
            Acceleration = Mathf.Lerp(carStats.minSpeed, carStats.maxSpeed, T);
            T += 1f * Time.deltaTime;
            carRigidbody.AddForceAtPosition(accelerationDirection * Acceleration, rightWheelTransform.position);
            carRigidbody.AddForceAtPosition(accelerationDirection * Acceleration, leftWheelTransform.position);
            checkVelocity();
        }
        else if (accelerationDirection.z == -1)
        {
            Acceleration = Mathf.Lerp(carStats.minSpeed, carStats.maxSpeed, T);
            T += 1f * Time.deltaTime * Acceleration;
            carRigidbody.AddForceAtPosition(accelerationDirection * Acceleration, rightWheelTransform.position);
            carRigidbody.AddForceAtPosition(accelerationDirection * Acceleration, leftWheelTransform.position);
            checkVelocity();
        }

    }

    //Función que se asegura de que el carro tenga un limite de velocidad, aplicando fueza
    //en la direccion contraria a su aceleración si se pasa de la velocidad límite
    void checkVelocity()
    {
        currentSpeed = carRigidbody.linearVelocity.magnitude;
        float offset = currentSpeed - speedLimit;
        getAccelerationDirection();
        if (accelerationDirection.z == 1)
        {
            if (currentSpeed > speedLimit)
            {
                carRigidbody.AddForceAtPosition(-rightMoveDirection * offset, rightWheelTransform.position);
                carRigidbody.AddForceAtPosition(-leftMoveDirection * offset, leftWheelTransform.position);
            }
        }
        else if (accelerationDirection.z == -1)
        {
            if (currentSpeed > Mathf.Abs(speedLimit))
            {
                carRigidbody.AddForceAtPosition(-rightMoveDirection * offset, rightWheelTransform.position);
                carRigidbody.AddForceAtPosition(-leftMoveDirection * offset, leftWheelTransform.position);
            }
        }
    }

    
    void manageDriftInput()
    {
        var currentSpeed = carRigidbody.linearVelocity.magnitude;
        float steeringDirection = getSteeringDirection(hasRun);
        hasRun = true;
        if (driftInput.IsPressed() & currentSpeed  > 1)
        {
            if (wheelTurning.getSteeringInput() != 0)
            {
                Drift(steeringDirection);
                
            }
        }
        else if(!driftInput.IsPressed() & carStats.frontTireGrip != baseFrontTireGrip)
        {
            float currentFrontTireGrip = carStats.frontTireGrip;
            float currentBackTireGrip = carStats.backTireGrip;
            //Lerpeamos los valores de vuelta a su valor original (lerpeamos para que se sienta mas suave la transición)
            //Ya que antes se sentía muy abrupto el cambio del estado de drift al estado "normal"
            carStats.frontTireGrip = Mathf.Lerp(currentFrontTireGrip, baseFrontTireGrip, 0.5f*Time.fixedDeltaTime); 
            carStats.backTireGrip = Mathf.Lerp(currentBackTireGrip, baseBackTireGrip, 0.5f*Time.fixedDeltaTime);
            carStats.tireMass = baseTireMass;
            hasRun = false;
        }
    }

    void Drift(float steeringDirection)
    {
        //obtengo rotacion actual
        currentRotation = carRigidbody.rotation;
        //cambio el valor de las siguientes variables del scriptable object Car
        carStats.frontTireGrip = driftFrontTireGrip;
        carStats.backTireGrip = driftBackTireGrip;
        carStats.tireMass = driftTireMass;

        //llamo el Metodo getSteeringInput de mi archivo frontWheelTurning
       
        
        carRigidbody.rotation = currentRotation*Quaternion.Euler(0f,steeringDirection*turnSpeed*Time.fixedDeltaTime , 0f);
    }

     float getSteeringDirection(bool hasRun)
    {
        if (hasRun == false)
        {
            return wheelTurning.getSteeringInput();
        }
        else
        {
            //Debug.Log("Esté metodo ya ha sido usado");
            return 0;
        }
    }

    

    [SerializeField] void Chile()
    {
        if (isChileActive == true)
        {
            Acceleration += 10;
            carStats.maxSpeed += 5;
            isChileActive = false;
        }
    }
}

