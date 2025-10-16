using System;
using UnityEngine;
using UnityEngine.InputSystem;

// This script should be attached to the root GameObject that has the Rigidbody component.
public class DriftController : MonoBehaviour
{
    // --- Public Parameters for Tuning ---

    [Header("Drift Settings")]
    [Tooltip("The rotation angle (in Y-axis Euler degrees) the car snaps to when drifting.")]
    public float targetDriftAngle = 45f;

    [Tooltip("The speed at which the car rotates into the target drift angle.")]
    public float rotationSnapSpeed = 10f;

    [Tooltip("The rotation angle limit (in Y-axis Euler degrees) beyond which the drift mode engages.")]
    public float driftActivationAngle = 10f;

    [Tooltip("Multiplier applied to linear velocity when initiating drift to maintain forward momentum.")]
    public float driftSpeedMultiplier = 0.8f;

    [Tooltip("The angular drag value applied during a drift to stop unwanted spinning.")]
    public float driftingAngularDrag = 5f;

    // --- Private Variables ---

    private Rigidbody rb;
    private float originalAngularDrag;
    private bool isDrifting = false;
    
    

    // --- Input & Initialization ---
    
    Car_Inputs car;
    private InputAction driftInput;

    private void Awake()
    {
        car = new Car_Inputs();
        driftInput = car.FindAction("Drift");
    }

    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("DriftController requires a Rigidbody component on the same GameObject!");
            enabled = false; // Disable script if no Rigidbody is found
            return;
        }

        // Store the original drag to restore it after drifting
        originalAngularDrag = rb.angularDamping;
    }

    // --- Main Logic ---

    void Update()
    {
        // 1. Check for Input (using Unity's standard Input Manager for a button named 'Fire1')
        // You should change "Fire1" to match your controller's drift button, e.g., "Jump" or a custom input.
        if (driftInput.IsPressed())
        {
            // Check if we meet the conditions to start drifting
            TryStartDrift();
        }
        else
        {
            // If the button is released, stop drifting
            StopDrift();
        }
    }

    void FixedUpdate()
    {
        if (isDrifting)
        {
            ApplyDriftRotation();
        }
    }

    // --- Methods ---

    private void TryStartDrift()
    {
        // Calculate the difference between forward direction and velocity direction
        Vector3 forward = transform.forward;
        Vector3 velocity = rb.linearVelocity;
        
        // Ignore vertical movement
        forward.y = 0;
        velocity.y = 0;

        // If the car isn't moving fast enough, don't drift
        if (velocity.magnitude < 1.0f) return;

        // Calculate the current drift angle (angle between the car's orientation and its movement direction)
        float currentDriftAngle = Vector3.Angle(forward, velocity);

        // Check if the car is currently sliding or turning enough to allow drift activation
        if (currentDriftAngle >= driftActivationAngle || isDrifting)
        {
            if (!isDrifting)
            {
                // Entering drift for the first time
                StartDrift();
            }
        }
        else
        {
            // Player is holding the button but the angle is too straight, so don't drift
            StopDrift();
        }
    }

    private void StartDrift()
    {
        isDrifting = true;
        
        // 1. Physics Tuning for Drift
        // Increase angular drag to prevent excessive spinning during the drift
        rb.angularDamping = driftingAngularDrag;

        // 2. Velocity Adjustment
        // Maintain a controlled forward speed while drifting
        rb.linearVelocity *= driftSpeedMultiplier; 
        
        Debug.Log("Drift Mode Engaged!");
    }

    private void StopDrift()
    {
        if (isDrifting)
        {
            isDrifting = false;
            
            // Restore original physics settings
            rb.angularDamping = originalAngularDrag;

            Debug.Log("Drift Mode Disengaged!");
        }
    }

    private void ApplyDriftRotation()
    {
        // 1. Determine the target rotation based on current movement direction
        
        // Get the direction of the car's current movement
        Vector3 movementDirection = rb.linearVelocity.normalized;

        // Determine if we are drifting left or right relative to the forward vector
        Vector3 forward = transform.forward;
        
        // Cross product determines the direction (positive Z for left drift, negative Z for right drift)
        float direction = Vector3.Cross(forward, movementDirection).y;

        // Base target angle is the direction of movement
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        
        // 2. Apply the 'Drift Snap' Offset
        float snapAngle = targetDriftAngle;

        // If 'direction' is positive (drifting left), rotate left (positive angle)
        // If 'direction' is negative (drifting right), rotate right (negative angle)
        float finalAngle = Mathf.Sign(direction) * snapAngle;

        // Apply the final angle offset to the rotation around the Y (up) axis
        Quaternion driftOffset = Quaternion.Euler(0, finalAngle, 0);
        targetRotation = targetRotation * driftOffset;

        // 3. Smoothly interpolate the car's rotation towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSnapSpeed);
        
        // Crucial: Reset the car's angular velocity to stop all spinning force
        rb.angularVelocity = Vector3.zero;
    }
}
