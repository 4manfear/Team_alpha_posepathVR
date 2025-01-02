using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class button_to_reset : MonoBehaviour
{
    [SerializeField] reset_the_rotation_of_the_mannequine mannequinReset;
    [SerializeField] InputActionReference button_right;

   

    private void Start()
    {
        button_right.action.performed += ctx => vr_button_pressed();
    }


    void vr_button_pressed()
    {
        Debug.Log("this button pressed");
        mannequinReset.ResetMannequin();
    }
}
