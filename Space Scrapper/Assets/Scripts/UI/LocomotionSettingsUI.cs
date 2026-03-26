using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Content.Interaction;

public class LocomotionSettingsUI : MonoBehaviour
{
    [Header("Data Architecture")]
    [SerializeField] private LocomotionSettingsSO locomotionSettingsSO;

    [Header("UI Movement Elements")]
    [SerializeField] private Slider moveSpeedSlider;
    [SerializeField] private TextMeshProUGUI moveSpeedLabel;
    private readonly float moveSpeedSliderRatio = 0.1f;

    [Header("UI Turning Elements")]
    [SerializeField] private Slider snapTurnSlider; // 1 = Snap, 0 = Continuous
    [SerializeField] private GameObject snapAngleContainer; // Group with Snap Slider
    [SerializeField] private GameObject turnSpeedContainer; // Group with Continuous Slider
    
    [SerializeField] private TextMeshProUGUI snapAngleLabel;
    [SerializeField] private Slider snapAngleSlider;
    private readonly int snapAngleSliderRatio = 15;

    [SerializeField] private TextMeshProUGUI turnSpeedLabel;
    [SerializeField] private Slider turnSpeedSlider;
    private readonly int turnSpeedSliderRatio = 5;

    private void OnEnable()
    {
        RefreshUIFromSO();
        SetupListeners();
    }

    private void SetupListeners()
    {
        // Movement
        moveSpeedSlider.onValueChanged.AddListener(val => {
            locomotionSettingsSO.moveSpeed = val * moveSpeedSliderRatio;
            moveSpeedLabel.text = $"Move Speed: {val * moveSpeedSliderRatio:F1} m/s";
            ApplySettings();
        });

        // Turning
        snapTurnSlider.onValueChanged.AddListener(val => {
            locomotionSettingsSO.turnType = val == 1 ? LocomotionSettingsSO.TurnType.Snap : LocomotionSettingsSO.TurnType.Continuous;
            UpdateTurnUIContainers(val == 1);
            ApplySettings();
        });

        snapAngleSlider.onValueChanged.AddListener(val => {
            float angle = val * snapAngleSliderRatio;
            locomotionSettingsSO.snapAngle = angle;
            snapAngleLabel.text = $"Snap: {angle}°";
            ApplySettings();
        });

        turnSpeedSlider.onValueChanged.AddListener(val => {
            float speed = val * turnSpeedSliderRatio;
            locomotionSettingsSO.turnSpeed = speed;
            turnSpeedLabel.text = $"Speed: {speed}°/s";
            ApplySettings();
        });
    }

    private void RefreshUIFromSO()
    {
        // Movement
        moveSpeedSlider.value = locomotionSettingsSO.moveSpeed / moveSpeedSliderRatio;
        moveSpeedLabel.text = $"Move Speed: {locomotionSettingsSO.moveSpeed:F1} m/s";

        // Turning
        snapTurnSlider.value = locomotionSettingsSO.turnType == LocomotionSettingsSO.TurnType.Snap ? 1 : 0;
        
        // Snap
        snapAngleSlider.value = locomotionSettingsSO.snapAngle / 15f; // Reverse the math
        snapAngleLabel.text = $"Snap: {locomotionSettingsSO.snapAngle}°";

        // Continuous
        turnSpeedSlider.value = locomotionSettingsSO.turnSpeed / 5f; // Reverse the math
        turnSpeedLabel.text = $"Speed: {locomotionSettingsSO.turnSpeed}°/s";

        UpdateTurnUIContainers(snapTurnSlider.value == 1);
        ApplySettings();
    }

    private void UpdateTurnUIContainers(bool isSnap)
    {
        snapAngleContainer.SetActive(isSnap);
        turnSpeedContainer.SetActive(!isSnap);
    }

    private void ApplySettings()
    {
        locomotionSettingsSO.NotifySettingsChanged();
    }
}
