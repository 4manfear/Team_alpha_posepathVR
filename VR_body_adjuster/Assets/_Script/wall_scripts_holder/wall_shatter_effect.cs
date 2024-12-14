using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_shatter_effect : MonoBehaviour
{
    public bool canshatter;

    [SerializeField] private GameObject shattered_wall;
    public pose_accruation_checker pac;

    private void Update()
    {
        if(canshatter)
        {
            this.gameObject.SetActive(false);
            shattered_wall.SetActive(true);
        }
    }


}
