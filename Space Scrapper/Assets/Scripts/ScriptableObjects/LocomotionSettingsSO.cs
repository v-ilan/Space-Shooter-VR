using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LocomotionSettingsSO", menuName = "Scriptable Objects/LocomotionSettingsSO")]
public class LocomotionSettingsSO : ScriptableObject
{
    public enum TurnType { Snap, Continuous }

    [Header("Movement")]
    public float moveSpeed = 2.5f;

    [Header("Turning")]
    public TurnType turnType = TurnType.Snap;
    public float turnSpeed = 60.0f; // Degrees per second for Continuous
    public float snapAngle = 45.0f; // Degrees per "click" for Snap

    public event EventHandler OnSettingsChanged;

    public void NotifySettingsChanged()
    {
        OnSettingsChanged?.Invoke(this, EventArgs.Empty);
    }
}
