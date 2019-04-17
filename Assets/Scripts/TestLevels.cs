using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevels : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            RespawnTestTrack();
        }
    }

    public void RespawnTestTrack()
    {
        DontDestroy.menuMusic.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("RespawnTest");
    }
}
