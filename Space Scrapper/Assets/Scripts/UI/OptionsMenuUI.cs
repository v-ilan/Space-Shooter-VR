using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using System;

public class OptionsMenuUI : MonoBehaviour
{
    private enum SubMenu { Options, Locomotion, None }

    [Header("UI Pages")]
    [SerializeField] private GameObject mainMenuPage;
    [SerializeField] private GameObject locomotionPage;

    [Header("Buttons")]
    [SerializeField] private Button locomotionButton;
    [SerializeField] private List<Button> returnButtons;

    [Header("Sound")]
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        // One-time setup for persistent button listeners
        locomotionButton.onClick.AddListener(() => ShowPage(SubMenu.Locomotion));
        
        foreach (var btn in returnButtons)
        {
            btn.onClick.AddListener(() => ShowPage(SubMenu.Options));
        }
    }

    private void Start()
    {
        // Sync Sliders to current Manager values whenever UI is opened
        musicSlider.value = MusicManager.Instance.GetVolume();
        sfxSlider.value = SoundManager.Instance.GetVolume();

        // Setup Sound Listeners
        musicSlider.onValueChanged.AddListener(HandleMusicChange);
        sfxSlider.onValueChanged.AddListener(HandleSFXChange);

        UpdateVisuals();

        ShowPage(SubMenu.Options);
    }

    private void OnDisable()
    {
        // Clean up Slider listeners to prevent memory leaks/multiple triggers
        musicSlider.onValueChanged.RemoveListener(HandleMusicChange);
        sfxSlider.onValueChanged.RemoveListener(HandleSFXChange);
    }

    private void HandleMusicChange(float value)
    {
        MusicManager.Instance.ChangeVolume(value);
        UpdateVisuals();
    }

    private void HandleSFXChange(float value)
    {
        SoundManager.Instance.ChangeVolume(value);
        UpdateVisuals();
    }

    private void ShowPage(SubMenu targetPage)
    {
        mainMenuPage.SetActive(targetPage == SubMenu.Options);
        locomotionPage.SetActive(targetPage == SubMenu.Locomotion);
    }

    private void UpdateVisuals()
    {
        // Rounding to 10 for a clean "0-10" display scale
        musicText.text = "Music: " + Mathf.RoundToInt(musicSlider.value * 10f);
        sfxText.text = "SFX: " + Mathf.RoundToInt(sfxSlider.value * 10f);
    }
}
