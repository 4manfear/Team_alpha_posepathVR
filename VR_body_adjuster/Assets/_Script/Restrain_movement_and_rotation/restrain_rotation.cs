using UnityEngine;
using System.Collections;

public class restrain_rotation : MonoBehaviour
{
    [Header("Target Object")]
    public Transform targetObject; // Object whose rotation is being restricted

    [Header("Rotation Limits")]
    public bool restrainX; // Toggle to restrain rotation on the X-axis
    public float minXRotation; // Minimum X rotation limit in degrees
    public float maxXRotation; // Maximum X rotation limit in degrees

    public bool restrainY; // Toggle to restrain rotation on the Y-axis
    public float minYRotation; // Minimum Y rotation limit in degrees
    public float maxYRotation; // Maximum Y rotation limit in degrees

    public bool restrainZ; // Toggle to restrain rotation on the Z-axis
    public float minZRotation; // Minimum Z rotation limit in degrees
    public float maxZRotation; // Maximum Z rotation limit in degrees

    void Update()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target Object is not assigned. Please assign a target object in the Inspector.");
            return;
        }

        // Get the current rotation in Euler angles
        Vector3 currentRotation = targetObject.localEulerAngles;

        // Normalize rotations to be within -180 to 180 degrees
        float normalizedX = currentRotation.x > 180 ? currentRotation.x - 360 : currentRotation.x;
        float normalizedY = currentRotation.y > 180 ? currentRotation.y - 360 : currentRotation.y;
        float normalizedZ = currentRotation.z > 180 ? currentRotation.z - 360 : currentRotation.z;

        // Clamp the rotation for each axis if enabled
        if (restrainX)
            normalizedX = Mathf.Clamp(normalizedX, minXRotation, maxXRotation);

        if (restrainY)
            normalizedY = Mathf.Clamp(normalizedY, minYRotation, maxYRotation);

        if (restrainZ)
            normalizedZ = Mathf.Clamp(normalizedZ, minZRotation, maxZRotation);

        // Apply the clamped rotation back to the target object
        targetObject.localEulerAngles = new Vector3(normalizedX, normalizedY, normalizedZ);
    }
}
