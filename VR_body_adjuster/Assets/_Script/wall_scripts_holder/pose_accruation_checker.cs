using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pose_accruation_checker : MonoBehaviour
{
    [SerializeField] private string leftLegTag, rightLegTag, leftArmTag, rightArmTag;

    [SerializeField] private GameObject leftLeg, rightLeg, leftArm, rightArm;

    [SerializeField] private Renderer leftLegRenderer, rightLegRenderer, leftArmRenderer, rightArmRenderer;

    [SerializeField] private Material red, green;

    public bool perfect_mach = false;

    private void Update()
    {
        // Cache the GameObjects based on their tags
        leftLeg = GameObject.FindGameObjectWithTag(leftLegTag);
        rightLeg = GameObject.FindGameObjectWithTag(rightLegTag);
        leftArm = GameObject.FindGameObjectWithTag(leftArmTag);
        rightArm = GameObject.FindGameObjectWithTag(rightArmTag);

        // Check if all GameObjects are found
        if (leftLeg != null) leftLegRenderer = leftLeg.GetComponent<Renderer>();
        if (rightLeg != null) rightLegRenderer = rightLeg.GetComponent<Renderer>();
        if (leftArm != null) leftArmRenderer = leftArm.GetComponent<Renderer>();
        if (rightArm != null) rightArmRenderer = rightArm.GetComponent<Renderer>();

        // Log errors for missing GameObjects
        if (leftLegRenderer == null) Debug.LogError("Left Leg Renderer not found!");
        if (rightLegRenderer == null) Debug.LogError("Right Leg Renderer not found!");
        if (leftArmRenderer == null) Debug.LogError("Left Arm Renderer not found!");
        if (rightArmRenderer == null) Debug.LogError("Right Arm Renderer not found!");
       

        if(leftLegRenderer == green && rightLegRenderer== green&& leftArmRenderer == green && rightArmRenderer == green )
        {
            perfect_mach = true;
        }
        else
        {
            perfect_mach = false;
        }
        
    }



}
