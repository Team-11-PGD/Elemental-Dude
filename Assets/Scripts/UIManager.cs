using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool gameIsPaused = false;
    GameObject pauseMenu;

    void Start()
    {
        SceneManager.activeSceneChanged += SceneChanged;
        SetMouseState(SceneManager.GetActiveScene().name != "GameScene");
        FindPauseMenu();
    }

    void FindPauseMenu()
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
    }

    void SetMouseState(bool value)
    {
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = value;
    }

    public void PlayGame()
    {
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
        FindPauseMenu();
        if (newScene.name == "GameScene")
        {
            ResumeGame();
        }
    }

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

    void SwitchPause()
    {
        gameIsPaused = !gameIsPaused;
        SetMouseState(gameIsPaused);

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
