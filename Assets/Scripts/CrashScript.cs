using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CrashScript : MonoBehaviour
{
    private void Awake()
    {
        shake.enabled = false;
    }

    public CinemachineBasicMultiChannelPerlin shake;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("npc") || other.gameObject.CompareTag("Walls"))
        {
            shake.enabled = true;
            StartCoroutine(StopShake());
        }
    }

    IEnumerator StopShake()
    {
        yield return new WaitForSeconds(0.5f);
        shake.enabled = false;
    }
}
