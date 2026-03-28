using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Content.Interaction;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public event EventHandler OnVolumeChange;

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private const string PLAYER_PREFS_SFX_VOLUME = "SFXVolume";
    private float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance != null) 
        { 
            Destroy(gameObject); 
            return; 
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);

        sfxVolume = PlayerPrefs.GetFloat("PLAYER_PREFS_SFX_VOLUME", 1f);
    }

    private void Start()
    {
        //PlaySound(audioClipRefsSO.space, Vector3.zero);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * sfxVolume);
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void ChangeVolume(float volume)
    {
        sfxVolume = volume;
        OnVolumeChange?.Invoke(this, EventArgs.Empty);
        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, sfxVolume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return sfxVolume;
    }
}
