using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class pose_accruation_checker : MonoBehaviour
{
    [SerializeField] private List<body_position_macher> bodyParts; // List of body part scripts
    public bool canShatter = false; // Determines if shattering can occur

    public bool start_checking;

    public int  matchedCount;
    public bool allareMatched;

    [SerializeField] private Transform cameraOffset; // Reference to the XR Origin's Camera Offset
    [SerializeField] private float shakeDuration = 0.5f; // Duration of the shake
    [SerializeField] private float shakeMagnitude = 0.1f; // Magnitude of the shake

    private Vector3 originalCameraOffsetPosition; // To store the initial offset position


    private void Start()
    {
        if (cameraOffset == null)
        {
            // Automatically find the Camera Offset under XR Origin
            cameraOffset = FindObjectOfType<XROrigin>().transform.Find("Camera Offset");
            if (cameraOffset == null)
            {
                Debug.LogError("Camera Offset not found in XR Origin. Please assign it manually.");
                return;
            }
        }

        originalCameraOffsetPosition = cameraOffset.localPosition;

    }

    private void Update()
    {
        if (bodyParts == null || bodyParts.Count == 0)
        {
            bodyParts = new List<body_position_macher>(FindObjectsOfType<body_position_macher>());
        }
        if (bodyParts.Count == 0)
        {
            Debug.LogError("No body_position_macher components found in the scene.");
        }

        if (start_checking)
        {
            CheckMatchingStatus();
        }
       
    }

    private void CheckMatchingStatus()
    {
        // Reset the matched count before counting
        matchedCount = 0;

        // Check if all body parts are matched
        bool allMatched = true;
        foreach (var part in bodyParts)
        {
            if (part.mached)
            {
                matchedCount++;
            }
            else
            {
                allMatched = false;
            }
        }

        // Check if all body parts are matched
        allareMatched = matchedCount == bodyParts.Count;

        // Set the canShatter state
        if (!canShatter && !allMatched) // Trigger only when `canShatter` transitions to true
        {
            canShatter = true;
            StartCoroutine(CameraShake());
        }
        else if (allMatched)
        {
            canShatter = false;
        }

        // Log the results
        Debug.Log($"Matched body parts: {matchedCount}/{bodyParts.Count}");
        if (canShatter)
        {
            Debug.Log("Can shatter! Not all body parts are matched.");
        }
        else
        {
            Debug.Log("All body parts matched. Cannot shatter.");
        }



    }

    private IEnumerator CameraShake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            cameraOffset.localPosition = originalCameraOffsetPosition + randomOffset;

            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Reset camera position
        cameraOffset.localPosition = originalCameraOffsetPosition;
    }
}

