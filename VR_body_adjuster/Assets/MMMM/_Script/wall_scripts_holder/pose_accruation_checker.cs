using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class pose_accruation_checker : MonoBehaviour
{
    [SerializeField] private wall_spawner wallSpawner;
    [SerializeField] private wall_movement wallmovement;
    [SerializeField] private List<body_position_macher> bodyParts;
    [SerializeField] private Transform cameraOffset;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeMagnitude = 0.1f;
    [SerializeField] private int resetTimerForChecking = 10;
    [SerializeField] private float destroyerDelay = 5f;

    public bool canShatter; // Ensure public for access
    public bool self_destroy; // Ensure public for access

    private Vector3 originalCameraOffsetPosition;
    public int matchedCount;
    public bool startChecking;
    public int wallSpawnCount = 1;

    private void Start()
    {
        wallmovement = GetComponent<wall_movement>();
        bodyParts = new List<body_position_macher>(FindObjectsOfType<body_position_macher>());


        if (cameraOffset == null)
        {
            cameraOffset = FindObjectOfType<XROrigin>()?.transform.Find("Camera Offset");
            if (cameraOffset == null)
            {
                Debug.LogError("Camera Offset not found. Please assign it manually.");
                return;
            }
        }

        originalCameraOffsetPosition = cameraOffset.localPosition;
        wallSpawner = GetComponent<wall_spawner>();
    }

    private void Update()
    {
        if (startChecking)
        {
            CheckMatchingStatus();
            UpdateDestroyTimer();
        }

        if (self_destroy && destroyerDelay <= 0)
        {
            ResetCheckerState();
        }
    }

    private void UpdateDestroyTimer()
    {
        destroyerDelay -= Time.deltaTime;
        if (destroyerDelay <= 0)
        {
            self_destroy = true;
        }
    }

    private void CheckMatchingStatus()
    {
        matchedCount = 0;
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

        if (!allMatched)
        {
            StartCoroutine(CameraShake());
            canShatter = true;
        }

        Debug.Log($"Matched body parts: {matchedCount}/{bodyParts.Count}");
    }

    private void ResetCheckerState()
    {
        self_destroy = false;
        destroyerDelay = 5f;
        startChecking = false;
        canShatter = false;
        wallmovement.isMoving = false;

        foreach (var part in bodyParts)
        {
            part.mached = false;
        }

        matchedCount = 0;
        wallSpawnCount++;

        switch (wallSpawnCount)
        {
            case 2:
                wallSpawner.TriggerNextPose(wall_spawner.PoseType.Second);
                bodyParts = new List<body_position_macher>(FindObjectsOfType<body_position_macher>());

                break;
            case 3:
                wallSpawner.TriggerNextPose(wall_spawner.PoseType.Third);
                bodyParts = new List<body_position_macher>(FindObjectsOfType<body_position_macher>());
                break;
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
            yield return null;
        }

        cameraOffset.localPosition = originalCameraOffsetPosition;
    }
}

