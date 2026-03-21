using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Climbing;

public class PlaySteps : MonoBehaviour
{
    [SerializeField] private Transform trashCan;
    [SerializeField] private Transform breakableRock;
    [SerializeField] private Transform analizer;
    [SerializeField] private Transform ladderHandles;
    [SerializeField] private Transform vortexTriggerZone;

    [Serializable]
    public class Step
    {
        public string name;
        public float time;
        public bool hasPlayed = false;
    }

    public List<Step> steps;

    private PlayableDirector playableDirector;
    


    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        trashCan.GetComponent<TrashCan>().OnObjectTrashed += TrashCan_OnObjectTrashed;
        breakableRock.GetComponent<BreakableRock>().OnObjectBroke += BreakableRock_OnObjectBroke;
        analizer.GetComponent<EnergySocketInteractor>().selectEntered.AddListener(EnergySocketInteractor_selectEntered);
        ladderHandles.GetComponent<ClimbInteractable>().selectEntered.AddListener(ClimbInteractable_selectEntered);
        vortexTriggerZone.GetComponent<TriggerZone>().OnZoneTriggered += TriggerZone_OnZoneTriggered;
    }

    private void TrashCan_OnObjectTrashed(object sender, EventArgs e) => PlaystepIndex(0);
    private void BreakableRock_OnObjectBroke(object sender, EventArgs e) => PlaystepIndex(1);
    private void EnergySocketInteractor_selectEntered(SelectEnterEventArgs arg0) => PlaystepIndex(2);
    private void ClimbInteractable_selectEntered(SelectEnterEventArgs arg0) => PlaystepIndex(3);
    private void TriggerZone_OnZoneTriggered(object sender, EventArgs e) => PlaystepIndex(4);

    private void PlaystepIndex(int index)
    {
        Step step = steps[index];

        if(!step.hasPlayed)
        {
            step.hasPlayed = true;

            playableDirector.Stop();
            playableDirector.time = step.time;
            playableDirector.Play();
        }
    }
}
