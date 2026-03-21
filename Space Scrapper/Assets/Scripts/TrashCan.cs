using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class TrashCan : MonoBehaviour
{
    public event EventHandler OnObjectTrashed;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<XRGrabInteractable>() != null)
        {
            other.gameObject.SetActive(false);
            OnObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
