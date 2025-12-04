using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Car_Inputs carInputs;
    [Header("Front and Left Wheel Components")]
    private GameObject FRWheel;
    private GameObject FLWheel;
    public Player_Input FRWheelControl;
    public Player_Input FLWheelControl;
    [Header("Required Car Components")]
    private GameObject Beetle;
    public Rigidbody carRigidBody;
    public Car carStats;

    private BombBehaviour _bombBehaviour;

    private void Awake()
    {
        
        carInputs = new Car_Inputs();
    }

    private void Start()
    {
        Beetle = GameObject.FindWithTag("car");
        FRWheel = GameObject.FindWithTag("FRWheel");
        FLWheel = GameObject.FindWithTag("FLWheel");
        FRWheelControl = FRWheel.GetComponent<Player_Input>();
        FLWheelControl = FLWheel.GetComponent<Player_Input>();
        carRigidBody = Beetle.GetComponent<Rigidbody>();
        _bombBehaviour = GetComponentInParent<BombBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("car")) 
        {
            StartCoroutine(BombEffect());
            
        }
    }

    IEnumerator BombEffect() 
    {
        carRigidBody.linearVelocity = Vector3.zero;
        FRWheelControl.carInputs.Disable();
        FLWheelControl.carInputs.Disable();
        carStats.frontTireGrip = 0;
        carStats.backTireGrip = 0;
        carRigidBody.AddTorque(Vector3.up * 40, ForceMode.Impulse);
        yield return StartCoroutine(returnToNormal());
        _bombBehaviour.exploded = true;
        Destroy(gameObject);
        yield return null;
    }

    IEnumerator returnToNormal()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Returning to normal");
        carStats.frontTireGrip = 1;
        carStats.backTireGrip = 1;
        FRWheelControl.carInputs.Enable();
        FLWheelControl.carInputs.Enable();
        yield return null;
    }
}
