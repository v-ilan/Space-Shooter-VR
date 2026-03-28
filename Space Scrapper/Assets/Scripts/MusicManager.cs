using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    private AudioSource musicSource;
    private float volume = 0.3f;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        musicSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 1f);
        musicSource.volume = volume;
        musicSource.loop = true;
    }

    private void Start() 
    {
        PlayBackgroundAmbient();
    }

    public void PlayBackgroundAmbient() 
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void ChangeVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
