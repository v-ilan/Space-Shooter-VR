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

    [Header("Settings")]
    [SerializeField] private string boolParameterName = "Open";
    
    // Using a Hash is more performant than a String for Animators
    private int _openBoolHash;

    private void Awake()
    {
        _openBoolHash = Animator.StringToHash(boolParameterName);
        
        // Safety check
        if (interactableButton == null) 
            interactableButton = GetComponentInChildren<XRSimpleInteractable>();
    }

    private void OnEnable()
    {
        // Roadmap Item #4: Lifecycle Management
        if (interactableButton != null)
            interactableButton.selectEntered.AddListener(OnButtonSelected);
    }

    private void OnDisable()
    {
        if (interactableButton != null)
            interactableButton.selectEntered.RemoveListener(OnButtonSelected);
    }

    private void OnButtonSelected(SelectEnterEventArgs args)
    {
        ToggleDoor();
        TriggerHaptic(args.interactorObject);
    }

    private void ToggleDoor()
    {
        if (doorAnimator == null) return;

        bool currentState = doorAnimator.GetBool(_openBoolHash);
        doorAnimator.SetBool(_openBoolHash, !currentState);
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
