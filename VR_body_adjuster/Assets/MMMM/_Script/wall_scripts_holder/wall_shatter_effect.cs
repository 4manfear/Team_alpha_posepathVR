using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_shatter_effect : MonoBehaviour
{
    public bool canShatterwall;
    [SerializeField] private GameObject shatteredWall, originalWall;
    public pose_accruation_checker poseChecker;

    private void Start()
    {
        shatteredWall.SetActive(false);


        poseChecker = FindObjectOfType<pose_accruation_checker>();
    }

    private void Update()
    {
        


        if (poseChecker == null) return;

        canShatterwall = poseChecker.canShatter;

        if (poseChecker.self_destroy)
        {
            Destroy(shatteredWall);
            Destroy(originalWall);
        }

        if (canShatterwall)
        {
            shatteredWall.transform.parent = null;
            originalWall.SetActive(false);
            shatteredWall.SetActive(true);
            Destroy(originalWall);
        }
    }
}
