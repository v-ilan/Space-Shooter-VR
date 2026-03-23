using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRBaseInteractable))]
public class SimpleHapticTrigger : MonoBehaviour
{
    [Header("Haptic Settings")]
    [Range(0, 1)] [SerializeField] private float intensity = 0.5f;
    [SerializeField] private float duration = 0.1f;

    [Header("Reference")]
    [SerializeField] private XRBaseInteractable interactable;

    private void Awake()
    {
        // Automatically captures whatever version of Interactable is on the object
        if (interactable == null) interactable = GetComponent<XRBaseInteractable>();
    }

    private void OnEnable()
    {
        if (interactable != null)
            interactable.selectEntered.AddListener(HandleHaptic);
    }

    private void OnDisable()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(HandleHaptic);
    }

    private void HandleHaptic(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRBaseInputInteractor controller)
        {
            controller.SendHapticImpulse(intensity, duration);
        }
    }
}
