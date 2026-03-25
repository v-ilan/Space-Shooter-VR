using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LocomotionSettingsSO", menuName = "Scriptable Objects/LocomotionSettingsSO")]
public class LocomotionSettingsSO : ScriptableObject
{
    public enum MoveType { Continuous, Teleport }
    public enum TurnType { Snap, Continuous }

    [Header("Movement")]
    public MoveType movementType = MoveType.Continuous;
    public float moveSpeed = 3.0f;

    [Header("Turning")]
    public TurnType turnType = TurnType.Snap;
    public float turnSpeed = 60.0f; // For Continuous
    public float snapAngle = 45.0f; // For Snap

    // This event notifies the XR Origin to refresh its providers instantly
    public event EventHandler OnSettingsChanged;

    public void NotifySettingsChanged()
    {
        OnSettingsChanged?.Invoke(this, EventArgs.Empty);
    }
}
