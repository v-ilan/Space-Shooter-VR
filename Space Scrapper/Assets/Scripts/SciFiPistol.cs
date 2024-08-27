using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SciFiPistol : MonoBehaviour
{

    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform shootSource;
    private float shootingDistance = 1.5f;
    private bool isShooting = false;
    
    // Start is called before the first frame update
    private void Start()
    {
        XRGrabInteractable xrGrabInteractable = GetComponent<XRGrabInteractable>();
        xrGrabInteractable.activated.AddListener(OnActivated_StartShooting);
        xrGrabInteractable.deactivated.AddListener(OnDeactivated_StopShooting);
    }

    //
    private void Update()
    {
        if(isShooting)
        {
            RaycastCheck();
        }
    }

    //
    private void OnActivated_StartShooting(ActivateEventArgs args)
    {
        isShooting = true;
    }
    
    //
    private void OnDeactivated_StopShooting(DeactivateEventArgs args)
    {
        isShooting = false;
    }

    private void RaycastCheck()
    {
        bool hasHit = Physics.Raycast(shootSource.position, shootSource.forward, out RaycastHit hit, shootingDistance, layerMask);
        if(hasHit)
        {   //need to be fixed to use event and not "SendMessege"
            hit.transform.gameObject.SendMessage("BreakPieces", SendMessageOptions.DontRequireReceiver);
        }
    }
}
