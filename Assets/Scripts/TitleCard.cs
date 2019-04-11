using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCard : MonoBehaviour {

    private void Start()
    {
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainMenu");
        asyncOperation.allowSceneActivation = false;

        yield return new WaitForSeconds(13.0f);

        asyncOperation.allowSceneActivation = true;
    }
}
