using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Splotch_Effect : MonoBehaviour
{
    [SerializeField]private Car carStats;
    [SerializeField]private Player_Input FRWheelControl;
    [SerializeField]private Player_Input FLWheelControl;
    [SerializeField]private GameObject parent;
    public itemInventory splotch;
    private Rigidbody carRB;

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
        splotch.didSplotchHappen = true;
        Destroy(parent);
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
