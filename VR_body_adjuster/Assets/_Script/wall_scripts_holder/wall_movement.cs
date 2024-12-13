using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_movement : MonoBehaviour
{
    [SerializeField] private float countdownTime = 60f; // Countdown time in seconds
    [SerializeField] private float movementSpeed = 5f; // Speed at which the wall moves
    [SerializeField]private float destroyDelay;
    [SerializeField] private Vector3 movementDirection = Vector3.forward; // Direction of movement

    private bool isMoving = false; // Tracks if the wall should start moving
    public delegate void WallDestroyed();
    public static event WallDestroyed OnWallDestroyed; // Event triggered when the wall is destroyed

    private void Start()
    {
        // Start the countdown
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {
        if (isMoving)
        {
            // Move the wall in the specified direction
            transform.Translate(movementDirection.normalized * movementSpeed * Time.deltaTime);
        }
    }

    private IEnumerator StartCountdown()
    {
        // Wait for the countdown to finish
        yield return new WaitForSeconds(countdownTime);

        // Start moving the wall
        isMoving = true;
        Debug.Log("Wall is moving!");

        // Destroy the wall after the delay
        StartCoroutine(DestroyWall());
    }

    private IEnumerator DestroyWall()
    {
        // Wait for the destroy delay
        yield return new WaitForSeconds(destroyDelay);

        // Notify the spawner
        OnWallDestroyed?.Invoke();

        // Destroy the current wall
        Destroy(gameObject);
        Debug.Log("Wall destroyed!");
    }
}
