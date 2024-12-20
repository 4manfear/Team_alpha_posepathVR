using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class flex_coin_adder : MonoBehaviour
{
    [SerializeField] pose_accruation_checker pac;
    [SerializeField] inlevel_1_coin level_one_scritable_object;

    public TextMeshPro flex_coin_shower; 

    public int perfect_mached_max_coin;
    public int three_machem_max_coin;
    public int lessthen_three_max_coin;

    private bool hasAddedPerfectCoins = false;
    private bool hasAddedThreeMatchCoins = false;
    private bool hasAddedLessThanThreeCoins = false;


    private void Start()
    {
        pac = GameObject.FindAnyObjectByType<pose_accruation_checker>();
    }

    private void Update()
    {
        if(pac.matchedCount == 4 && level_one_scritable_object.level_is_completed != true && !hasAddedPerfectCoins)
        {
            level_one_scritable_object.in_level_flexCoin += perfect_mached_max_coin;
            hasAddedPerfectCoins = true;
        }
        if(pac.matchedCount == 3 && level_one_scritable_object.level_is_completed != true && !hasAddedThreeMatchCoins)
        {
            level_one_scritable_object.in_level_flexCoin += three_machem_max_coin;
            hasAddedThreeMatchCoins = true;
        }
        if(pac.matchedCount < 3 && pac.matchedCount>0 &&level_one_scritable_object.level_is_completed != true && !hasAddedLessThanThreeCoins)
        {
            level_one_scritable_object.in_level_flexCoin += lessthen_three_max_coin;
            hasAddedLessThanThreeCoins = true;
        }


        //if(level_one_scritable_object.in_level_flexCoin > 9)
        //{
        //    level_one_scritable_object.in_level_flexCoin = 9;
        //}

        flex_coin_shower.text = level_one_scritable_object.in_level_flexCoin.ToString();


    }

    private void FixedUpdate()
    {
        if (pac.start_checking == false)
        {
            hasAddedPerfectCoins = false;
            hasAddedThreeMatchCoins = false;
            hasAddedLessThanThreeCoins = false;
        }
    }



}
