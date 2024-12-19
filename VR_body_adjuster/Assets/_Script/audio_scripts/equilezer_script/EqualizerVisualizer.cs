using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EqualizerVisualizer : MonoBehaviour
{
    public AudioSource audioSource; // Reference to your audio source
    public List<Image> bars; // List of UI bars (manually assigned in the Inspector)
    public float sensitivity = 100f; // Sensitivity of the bar movement
    public float lerpSpeed = 10f; // Speed of the bar smoothing
    public float decaySpeed = 2f; // Speed at which bars shrink when spectrum data decreases
    public Gradient colorGradient; // Gradient for color transitions based on height

    private float[] spectrumData; // Array for spectrum data
    private int spectrumSize = 512; // Must be a power of two (e.g., 64, 128, 256)
    private float[] barHeights; // Track the current height of each bar

    void Start()
    {
        // Initialize the spectrum data array
        spectrumData = new float[spectrumSize];

        // Initialize the bar heights array
        barHeights = new float[bars.Count];
    }

    void Update()
    {
        // Get the spectrum data from the audio source
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Blackman);

        // Map spectrum data to the bars
        for (int i = 0; i < bars.Count; i++)
        {
            // Determine the range of spectrum data to average for this bar
            int startIndex = Mathf.FloorToInt((float)i / bars.Count * spectrumSize);
            int endIndex = Mathf.FloorToInt((float)(i + 1) / bars.Count * spectrumSize);

            float average = 0f;
            for (int j = startIndex; j < endIndex; j++)
            {
                if (j < spectrumData.Length)
                {
                    average += spectrumData[j];
                }
            }

            average /= (endIndex - startIndex);

            // Calculate the target height based on the averaged spectrum data
            float targetHeight = average * sensitivity;

            // Smoothly scale the bar using Lerp
            barHeights[i] = Mathf.Lerp(barHeights[i], targetHeight, lerpSpeed * Time.deltaTime);

            // Apply a decay effect when the target height decreases
            if (barHeights[i] > targetHeight)
            {
                barHeights[i] -= decaySpeed * Time.deltaTime;
                if (barHeights[i] < targetHeight)
                {
                    barHeights[i] = targetHeight;
                }
            }

            // Update the bar's scale
            Vector3 currentScale = bars[i].rectTransform.localScale;
            bars[i].rectTransform.localScale = new Vector3(currentScale.x, Mathf.Clamp(barHeights[i], 0.1f, 10f), currentScale.z);

            // Update the bar's color based on its height
            float normalizedHeight = Mathf.InverseLerp(0.1f, 10f, barHeights[i]);
            bars[i].color = colorGradient.Evaluate(normalizedHeight);
        }
    }
}
