using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public bool gameIsPaused = false;
    GameObject pauseMenu;
    [SerializeField]
    Health player;
    [SerializeField]
    Slider playerHpBar;
    [SerializeField]
    bool startWithoutMouseOverride = false;
    [SerializeField]
    UIScore iScore;

    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        if (player != null)
            player.Died += PlayerDied;
    }

    void OnDisable()
    {
        if (player != null)
            player.Died -= PlayerDied;
    }


    void PlayerDied()
    {
        //SOUND: (Player death)
        GoToMainMenu();
        
    }

    void Start()
    {
        if (startWithoutMouseOverride) SetMouseState(false);
        else SetMouseState(SceneManager.GetActiveScene().name != "InBetweenLevel1");

        SceneManager.activeSceneChanged += SceneChanged;
        FindPauseMenu();
        // TODO: put this in its own script on the slider
        if (playerHpBar != null)
        {
            playerHpBar.maxValue = player.maxHp;
            playerHpBar.value = player.currentHp;
        }
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
        SceneManager.LoadScene("InBetweenLevel1");
    }
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
    public void GoToMainMenu()
    {
        iScore.UpdateTimeScore();
        //updates the best play time.
        //iScore.UpdateTimeScore();
        SceneManager.LoadScene("MainMenu");
    }

    void SceneChanged(Scene oldScene, Scene newScene)
    {
        FindPauseMenu();
        if (newScene.name == "InBetweenLevel1" || newScene.name == "InBetweenLevel2" || newScene.name == "InBetweenLevel3")
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
        //SOUND: ( Switch sounds)
        gameIsPaused = !gameIsPaused;
        SetMouseState(gameIsPaused);

        pauseMenu?.SetActive(gameIsPaused);
        Time.timeScale = gameIsPaused ? 0 : 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (SceneManager.GetActiveScene().name == "InBetweenLevel1" || SceneManager.GetActiveScene().name == "InBetweenLevel2" || SceneManager.GetActiveScene().name == "InBetweenLevel3"))
        {
            SwitchPause();
        }

        //Hp Bar functionality
        // TODO: put this in its own script on the slider
        if (playerHpBar != null)
        {
            playerHpBar.value = player.currentHp;


            if (player.currentHp <= 0)
            {
                //GameOver
                GoToMainMenu();
            }
        }
    }

}
