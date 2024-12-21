using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class flex_coin_adder : MonoBehaviour
{
    [SerializeField] private pose_accruation_checker pac; // Pose accuracy checker
    [SerializeField] private inlevel_1_coin levelOneScriptableObject; // Scriptable Object for level

    public TextMeshPro flexCoinDisplay;

    public int perfectMatchMaxCoin;
    public int threeMatchMaxCoin;
    public int lessThanThreeMaxCoin;

    private bool hasAddedPerfectCoins = false;
    private bool hasAddedThreeMatchCoins = false;
    private bool hasAddedLessThanThreeCoins = false;

    private void Start()
    {
        // Find the pose accuracy checker if not assigned
        if (pac == null)
        {
            pac = FindObjectOfType<pose_accruation_checker>();
        }

        // Log error if pac is still null
        if (pac == null)
        {
            Debug.LogError("PoseAccruationChecker not found in the scene!");
        }

        // Ensure the Scriptable Object is assigned
        if (levelOneScriptableObject == null)
        {
            Debug.LogError("Level One Scriptable Object is not assigned!");
        }
    }

    private void Update()
    {
        if (pac == null || levelOneScriptableObject == null) return;

        // Check for 4 matched parts
        if (pac.matchedCount == 4 && !levelOneScriptableObject.level_is_completed && !hasAddedPerfectCoins)
        {
            levelOneScriptableObject.in_level_flexCoin += perfectMatchMaxCoin;
            hasAddedPerfectCoins = true;
        }
        // Check for 3 matched parts
        else if (pac.matchedCount == 3 && !levelOneScriptableObject.level_is_completed && !hasAddedThreeMatchCoins)
        {
            levelOneScriptableObject.in_level_flexCoin += threeMatchMaxCoin;
            hasAddedThreeMatchCoins = true;
        }
        // Check for less than 3 matched parts
        else if (pac.matchedCount < 3 && pac.matchedCount > 0 && !levelOneScriptableObject.level_is_completed && !hasAddedLessThanThreeCoins)
        {
            levelOneScriptableObject.in_level_flexCoin += lessThanThreeMaxCoin;
            hasAddedLessThanThreeCoins = true;
        }

        // Optional: Cap the maximum coin value
        // Uncomment if needed
        // if (levelOneScriptableObject.in_level_flexCoin > 9)
        // {
        //     levelOneScriptableObject.in_level_flexCoin = 9;
        // }

        // Update the flex coin display
        if (flexCoinDisplay != null)
        {
            flexCoinDisplay.text = levelOneScriptableObject.in_level_flexCoin.ToString();
        }
    }

    private void FixedUpdate()
    {
        // Reset coin addition flags when checking stops
        if (pac != null && !pac.startChecking)
        {
            hasAddedPerfectCoins = false;
            hasAddedThreeMatchCoins = false;
            hasAddedLessThanThreeCoins = false;
        }
    }

}
