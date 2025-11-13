using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Splotch_Effect : MonoBehaviour
{
    [SerializeField]private Car carStats;
    [Header("Needed Car Components")]
    private GameObject Beetle;
    private GameObject FRWheel;
    private GameObject FLWheel;
    [SerializeField]private Player_Input FRWheelControl;
    [SerializeField]private Player_Input FLWheelControl;
    [SerializeField]private GameObject parent;
    private itemInventory splotch;
    private Rigidbody carRB;

    private void Start()
    {
        Beetle = GameObject.FindWithTag("car");
        FRWheel = GameObject.FindWithTag("FRWheel");
        FLWheel = GameObject.FindWithTag("FLWheel");
        splotch = Beetle.GetComponent<itemInventory>();
        FRWheelControl = FRWheel.GetComponent<Player_Input>();
        FLWheelControl = FLWheel.GetComponent<Player_Input>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("car"))
        { 
            carRB = other.GetComponent<Rigidbody>();
            
            StartCoroutine(SplotchEffect());
            

        }
    }
    

    IEnumerator SplotchEffect()
    {
        carRB.linearVelocity = Vector3.zero;
        FRWheelControl.carInputs.Disable();
        FLWheelControl.carInputs.Disable();
        carRB.AddTorque(Vector3.up*40, ForceMode.Impulse);
        carStats.frontTireGrip = 0;
        carStats.backTireGrip = 0;
        Destroy(parent);
        yield return StartCoroutine(returnToNormal());
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
