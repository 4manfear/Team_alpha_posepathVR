using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_shatter_effect : MonoBehaviour
{
    public bool canshatter;
  

    [SerializeField] private GameObject shattered_wall, the_orignal;
    public pose_accruation_checker pac;

    private void Start()
    {
        shattered_wall.SetActive(false);
    }

    private void Update()
    {

        pac = GameObject.FindObjectOfType<pose_accruation_checker>();

        if (pac.canShatter == true)
        {
            canshatter = true;
        }

        if (pac.self_destroy == true)
        {
            Destroy(shattered_wall);
            Destroy(the_orignal);
           
        }


        if (canshatter)
        {
            shattered_wall.transform.parent = null;
            the_orignal.gameObject.SetActive(false);
            shattered_wall.SetActive(true);
            Destroy(the_orignal);
        }
    }


}
