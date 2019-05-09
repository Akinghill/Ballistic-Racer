using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    // The folder to contain our screenshots.
    // If the folder exists we will append numbers to create an empty folder.
    public string folder = "ScreenshotFolder";

    void Start()
    {
        // Create the folder
        System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/" + folder);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            DateTime dateTime = DateTime.Now;
            string date = dateTime.ToString("yyyy-MM-dd HH.mm.ss");

            string name = string.Format(Application.persistentDataPath + "/{0}/Screenshot {1}.png", folder, date);

            // Capture the screenshot to the specified file.
            ScreenCapture.CaptureScreenshot(name);
        }
    }
}
