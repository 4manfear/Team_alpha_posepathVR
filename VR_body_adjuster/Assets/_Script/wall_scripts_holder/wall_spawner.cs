using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_spawner : MonoBehaviour
{
    [SerializeField] private GameObject pose1, pose2, pose3;

    public bool frstPose, secondpose, thirdpose;

    [SerializeField] private wall_movement wallmovement;
    [SerializeField] float reset_wall_start_movening_timer;

    public float timer_starts;

    public bool callthefunction;

    private void Start()
    {
        wallmovement = GetComponent<wall_movement>();
        frstPose = true;
    }

    private void Update()
    {
        if (callthefunction)
        {
            StartCoroutine(ResetWallMovementCoroutine());
        }

        if (secondpose)
        {
            pose2.SetActive(true);
            Destroy(pose1);
            StartCoroutine(function_calling());
            secondpose = false;
        }
        if (thirdpose)
        {
            pose3.SetActive(true);
            Destroy(pose2);
            StartCoroutine(function_calling());
        }
    }

    IEnumerator function_calling()
    {
        yield return new WaitForSeconds(timer_starts);
        callthefunction = true;

        yield return new WaitForSeconds(3);
        callthefunction = false;
    }

    IEnumerator ResetWallMovementCoroutine()
    {
        reseting(); // Call the reseting logic
        yield return null; // Wait for the current frame to finish

        // Wait for the reset_wall_start_movening_timer duration
        yield return new WaitForSeconds(reset_wall_start_movening_timer);
        callthefunction = false; // Set callthefunction to false after reset logic is completed
    }

    void reseting()
    {
        wallmovement.countdownTime = reset_wall_start_movening_timer;
        wallmovement.isMoving = false;
    }

}
