using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_maninmenu : MonoBehaviour
{
    public int scene_to_be_loaded;

    public GameObject main;
    public GameObject about;
    public void about_button()
    {
        main.SetActive(false);
        about.SetActive(true);
    }
    public void back_to_main()
    {
        main.SetActive(true);
        about.SetActive(false);
    }

    public void Play_the_game()
    {
        SceneManager.LoadScene(scene_to_be_loaded);
    }
}
