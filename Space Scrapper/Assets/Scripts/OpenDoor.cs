using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttonsList;
    [SerializeField] private Animator animator;

    private const string BOOL_NAME = "isOpen";

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject button in buttonsList)
        {
            GetComponent<XRSimpleInteractable>().selectEntered.AddListener(ToggleDoorOpen);
        }
    }

    private void ToggleDoorOpen(SelectEnterEventArgs arg0)
    {
        bool isOpen = animator.GetBool(BOOL_NAME);
        animator.SetBool(BOOL_NAME, !isOpen);
    }
}
