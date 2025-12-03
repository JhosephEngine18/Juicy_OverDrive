using System;
using System.Collections;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using Unity.Cinemachine;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class CrashScript : MonoBehaviour
{
    public EventReference Crash;
        private EventInstance Crashed;
    private void Awake()
    {
        shake.enabled = false;
        Crashed.setVolume(50);
    }

    public CinemachineBasicMultiChannelPerlin shake;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("npc") || other.gameObject.CompareTag("Walls"))
        {
            shake.enabled = true;
            RuntimeManager.PlayOneShotAttached(Crash, gameObject);
            StartCoroutine(StopShake());
        }
    }

    IEnumerator StopShake()
    {
        yield return new WaitForSeconds(0.5f);
        shake.enabled = false;
    }
}
