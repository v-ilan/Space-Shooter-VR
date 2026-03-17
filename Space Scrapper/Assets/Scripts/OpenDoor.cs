using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private GameObject openButton;
    [SerializeField] private Animator animator;

    private const string BOOL_NAME = "character_nearby";

    // Start is called before the first frame update
    void Start()
    {
        openButton.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(ToggleDoorOpen);
    }

    private void ToggleDoorOpen(SelectEnterEventArgs arg0)
    {
        bool isOpen = animator.GetBool(BOOL_NAME);
        animator.SetBool(BOOL_NAME, !isOpen);
    }
}
