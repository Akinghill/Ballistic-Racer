using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XInputDotNetPure;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{
    GamePadState m_P1State;
    GamePadState m_P2State;
    GamePadState m_P3State;
    GamePadState m_P4State;

    GamePadState m_P1PrevState;
    GamePadState m_P2PrevState;
    GamePadState m_P3PrevState;
    GamePadState m_P4PrevState;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject creditMenuUI;

    public string mainMenu;

    bool loadMainMenu;

    void Start()
    {
        loadMainMenu = false;
        GameIsPaused = false;
        StartCoroutine(LoadScene());
    }

    void Update()
    {
        // Set the previous state for 1-4 player controllers to the state from the last frame
        m_P1PrevState = m_P1State;
        m_P2PrevState = m_P2State;
        m_P3PrevState = m_P3State;
        m_P4PrevState = m_P4State;

        // Get gamepad states for 1-4 player controllers for this frame
        m_P1State = GamePad.GetState(PlayerIndex.One);
        m_P2State = GamePad.GetState(PlayerIndex.Two);
        m_P3State = GamePad.GetState(PlayerIndex.Three);
        m_P4State = GamePad.GetState(PlayerIndex.Four);

        // Check if player 1 pressed the Start button this frame
        if (m_P1PrevState.Buttons.Start == ButtonState.Released && m_P1State.Buttons.Start == ButtonState.Pressed)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // Check if player 2 pressed the Start button this frame
        if (m_P2PrevState.Buttons.Start == ButtonState.Released && m_P2State.Buttons.Start == ButtonState.Pressed)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // Check if player 3 pressed the Start button this frame
        if (m_P3PrevState.Buttons.Start == ButtonState.Released && m_P3State.Buttons.Start == ButtonState.Pressed)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // Check if player 4 pressed the Start button this frame
        if (m_P4PrevState.Buttons.Start == ButtonState.Released && m_P4State.Buttons.Start == ButtonState.Pressed)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // Check if the escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Credits()
    {
        pauseMenuUI.SetActive(false);
        creditMenuUI.SetActive(true);
    }

    public void Back()
    {
        creditMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void StartMenu()
    {
        Time.timeScale = 1f;
        loadMainMenu = true;
        MainMenu.level = "MainMenu";
        DontDestroy.menuMusic.GetComponent<AudioSource>().Play();
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    IEnumerator LoadScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("LoadingScene");
        asyncOperation.allowSceneActivation = false;

        yield return new WaitUntil(() => loadMainMenu);

        asyncOperation.allowSceneActivation = true;
    }
}
