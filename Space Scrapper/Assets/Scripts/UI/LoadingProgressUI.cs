using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LoadingProgressUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI loadingText;

    [SerializeField] private Slider progressBar;
    [SerializeField] private Image fillImage;
    //[SerializeField] private Image spinnerIcon;

    [Header("Settings")]
    [SerializeField] private float dotInterval = 1f; // Speed of the dots

    private string _baseText = "Loading";
    private float _dotTimer = 0;
    private int _dotCount = 0;
    private int _dotMax = 3;
    //[SerializeField] private float rotationSpeed = 200f;

    private void Update()
    {
        float progress = SceneLoader.GetLoadingProgress();
        // Update the bar
        UpdateProgressBar(progress);

        /* Rotate the spinner (so they know the app is "Alive")
            spinnerIcon.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        */

        UpdateLoadingDots();
    }

    private void UpdateProgressBar(float progress)
    {
        progressBar.value = progress;
        fillImage.fillAmount = progress;
    }

    private void UpdateLoadingDots()
    {
        _dotTimer += Time.deltaTime;
        
        if(_dotTimer >= dotInterval)
        {
            _dotCount = (_dotCount + 1) % _dotMax + 1;
            loadingText.text = _baseText + string.Concat(Enumerable.Repeat(".", _dotCount));
            _dotTimer = 0;
        }

    }
}
