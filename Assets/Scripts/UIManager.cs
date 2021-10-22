using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool gameIsPaused = false;

    void Start()
    {
        SceneManager.activeSceneChanged += SceneChanged;
    }

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

    void SceneChanged(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "GameScene")
        {
            ResumeGame();
        }
    }

    GameObject pauseMenu;

    public void ResumeGame()
    {
        if (gameIsPaused)
        {
            SwitchPause();
        }
    }

    public void PauseGame()
    {
        if (!gameIsPaused)
        {
            SwitchPause();
        }
    }

    public void SwitchPause()
    {
        if (pauseMenu == null)
        {
            GameObject[] gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.name == "PauseMenu")
                {
                    pauseMenu = gameObject;
                    break;
                }
            }
        }

        gameIsPaused = !gameIsPaused;

        pauseMenu?.SetActive(gameIsPaused);
        Time.timeScale = gameIsPaused ? 0 : 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "GameScene")
        {
            SwitchPause();
        }
    }

}
