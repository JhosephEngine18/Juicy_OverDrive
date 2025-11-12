using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Car_Inputs carInputs;
    [Header("Front and Left Wheel Controls")]
    [SerializeField] private Player_Input FRWheelControl;
    [SerializeField] private Player_Input FLWheelControl;
    [Header("Required Car Components")]
    [SerializeField] private Rigidbody carRigidBody;
    [SerializeField] private Car carStats;

    private void Awake()
    {
        carInputs = new Car_Inputs();
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
        carRigidBody.AddTorque(Vector3.up * 40, ForceMode.Impulse);
        carStats.frontTireGrip = 0;
        carStats.backTireGrip = 0;
        yield return null;
    }
}
