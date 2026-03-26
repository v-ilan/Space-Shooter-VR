using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using System.Linq;

public class NarrativeTrigger : MonoBehaviour
{
    [Header("Narrative Data")]
    [SerializeField] private NarrativeStepSO stepToTrigger;

    // This runs whenever you change something in the Inspector
    private void OnValidate()
    {
        bool isLinkedToEvent = CheckIfLinked();
        
        if (!isLinkedToEvent)
        {
            Debug.LogWarning($"[Narrative Warning] {gameObject.name} has a NarrativeTrigger but is NOT linked to any UnityEvent!", this);
        }
    }

    private bool CheckIfLinked()
    {
        // Get all components on this GameObject
        var components = GetComponents<Component>();

        foreach (var comp in components)
        {
            if (comp == null || comp == this) continue;

            // Use Reflection to find all Public and Private fields
            var fields = comp.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                // Is this field a UnityEvent?
                if (typeof(UnityEventBase).IsAssignableFrom(field.FieldType))
                {
                    UnityEventBase ev = field.GetValue(comp) as UnityEventBase;
                    if (ev == null) continue;

                    // Check if any persistent listener points to THIS script's TriggerStep method
                    for (int i = 0; i < ev.GetPersistentEventCount(); i++)
                    {
                        if (ev.GetPersistentTarget(i) == this && ev.GetPersistentMethodName(i) == nameof(TriggerStep))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void TriggerStep()
    {
        stepToTrigger.Activate();
    }
}
