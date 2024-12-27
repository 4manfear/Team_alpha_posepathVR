using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui_maninmenu : MonoBehaviour
{
    public int scene_to_be_loaded;

    public void quit_game()
    {
        Application.Quit();
    }

    public void Play_the_game()
    {
        SceneManager.LoadScene(scene_to_be_loaded);
    }
}
