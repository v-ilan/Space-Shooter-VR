using UnityEngine;

public class NarativeOver : MonoBehaviour
{
    [Header("Transition Settings")]
    [SerializeField] private SceneLoader.Scene targetScene = SceneLoader.Scene.StartMenuScene;
    
    [Header("Debug")]
    [SerializeField] private bool logTransition = true;

    /// <summary>
    /// Triggered by the SIG_END Signal Emitter in the Timeline.
    /// </summary>
    public void OnNarrativeSequenceComplete()
    {
        if (logTransition)
        {
            Debug.Log($"[NarrativeOver]Signal Received. Transitioning to {targetScene}...");
        }

        // Check if the Fader exists before calling it to prevent NullRefs during testing
        if (SceneTransitionUI.Instance != null)
        {
            SceneTransitionUI.Instance.FadeOut(() => 
            {
                SceneLoader.Load(targetScene);
            });
        }
        else
        {
            // Safety: If for some reason the fader is missing, load the scene anyway
            Debug.LogWarning("[NarrativeOver] SceneTransitionUI Instance missing! Loading scene immediately.");
            SceneLoader.Load(targetScene);
        }
    }
}
