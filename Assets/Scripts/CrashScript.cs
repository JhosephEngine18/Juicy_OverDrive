using System;
using System.Collections;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using Unity.Cinemachine;
using UnityEngine;
using EventHandler = FMODUnity.EventHandler;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class CrashScript : MonoBehaviour
{
    public EventReference Crash;
    public Transform cam;
    private EventInstance Crashed;
    private RigidBody rb;
    private void Awake()
    {
        shake.enabled = false;
        Crashed = RuntimeManager.CreateInstance(Crash);
    }

    public CinemachineBasicMultiChannelPerlin shake;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("npc") || other.gameObject.CompareTag("Walls"))
        {
            shake.enabled = true;
            RuntimeManager.AttachInstanceToGameObject(Crashed, cam);
            Crashed.start();
            StartCoroutine(StopShake());
        }
    }
    

    IEnumerator StopShake()
    {
        yield return new WaitForSeconds(0.5f);
        shake.enabled = false;
    }
}
