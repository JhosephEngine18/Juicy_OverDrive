using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioSystem : MonoBehaviour
{
    public Car_Inputs carInputs;
    private InputAction driftInput;
    private Rigidbody rb;
    [Header("Engine Effects")]
    public EventReference PitchCarSound;
    private EventInstance  PitchCarInstance;
    public float pitch = 0.02f;
    [Header("Brake Effects")]
    public EventReference BakeCarSound;
    private EventInstance BakeCarInstance;
    
    private void OnEnable()
    {
        carInputs.Enable();
    }

    private void OnDisable()
    {
        carInputs.Disable();
    }

    private void Awake()
    {
        carInputs = new Car_Inputs();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        driftInput = carInputs.FindAction("Drift");
        PitchCarInstance = RuntimeManager.CreateInstance(PitchCarSound);
        BakeCarInstance = RuntimeManager.CreateInstance(BakeCarSound);
        PitchCarInstance.start();
        PitchCarInstance.setVolume(2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (driftInput.IsPressed())
        {
            RuntimeManager.PlayOneShotAttached(BakeCarSound, gameObject);
        }
        else if (driftInput.WasReleasedThisFrame())
        {
            BakeCarInstance.release();
        }
        PitchCarInstance.setPitch(Math.Clamp(rb.linearVelocity.magnitude * pitch, 0, 1));
    }

    private void OnDestroy()
    {
        PitchCarInstance.release();
    }

    public void stopAudio()
    {
        PitchCarInstance.stop(STOP_MODE.IMMEDIATE);
    }
}
