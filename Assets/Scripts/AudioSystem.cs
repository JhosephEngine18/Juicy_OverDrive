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
    [Header("Background Music")]
    public GameObject BackgroundMusic;
    public StartRace StartRace;
    
    [Header("UI Reference")]
    public GameObject PauseMenu, OptionsMenu;
    
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
        BackgroundMusic.SetActive(false);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        driftInput = carInputs.FindAction("Drift");
        PitchCarInstance = RuntimeManager.CreateInstance(PitchCarSound);
        BakeCarInstance = RuntimeManager.CreateInstance(BakeCarSound);
        PitchCarInstance.start();
        PitchCarInstance.setVolume(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (driftInput.IsPressed() && rb.linearVelocity.magnitude > 1f)
        {
            RuntimeManager.PlayOneShotAttached(BakeCarSound, gameObject);
        }
        else if (driftInput.WasReleasedThisFrame() && rb.linearVelocity.magnitude > 1f)
        {
            BakeCarInstance.release();
        }
        PitchCarInstance.setPitch(Math.Clamp(rb.linearVelocity.magnitude * pitch, 0, 1));
        
        if (PauseMenu.activeInHierarchy|| OptionsMenu.activeInHierarchy)
        {
            PitchCarInstance.setVolume(0f);
        }
        else
        {
            PitchCarInstance.setVolume(2f);
        }
    }

    private void FixedUpdate()
    {
        if (StartRace.Race)
        {
            BackgroundMusic.SetActive(true);
        }
        
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
