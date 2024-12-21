using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_spawner : MonoBehaviour
{
    public enum PoseType { First, Second, Third }

    [SerializeField] private GameObject pose1;
    [SerializeField] private GameObject pose2;
    [SerializeField] private GameObject pose3;
    [SerializeField] private wall_movement wallMovement;
    [SerializeField] private float resetWallStartTimer ;
    [SerializeField] private float functionCallDelay ;

    private void Start()
    {
        wallMovement = GetComponent<wall_movement>();
        TriggerNextPose(PoseType.First);
    }

    public void TriggerNextPose(PoseType pose)
    {
        switch (pose)
        {
            case PoseType.First:
                pose1.SetActive(true);
                break;
            case PoseType.Second:
                pose2.SetActive(true);
                Destroy(pose1);
                StartCoroutine(DelayedWallReset());
                break;
            case PoseType.Third:
                pose3.SetActive(true);
                Destroy(pose2);
                StartCoroutine(DelayedWallReset());
                break;
        }
    }

    private IEnumerator DelayedWallReset()
    {
        yield return new WaitForSeconds(functionCallDelay);
        wallMovement.ResetWallMovement(resetWallStartTimer);
    }
}
