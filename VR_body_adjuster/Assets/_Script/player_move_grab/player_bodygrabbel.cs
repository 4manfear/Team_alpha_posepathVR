using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class player_bodygrabbel : MonoBehaviour
{
    [SerializeField] private InputActionReference _rightHandTriggerButton;
    [SerializeField] private InputActionReference _rightHandGrabButton;
    [SerializeField] private InputActionReference _leftHandTriggerButton;
    [SerializeField] private InputActionReference _leftHandGrabButton;

    [SerializeField] private Transform rightHandTransform; // Reference to the right hand/controller transform in the scene
    [SerializeField] private Transform leftHandTransform;  // Reference to the left hand/controller transform in the scene

    [SerializeField] private XRBaseController rightXRController; // Reference to the XRController for right hand
    [SerializeField] private XRBaseController leftXRController;  // Reference to the XRController for left hand


    [SerializeField] private float distance_to_grab;

    public Transform grabbedObject; // Currently grabbed body part
    private bool isGrabbing;
    private Quaternion previousHandRotation; // Stores the previous rotation of the hand

    private void Start()
    {
        

        // Enable input action events
        //ctx is the call back function used in here to bind the button and the function calling it
        _rightHandGrabButton.action.performed += ctx => TryGrabRightHand();
        _rightHandGrabButton.action.canceled += ctx => ReleaseRightHand();

        _leftHandGrabButton.action.performed += ctx => TryGrabLeftHand();
        _leftHandGrabButton.action.canceled += ctx => ReleaseLeftHand();
    }

    private void TryGrabRightHand()
    {
        Debug.Log("grab button pressed");

        RaycastHit hit;

        Debug.DrawRay(rightHandTransform.position, rightHandTransform.up, Color.black, 0.5f);

        if (Physics.Raycast(rightHandTransform.position, rightHandTransform.forward, out hit))
        {
            if (hit.collider.CompareTag("BodyPart"))
            {
                Debug.Log($"Hit body part: {hit.collider.name}");

                // Parent the hit object to the hand for direct movement
                grabbedObject = hit.transform;

                // Save the initial hand rotation when grabbing starts
                previousHandRotation = rightHandTransform.rotation;


                // Ensure the grabbed object remains at its current position
                isGrabbing = true;

                //if (rightHandTransform.position.magnitude <= distance_to_grab)
                //{

                //}

                // Trigger haptic feedback on the right controller
                TriggerHapticFeedback(rightXRController, 0.5f, 0.2f); // Intensity: 0.5, Duration: 0.2 seconds
            
            }
        }
    }

    private void TryGrabLeftHand()
    {
        Debug.Log("Grab button pressed (Left)");

        RaycastHit hit;
        Debug.DrawRay(leftHandTransform.position, leftHandTransform.forward, Color.black, 0.5f);

        if (Physics.Raycast(leftHandTransform.position, leftHandTransform.forward, out hit))
        {
            if (hit.collider.CompareTag("BodyPart"))
            {
                Debug.Log($"Hit body part: {hit.collider.name}");

                // Parent the hit object to the hand for direct movement
                grabbedObject = hit.transform;

                // Save the initial hand rotation when grabbing starts
                previousHandRotation = leftHandTransform.rotation;

                // Ensure the grabbed object remains at its current position
                isGrabbing = true;

                // Trigger haptic feedback on the left controller
                TriggerHapticFeedback(leftXRController, 0.5f, 0.2f); // Intensity: 0.5, Duration: 0.2 seconds
            }
        }
    }

    

    private void ReleaseRightHand()
    {
        if (isGrabbing && grabbedObject != null)
        {
            Debug.Log("Releasing body part");

            grabbedObject = null;
            isGrabbing = false;
        }
    }

    private void ReleaseLeftHand()
    {
        if (isGrabbing && grabbedObject != null)
        {
            Debug.Log("Releasing body part (Left)");

            grabbedObject = null;
            isGrabbing = false;
        }
    }

    private void Update()
    {
        if (isGrabbing && grabbedObject != null)
        {
            // Calculate the rotation offset based on hand's current rotation
            Quaternion currentHandRotation = rightHandTransform.rotation;
            Quaternion rotationOffset = currentHandRotation * Quaternion.Inverse(previousHandRotation);

            // Apply the offset to the grabbed object
            grabbedObject.rotation = Quaternion.Inverse(rotationOffset) * grabbedObject.rotation ;

            

            // Update the previous hand rotation
            previousHandRotation = currentHandRotation;
        }
    }

    private void TriggerHapticFeedback(XRBaseController controller, float intensity, float duration)
    {
        if (controller != null)
        {
            controller.SendHapticImpulse(intensity, duration);
        }
    }





}
