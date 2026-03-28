using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(AudioSource))]
public class SciFiPistolSound : MonoBehaviour
{
private XRGrabInteractable xrGrabInteractable;
    private AudioSource audioSource;

    private void Awake()
    {
        xrGrabInteractable = GetComponentInParent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f; 
    }

    void OnEnable()
    {
        // XRI Listeners
        xrGrabInteractable.activated.AddListener(OnActivated_StartShooting);
        xrGrabInteractable.deactivated.AddListener(OnDeactivated_StopShooting);

        SoundManager.Instance.OnVolumeChange += SoundManager_OnVolumeChange;
        UpdateVolume();
    }

    void OnDisable()
    {
        xrGrabInteractable.activated.RemoveListener(OnActivated_StartShooting);
        xrGrabInteractable.deactivated.RemoveListener(OnDeactivated_StopShooting);
        
        SoundManager.Instance.OnVolumeChange -= SoundManager_OnVolumeChange;
    }
    
    private void SoundManager_OnVolumeChange(object sender, EventArgs e) => UpdateVolume();

    private void UpdateVolume()
    {
        audioSource.volume = SoundManager.Instance.GetVolume();
    }

    private void OnActivated_StartShooting(ActivateEventArgs arg0) => audioSource.Play();
    private void OnDeactivated_StopShooting(DeactivateEventArgs arg0) => audioSource.Stop();
}
