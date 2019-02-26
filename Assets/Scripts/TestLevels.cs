using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevels : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PrototypeTrack();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            PlayExplosion();
        }
    }

    public void PrototypeTrack()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void PlayExplosion()
    {
        SceneManager.LoadScene("JamieBTestExpPart");
    }
}
