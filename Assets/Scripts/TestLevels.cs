using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevels : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            AITestTrack1();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            AITestTrack2();
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            PlayExplosion();
        }
    }

    public void AITestTrack1()
    {
        SceneManager.LoadScene("Andrew_AI_Test-Track_1");
    }

    public void AITestTrack2()
    {
        SceneManager.LoadScene("Andrew_AI_Test-Track_2");
    }

    public void PlayExplosion()
    {
        SceneManager.LoadScene("JamieBTestExpPart");
    }
}
