using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collided_with_the_wall : MonoBehaviour
{
    public  pose_accruation_checker pac;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("wall"))
        {
            pac.start_checking = true;
            Debug.Log("touch_huaaa");
        }
    }
}