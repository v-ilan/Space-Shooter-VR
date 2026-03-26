using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Content.Interaction;

public class LocomotionSettings : MonoBehaviour
{
    [Header("Data Architecture")]
    [SerializeField] private LocomotionSettingsSO locomotionSettingsSO;
    [SerializeField] private LocomotionManager locomotionManager;

    private void OnEnable() 
    {
        locomotionSettingsSO.OnSettingsChanged += ApplySettings;
        ApplySettings(); // Initial sync
    }

    private void OnDisable() => locomotionSettingsSO.OnSettingsChanged -= ApplySettings;

    private void ApplySettings(object sender, EventArgs e)
    {
        ApplySettings();
    }

    private void ApplySettings()
    {
        if (!locomotionManager) return;

        // Turn Logic
        var style = (locomotionSettingsSO.turnType == LocomotionSettingsSO.TurnType.Snap) 
            ? LocomotionManager.TurnStyle.Snap : LocomotionManager.TurnStyle.Smooth;
        
        locomotionManager.leftHandTurnStyle = style;
        locomotionManager.rightHandTurnStyle = style;

        // Provider Logic
        if (locomotionManager.dynamicMoveProvider) 
        {
            locomotionManager.dynamicMoveProvider.moveSpeed = locomotionSettingsSO.moveSpeed;
        }
        if (locomotionManager.snapTurnProvider) 
        {
            locomotionManager.snapTurnProvider.turnAmount = locomotionSettingsSO.snapAngle;
        }
        if (locomotionManager.smoothTurnProvider)
        {
            locomotionManager.smoothTurnProvider.turnSpeed = locomotionSettingsSO.turnSpeed;
        }
        
        Debug.Log("Locomotion Hardware Synchronized.");
    }

    public LocomotionSettingsSO GetLocomotionSettings() => locomotionSettingsSO;
}
