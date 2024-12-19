using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class wall_movement : MonoBehaviour
{
    [SerializeField] private TextMeshPro textoftime;
   


    [SerializeField] private Transform cut_out_wall;
    [SerializeField] private wall_spawner wallspawner;


    public float countdownTime ; // Countdown time in seconds
    [SerializeField] private float movementSpeed; // Speed at which the wall moves
   // [SerializeField]private float destroyDelay;
    [SerializeField] private Vector3 movementDirection = Vector3.forward; // Direction of movement

    public bool isMoving = false; // Tracks if the wall should start moving
    //public delegate void WallDestroyed();
  //  public static event WallDestroyed OnWallDestroyed; // Event triggered when the wall is destroyed

   

    private void Start()
    {
       
        wallspawner.frstPose = true;
        wallspawner = FindObjectOfType<wall_spawner>();

    }

    private void Update()
    {
        

        // Update countdown display
        textoftime.text = Mathf.Max(0, countdownTime).ToString("F1") + "s";

        GameObject wall = GameObject.FindGameObjectWithTag("cut_out_wall");
        cut_out_wall = wall.GetComponent<Transform>();


        if (isMoving)
        {
            // Move the wall in the specified direction
            cut_out_wall.Translate(movementDirection.normalized * movementSpeed * Time.deltaTime);
        }
        // Start the countdown
        StartCoroutine(StartCountdown());



        
    }

    

    private IEnumerator StartCountdown()
    {
        // Countdown timer
        float timeRemaining = countdownTime;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            countdownTime = timeRemaining; // Update the display value
            yield return null; // Wait for the next frame
        }

        // Countdown complete, start moving the wall
        isMoving = true;
        Debug.Log("Wall is moving!");

       
    }

    

    
}
