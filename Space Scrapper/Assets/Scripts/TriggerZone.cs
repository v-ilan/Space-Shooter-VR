using System;
using Unity.XR.CoreUtils;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public event EventHandler OnZoneTriggered; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out XROrigin xrOrigin))
        {
            OnZoneTriggered?.Invoke(this, EventArgs.Empty);
        }
    }
}
