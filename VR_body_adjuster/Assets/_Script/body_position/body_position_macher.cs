using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body_position_macher : MonoBehaviour
{
    [SerializeField] Transform sameBodyTarget;

    [SerializeField] float accurateGap;
    [SerializeField] Material color_green;
    [SerializeField] Material color_red;
    [SerializeField] Renderer objectRenderer; // Renderer of the GameObject to change material

    
    public bool mached;


    private void Start()
    {
        objectRenderer.material = color_red;
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
