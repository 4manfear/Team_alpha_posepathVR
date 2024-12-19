using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // Speed of rotation in degrees per second

    void Update()
    {
        // Calculate the new rotation based on time
        float rotation = rotationSpeed * Time.deltaTime;

        // Rotate the skybox around the Y-axis (you can change this if needed)
        RenderSettings.skybox.SetFloat("_Rotation", RenderSettings.skybox.GetFloat("_Rotation") + rotation);
    }
}