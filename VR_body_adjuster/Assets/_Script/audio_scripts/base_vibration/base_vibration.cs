using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class base_vibration : MonoBehaviour
{
    // Audio source to analyze
    [SerializeField] private AudioSource audioSource;

    // FFT sample size (must be a power of 2, typically 256 or 512)
    public const int sampleSize = 512;

    // Array to hold spectrum data
    public float[] spectrumData;

    // Rigidbody for applying vibration (optional)
    public Rigidbody rb;

    // Vibration strength and frequency settings
    [SerializeField] private float vibrationStrength = 0.5f; // Strength of the vibration
    [SerializeField] private float bassFrequencyRange = 100f; // Max frequency for bass

    private void Start()
    {
        // Initialize the spectrum data array
        spectrumData = new float[sampleSize];

        // Ensure the audio source is assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource is not assigned or attached to this GameObject.");
            }
        }

        // Get the Rigidbody component for vibration (optional)
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the audio source is valid and playing
        if (audioSource == null || !audioSource.isPlaying) return;

        // Get the spectrum data
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        // Calculate bass strength (sum of lower frequencies)
        float bassStrength = 0f;
        for (int i = 0; i < spectrumData.Length; i++)
        {
            float frequency = i * (AudioSettings.outputSampleRate / 2) / sampleSize;

            // Check if the frequency is in the bass range
            if (frequency <= bassFrequencyRange)
            {
                bassStrength += spectrumData[i];
            }
        }

        // Apply vibration effect if Rigidbody is available
        if (rb != null)
        {
            // Adjust vibration force based on bass strength
            Vector3 vibrationForce = new Vector3(
                Random.Range(-1f, 1f) * bassStrength * vibrationStrength,
                Random.Range(-1f, 1f) * bassStrength * vibrationStrength,
                Random.Range(-1f, 1f) * bassStrength * vibrationStrength
            );

            rb.AddForce(vibrationForce);
        }

        // Optionally, visualize the bass frequencies in the spectrum (for debugging)
        float scaleFactor = 50f;
        for (int i = 1; i < spectrumData.Length - 1; i++)
        {
            float x1 = i - 1;
            float x2 = i;

            float y1 = spectrumData[i - 1] * scaleFactor;
            float y2 = spectrumData[i] * scaleFactor;

            // Logarithmic scaling
            float logY1 = Mathf.Log(spectrumData[i - 1] + 1) * scaleFactor;
            float logY2 = Mathf.Log(spectrumData[i] + 1) * scaleFactor;

            Debug.DrawLine(new Vector3(x1, y1 + 10, 0), new Vector3(x2, y2 + 10, 0), Color.red); // Linear spectrum
            Debug.DrawLine(new Vector3(x1, logY1 + 10, 2), new Vector3(x2, logY2 + 10, 2), Color.cyan); // Log spectrum
        }
    }
}
