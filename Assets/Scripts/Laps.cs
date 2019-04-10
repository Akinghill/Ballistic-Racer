using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        startPos = transform.position;
        currentCheckpoint = 0;
        currentLap = 0;
       

    }

    void Update()
    {
        Lap = currentLap;
        checkPoint = currentCheckpoint;
        checkpointA = checkPointArray;
    }

}



