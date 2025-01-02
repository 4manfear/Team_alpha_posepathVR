using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InLevelScene_changer : MonoBehaviour
{
    [SerializeField] int next_scene_index_value;
    [SerializeField] pose_accruation_checker pac;

    private void Start()
    {
        pac = FindObjectOfType<pose_accruation_checker>();
    }

    private void FixedUpdate()
    {
        if(pac.matchedCount == 4)
        {
            StartCoroutine(sceneloader());
        }
    }
    IEnumerator sceneloader()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(next_scene_index_value);
    }

}
