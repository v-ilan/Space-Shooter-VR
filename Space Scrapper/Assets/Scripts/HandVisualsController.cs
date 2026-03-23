using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandVisualsController : MonoBehaviour
{
    [SerializeField] private NearFarInteractor nearFarInteractor;
    [SerializeField] private Renderer handRenderer; 

    private void OnEnable()
    {
        nearFarInteractor.selectEntered.AddListener(HideHand);
        nearFarInteractor.selectExited.AddListener(ShowHand);
    }

    private void OnDisable()
    {
        nearFarInteractor.selectEntered.RemoveListener(HideHand);
        nearFarInteractor.selectExited.RemoveListener(ShowHand);
    }
    
    private void HideHand(SelectEnterEventArgs args)
    {
        handRenderer.enabled = false;
    }

    private void ShowHand(SelectExitEventArgs args)
    {
        handRenderer.enabled = true;
    }
}
