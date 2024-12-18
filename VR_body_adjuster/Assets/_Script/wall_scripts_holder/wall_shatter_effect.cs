using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_shatter_effect : MonoBehaviour
{
    public bool canshatter;

    [SerializeField] private GameObject shattered_wall, the_orignal;
    public pose_accruation_checker pac;

    private void Update()
    {
        if (pac.canShatter == true)
        {
            canshatter = true;
        }


        if(canshatter)
        {
            shattered_wall.transform.parent = null;
            the_orignal.gameObject.SetActive(false);
            shattered_wall.SetActive(true);
        }
    }


}
