using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    public static bool GameIsPaused = false;
    string CurrentScene;
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGame()
    {

        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;

    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToPauseMenu()
    {


        SceneManager.LoadScene("PauseMenu");
        GameIsPaused = true;
    }

    public void Pause()
    {
        /*
            pause everything
        */

        // loads the pausemenu scene

        GoToPauseMenu();
    }
    public void Resume()
    {
        //loads currently the gamescene 
        SceneManager.LoadScene("GameScene");


        GameIsPaused = false;
    }
    void Update()
    {
        CurrentScene = SceneManager.GetActiveScene().name;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                //resume
                Resume();
            }
            else if (CurrentScene == "GameScene")
            {
                //pause
                Pause();
            }

        }

    }

}
