using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class SceneTransitionUI : MonoBehaviour
{
    public static SceneTransitionUI Instance { get; private set; }

    [SerializeField] private CanvasGroup faderCanvasGroup;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private void Awake()
    {
        if (Instance != null) { 
            Destroy(gameObject); 
            return; 
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Keeps fader alive during scene jumps

        if (faderCanvasGroup == null)
        {
            faderCanvasGroup = GetComponent<CanvasGroup>();
        }
        
        // Start blacked out if you want to fade IN to the main menu
        faderCanvasGroup.alpha = 1f;
    }

    private void Start()
    {
        FadeIn(); // Initial fade in when the game actually launches
    }

    public void FadeIn(Action onComplete = null) => StartCoroutine(FadeRoutine(1, 0, onComplete));
    public void FadeOut(Action onComplete = null) => StartCoroutine(FadeRoutine(0, 1, onComplete));

    private IEnumerator FadeRoutine(float startAlpha, float targetAlpha, Action onComplete)
    {
        float timer = 0;
        faderCanvasGroup.blocksRaycasts = true; // Prevent clicking buttons while fading

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            faderCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, fadeCurve.Evaluate(t));
            yield return null;
        }

        faderCanvasGroup.alpha = targetAlpha;
        faderCanvasGroup.blocksRaycasts = (targetAlpha == 1); // Only block if we are fully blacked out
        
        onComplete?.Invoke();
    }
}
