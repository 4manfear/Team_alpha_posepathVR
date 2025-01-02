using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class wall_movement : MonoBehaviour
{
    [SerializeField] private TextMeshPro timerText;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private Vector3 movementDirection = Vector3.forward;

    [SerializeField] private Transform cutOutWall;
    [SerializeField]private float countdownTime;
    public bool isMoving;

    private void Start()
    {
       
       
        ResetWallMovement(countdownTime);
    }

    private void Update()
    {
        GameObject wall = GameObject.FindGameObjectWithTag("cut_out_wall");
        if (wall != null)
        {
            cutOutWall = wall.transform;
        }

        if (cutOutWall == null) return;

        UpdateTimerUI();

        if (isMoving)
        {
            MoveWall();
        }
    }

    private void UpdateTimerUI()
    {
        timerText.text = Mathf.Max(0, countdownTime).ToString("F1") + "s";
    }

    private void MoveWall()
    {
        cutOutWall.Translate(movementDirection.normalized * movementSpeed * Time.deltaTime);
    }

    public void ResetWallMovement(float timer)
    {
        countdownTime = timer;
        isMoving = false;
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        float timeRemaining = countdownTime;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            countdownTime = timeRemaining;
            yield return null;
        }

        isMoving = true;
    }

}
