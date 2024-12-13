using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset_the_rotation_of_the_mannequine : MonoBehaviour
{
    [SerializeField] Transform[] limbs; // Array of mannequin limbs to reset
    private Quaternion[] originalRotations; // Store original rotations

    private void Start()
    {
        // Save the original rotations of all limbs
        originalRotations = new Quaternion[limbs.Length];
        for (int i = 0; i < limbs.Length; i++)
        {
            originalRotations[i] = limbs[i].rotation;
        }
    }

    // Method to reset the rotations of all limbs
    public void ResetMannequin()
    {
        for (int i = 0; i < limbs.Length; i++)
        {
            limbs[i].rotation = originalRotations[i];
        }
        Debug.Log("Mannequin reset to original rotation.");
    }
}
