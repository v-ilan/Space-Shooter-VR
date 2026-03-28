using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Content.Interaction;

[RequireComponent(typeof(AudioSource))]
public class XRLeverSound : MonoBehaviour
{
    [SerializeField] private XRLever xrLever;
    private AudioSource audioSource;

    private void Awake()
    {
        if (xrLever == null) xrLever = GetComponent<XRLever>();
        audioSource = GetComponent<AudioSource>();

        // Engine Polish
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        
        // Set to 0.5f for "Spatial-Stereo" - feels like it's all around the ship
        audioSource.spatialBlend = 0.5f; 
    }

    void Start()
    {
        xrLever.onLeverActivate.AddListener(OnEngineStart);
        xrLever.onLeverDeactivate.AddListener(OnEngineStop);

        SoundManager.Instance.OnVolumeChange += SoundManager_OnVolumeChange;
        UpdateVolume();
    }

    void OnDisable()
    {
        xrLever.onLeverActivate.RemoveListener(OnEngineStart);
        xrLever.onLeverDeactivate.RemoveListener(OnEngineStop);
        
        SoundManager.Instance.OnVolumeChange -= SoundManager_OnVolumeChange;
    }

    private void SoundManager_OnVolumeChange(object sender, EventArgs e) => UpdateVolume();

    private void UpdateVolume() => audioSource.volume = SoundManager.Instance.GetVolume();

    private void OnEngineStart() => audioSource.Play();

    private void OnEngineStop() => audioSource.Stop();
}
