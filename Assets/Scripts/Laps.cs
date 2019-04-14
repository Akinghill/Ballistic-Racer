using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laps : MonoBehaviour
{
    // These Static Variables are accessed in "checkpoint" Script
    public Transform[] checkPointArray;
    public static Transform[] checkpointA;
    public static int currentCheckpoint = 0;
    public static int currentLap = 1;
    public Vector3 startPos;
    public int Lap;
    public int checkPoint;
    float startTime;
    float endTime;
    float levelTimer;
    bool updateTimer;
    bool alapCompleted = false;
    string minutes;
    string seconds;
    float t;
    public Text timerText;
    public Text checkpointNum;
    public Text lapNum;

    void Awake()
    {
        GameObject check = GameObject.FindGameObjectWithTag("Checkpoint");
        int checkChildren = check.transform.childCount;
        checkPointArray = new Transform[checkChildren];

        for (int i = 0; i < checkChildren; i++)
        {
            checkPointArray[i] = check.transform.GetChild(i);
        }
    }

    void Start()
    {
        startTime = Time.timeSinceLevelLoad;
        startPos = transform.position;
        currentCheckpoint = 0;
        currentLap = 0;
       

    }

    void Update()
    {
        float t = Time.timeSinceLevelLoad - startTime;
        string minutes = Mathf.Floor((int)t / 60).ToString("00");
        string seconds = Mathf.Floor(t % 60).ToString("00");
        timerText.text = "Time Running: " + minutes + ":" + seconds;

        if (updateTimer)
        {
            levelTimer += startTime;
        }

        Lap = currentLap;
        checkPoint = currentCheckpoint;
        checkpointA = checkPointArray;
    }

}



