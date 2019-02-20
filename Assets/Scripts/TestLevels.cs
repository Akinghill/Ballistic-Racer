using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevels : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PlayMovingPlatforms();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            PlayExplosion();
        }
    }

    public void PlayMovingPlatforms()
    {
        SceneManager.LoadScene("Rose_MichaelB");
    }

    public void PlayExplosion()
    {
        SceneManager.LoadScene("JamieBTestExpPart");
    }
}
