using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRBaseInteractable))]
public class GrabPhysicsStabilizer : MonoBehaviour
{
    private Rigidbody _rb;
    private XRBaseInteractable _interactable;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _interactable = GetComponent<XRBaseInteractable>();
    }

    private void OnEnable()
    {
        // Subscribe to XRI events
        _interactable.selectEntered.AddListener(HandleGrab);
        _interactable.selectExited.AddListener(HandleRelease);
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks!
        _interactable.selectEntered.RemoveListener(HandleGrab);
        _interactable.selectExited.RemoveListener(HandleRelease);
    }

    private void HandleGrab(SelectEnterEventArgs args)
    {
        // When held, use high-accuracy collision to prevent tunneling through walls
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        
        // Ensure interpolation is on while moving
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void HandleRelease(SelectExitEventArgs args)
    {
        // When dropped, go back to cheap Discrete physics
        _rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        _rb.interpolation = RigidbodyInterpolation.None;
    }
}
