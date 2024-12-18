using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class body_position_macher : MonoBehaviour
{
    public string tag_of_connected_body;
    

    [SerializeField] Transform sameBodyTarget;

    [SerializeField] float accurateGap;
    [SerializeField] Material color_green;
    [SerializeField] Material color_red;
    [SerializeField] Renderer objectRenderer; // Renderer of the GameObject to change material
    

    
    public bool mached;


    private void Start()
    {

        // Find the object with the specified tag and assign its Transform
        GameObject targetObject = GameObject.FindGameObjectWithTag(tag_of_connected_body);
        if (targetObject != null)
        {
            sameBodyTarget = targetObject.transform;

            // Try to get the Renderer component
            objectRenderer = targetObject.GetComponent<Renderer>();
            if (objectRenderer == null)
            {
                Debug.LogError($"The object with tag '{tag_of_connected_body}' does not have a Renderer component.");
            }
            else
            {
                // Initialize the material to red
                objectRenderer.material = color_red;
            }
        }
        else
        {
            Debug.LogError($"No GameObject found with tag: {tag_of_connected_body}");
        }





    }
    private void Update()
    {
       if(mached == false)
        {
            objectRenderer.material = color_red;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has the "BodyPart" tag
        if (other.CompareTag("BodyPart"))
        {
            // Calculate the distance between the positions
            float distance = Vector3.Distance(sameBodyTarget.position, this.transform.position);

            // Change the material based on the distance
            if (distance <= accurateGap)
            {
                objectRenderer.material = color_green;
                Debug.Log("Material changed to green.");
                mached = true;
            }
            else
            {
                objectRenderer.material = color_red;
                Debug.Log("Material changed to red.");
                mached = false;
            }
        }
    }

}
