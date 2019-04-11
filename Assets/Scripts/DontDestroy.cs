using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {

    public static GameObject menuMusic;

    private void Awake()
    {
        menuMusic = this.gameObject;
        DontDestroyOnLoad(this.gameObject);
    }
}
