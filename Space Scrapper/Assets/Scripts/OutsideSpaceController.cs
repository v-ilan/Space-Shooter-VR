using System;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class OutsideSpaceController : MonoBehaviour
{
    [Header("Input Hardware")]
    [SerializeField] private XRLever throttleLever;
    [SerializeField] private XRKnob steeringWheel;
    [SerializeField] private XRJoystick flightJoystick;

    [Header("Flight Settings")]
    [SerializeField] private float forwardSpeed = 5.0f;
    [SerializeField] private float strafeSpeed = 3.0f;
    [SerializeField] private float verticalSpeed = 5f;

    [Header("Visual Tilt (Feedback)")]
    [SerializeField] private Transform tiltPivot; // Position in the cockpit where rotation centers
    [SerializeField] private float maxPitchAngle = 20f;
    [SerializeField] private float maxRollAngle = 25f;
    [SerializeField] private float rotationSmoothing = 2f;

    private bool _isEngineActive;

    private void OnEnable()
    {
        if (throttleLever != null)
        {
            throttleLever.onLeverActivate.AddListener(OnEngineStart);
            throttleLever.onLeverDeactivate.AddListener(OnEngineStop);
        }
    }

    private void OnDisable()
    {
        if (throttleLever != null)
        {
            throttleLever.onLeverActivate.RemoveListener(OnEngineStart);
            throttleLever.onLeverDeactivate.RemoveListener(OnEngineStop);
        }
    }

    private void OnEngineStart() => _isEngineActive = true;
    private void OnEngineStop() => _isEngineActive = false;

    private void Update()
    {
        if (_isEngineActive)
        {
            HandleSpaceTranslation();
            //HandleSpaceRotation();
        }
        HandleSpaceRotation();
    }

    private void HandleSpaceTranslation()
    {
        // Strafe (Wheel)
        float xInput = Mathf.Lerp(-1f, 1f, steeringWheel.value);
        
        // Vertical (Joystick Y) -> Now the joystick moves the ship UP/DOWN
        float yInput = flightJoystick.value.y;

        Vector3 moveDir = new Vector3(xInput * strafeSpeed, yInput * verticalSpeed, forwardSpeed);
        
        // Move the World Parent
        transform.Translate(moveDir * Time.deltaTime, Space.Self);
    }

    private void HandleSpaceRotation()
    {
        if (tiltPivot == null) return;

        // Calculate intended Tilt based on Joystick
        // Note: We use the joystick input to define what the SHIP'S rotation should look like
        float targetPitch = flightJoystick.value.y * maxPitchAngle;
        float targetRoll = -flightJoystick.value.x * maxRollAngle;

        // Create the rotation we WANT the ship to have
        Quaternion targetRotation = Quaternion.Euler(targetPitch, 0, targetRoll);

        // 3. Apply the rotation RELATIVE to the pivot
        // We smoothly interpolate the world's rotation toward the target
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation, 
            targetRotation, 
            Time.deltaTime * rotationSmoothing
        );
    }
}
