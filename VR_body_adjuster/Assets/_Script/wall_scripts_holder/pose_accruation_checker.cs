using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class pose_accruation_checker : MonoBehaviour
{
    [SerializeField] private wall_spawner wallspawner;
    [SerializeField] private int wall_spawn_count = 1;

    [SerializeField] private wall_movement wallmovement;
    [SerializeField] float reset_wall_start_movening_timer;

    [SerializeField] private List<body_position_macher> bodyParts; // List of body part scripts
    public bool canShatter ; // Determines if shattering can occur

    public bool start_checking;

    public int  matchedCount;
    public bool allareMatched;

    [SerializeField] private Transform cameraOffset; // Reference to the XR Origin's Camera Offset
    [SerializeField] private float shakeDuration = 0.5f; // Duration of the shake
    [SerializeField] private float shakeMagnitude = 0.1f; // Magnitude of the shake

    private Vector3 originalCameraOffsetPosition; // To store the initial offset position

    public int reset_timer_for_checking;
    public float destroyer_delay;
    public bool self_destroy;


    private void Start()
    {
        wallspawner = GetComponent<wall_spawner>();


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
            time_to_destroy();
            StartCoroutine(falsethestartchecking());

        }
       
        if(destroyer_delay>0)
        {
            self_destroy = false;
        }

    }


  


    void time_to_destroy()
    {
        destroyer_delay -= Time.deltaTime;
        if(destroyer_delay <= 0)
        {
            self_destroy = true;
        }

        
    }


    // reseting all the things
    IEnumerator falsethestartchecking()
    {
        yield return new WaitForSeconds(reset_timer_for_checking);
        
        self_destroy =false;
        if(start_checking)
        {
            wall_spawn_count++;
        }

        

        if(wall_spawn_count == 2)
        {
            
            canShatter = false;
            destroyer_delay = 5;
            reset_timer_for_checking = 10;
            wallspawner.secondpose = true;
            wallspawner.frstPose = false;
        }
        if(wall_spawn_count==3)
        {
           
            canShatter = false;
            destroyer_delay = 5;
            reset_timer_for_checking = 10;
            wallspawner.thirdpose = true;
            wallspawner.secondpose = false;
        }

        foreach (var part in bodyParts)
        {
            if(part.mached==true)
            {
                part.mached = false;
            }
        }
  
        matchedCount =0;
        start_checking = false;
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

