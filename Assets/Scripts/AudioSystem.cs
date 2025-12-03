using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private Rigidbody rb;
    [Header("Car Sound Effects")]
    public EventReference PitchCarSound;
    private EventInstance  PitchCarInstance;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PitchCarInstance = RuntimeManager.CreateInstance(PitchCarSound);
        PitchCarInstance.start();
        PitchCarInstance.setVolume(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        PitchCarInstance.setPitch(Math.Clamp(rb.linearVelocity.magnitude * 0.05f, 0, 1));
    }

    private void OnDestroy()
    {
        PitchCarInstance.release();
    }
}
