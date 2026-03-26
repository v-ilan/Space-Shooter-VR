using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class OpenDoor : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private XRSimpleInteractable interactableButton;
    [SerializeField] private EnergySocketInteractor energySocketInteractor;

    [Header("Settings")]
    [SerializeField] private string boolParameterName = "Open";
    
    private bool _isPowered = false;
    private int _openBoolHash;    // Using a Hash is more performant than a String for Animators

    private void Awake()
    {
        _openBoolHash = Animator.StringToHash(boolParameterName);
        
        // Safety check
        if (interactableButton == null) 
        {
            interactableButton = GetComponentInChildren<XRSimpleInteractable>();
        }
    }

    private void OnEnable()
    {
        if (interactableButton != null)
        {
            interactableButton.selectEntered.AddListener(OnButtonSelected);
        }
        if (energySocketInteractor != null)
        {
            // Subscribe to XRI Select events
            energySocketInteractor.selectEntered.AddListener(OnPowerRestored);
            energySocketInteractor.selectExited.AddListener(OnPowerLost);

            // Initial Check: Is there already something in the socket?
            CheckInitialPowerState();
        }
    }


    private void OnDisable()
    {
        if (interactableButton != null)
        {
            interactableButton.selectEntered.RemoveListener(OnButtonSelected);
        }

        if (energySocketInteractor != null)
        {
            energySocketInteractor.selectEntered.RemoveListener(OnPowerRestored);
            energySocketInteractor.selectExited.AddListener(OnPowerLost);
        }
        
    }

    private void OnButtonSelected(SelectEnterEventArgs args)
    {
        ToggleDoor();
        //TriggerHaptic(args.interactorObject);
    }

    private void OnPowerRestored(SelectEnterEventArgs args)
    {
        _isPowered = true;
        ToggleDoor();
    }

    private void OnPowerLost(SelectExitEventArgs args)
    {
        _isPowered = false;
        ToggleDoor();
    }

    private void CheckInitialPowerState()
    {
        // If the socket already has an interactor (e.g. starting with a battery inside)
        _isPowered = energySocketInteractor.hasSelection;
        ToggleDoor();
    }

    private void ToggleDoor()
    {
        if(!_isPowered)
        {
            doorAnimator.SetBool(_openBoolHash, false);
        }
        else if (doorAnimator != null)
        {
            bool currentState = doorAnimator.GetBool(_openBoolHash);
            doorAnimator.SetBool(_openBoolHash, !currentState);
        }
    }

    private void TriggerHaptic(IXRSelectInteractor interactor)
    {
        // Provides that "click" feel in the controller
        // Check if the interactor that clicked the button supports haptics
        if (interactor is IXRHapticImpulseChannel hapticInteractor)
        {
            // Parameters: Amplitude (0-1), Duration (seconds)
            // 0.5f and 0.1s is a standard "click" feel.
            hapticInteractor.SendHapticImpulse(0.5f, 0.1f);
        }
    }
}
