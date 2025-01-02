using UnityEngine;

public class restrain_rotation_New : MonoBehaviour
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

    private Vector3 currentRotation; // Cached rotation to avoid Unity's recalculation

    void Start()
    {
        if (targetObject != null)
        {
            currentRotation = targetObject.localEulerAngles;
        }
    }

    void Update()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target Object is not assigned. Please assign a target object in the Inspector.");
            return;
        }

        // Update the cached rotation from the target object
        currentRotation = targetObject.localEulerAngles;

        // Normalize rotations to be within -180 to 180 degrees
        float normalizedX = NormalizeAngle(currentRotation.x);
        float normalizedY = NormalizeAngle(currentRotation.y);
        float normalizedZ = NormalizeAngle(currentRotation.z);

        // Clamp the rotation for each axis if enabled
        if (restrainX)
            normalizedX = Mathf.Clamp(normalizedX, minXRotation, maxXRotation);

        if (restrainY)
            normalizedY = Mathf.Clamp(normalizedY, minYRotation, maxYRotation);

        if (restrainZ)
            normalizedZ = ClampReversed(normalizedZ, minZRotation, maxZRotation);

        // Apply the clamped rotation back to the cached rotation
        currentRotation = new Vector3(normalizedX, normalizedY, normalizedZ);

        // Convert the adjusted rotation to a Quaternion and apply it to the target object
        targetObject.localRotation = Quaternion.Euler(currentRotation);
    }

    // Helper function to normalize an angle to the range [-180, 180]
    private float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;

        return angle;
    }

    // Helper function to clamp a value with reversed bounds
    private float ClampReversed(float value, float min, float max)
    {
        if (min > max)
        {
            // Swap min and max if they're reversed
            float temp = min;
            min = max;
            max = temp;
        }
        return Mathf.Clamp(value, min, max);
    }
}
