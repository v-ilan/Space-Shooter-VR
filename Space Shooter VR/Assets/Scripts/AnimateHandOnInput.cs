using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty pinchAnimationAction;
    [SerializeField] private InputActionProperty gripAnimationAction;
    [SerializeField] private Animator handAnimator;
    
    // 
    void Awake()
    {
        handAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateHands();
    }

    private void UpdateHands()
    {
        handAnimator.SetFloat("Pinch", pinchAnimationAction.action.ReadValue<float>());
        handAnimator.SetFloat("Flex", gripAnimationAction.action.ReadValue<float>());
    }
}
