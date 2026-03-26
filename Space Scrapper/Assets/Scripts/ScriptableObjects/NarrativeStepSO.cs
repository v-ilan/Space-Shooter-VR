using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NarrativeStepSO", menuName = "Scriptable Objects/NarrativeStepSO")]
public class NarrativeStepSO : ScriptableObject
{
    public string stepName;
    public double startTime; // Where in the Timeline this beat starts
    public bool isCompleted;

    public event EventHandler OnStepActivated;

    public void Activate()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            OnStepActivated?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ResetStep() => isCompleted = false;
}
