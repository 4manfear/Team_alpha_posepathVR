using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_spawner : MonoBehaviour
{
    [SerializeField] private GameObject pose1, pose2, pose3;

    public bool frstPose, secondpose, thirdpose;

    [SerializeField] private wall_movement wallmovement;
    [SerializeField] float reset_wall_start_movening_timer;

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
            reseting();
        }
        
        if (secondpose)
        {
            pose2.SetActive(true);
            Destroy(pose1);
            callthefunction = true;
        
        }
        if(thirdpose)
        {
            pose3.SetActive(true);
            Destroy (pose2);
            callthefunction = true;
           
        }
    }

    void reseting()
    {
        callthefunction = false;
        wallmovement.countdownTime = reset_wall_start_movening_timer;
        wallmovement.isMoving = false;
       
    }



}
