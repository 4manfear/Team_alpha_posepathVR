using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voice_active_script : MonoBehaviour
{
    [SerializeField] private GameObject next_wall_text;
    [SerializeField] private AudioSource next_wall_audio;

    [SerializeField] private pose_accruation_checker pac;



    private void Start()
    {
        pac = GameObject.FindObjectOfType<pose_accruation_checker>();
    }

    private void FixedUpdate()
    {
        if (pac.self_destroy == true)
        {


            Debug.Log("bool ne lagi");
            next_wall_text.SetActive(true);
            next_wall_audio.Play();
           

        }
        else if (pac.self_destroy == false)
        {

            next_wall_text.SetActive(false);


        }
    }
}
