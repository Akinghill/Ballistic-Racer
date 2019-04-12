using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{

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
        if (Input.GetButtonDown("Pause"))
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
