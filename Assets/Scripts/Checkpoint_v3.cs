﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Checkpoint_v3 : MonoBehaviour
{
    //static Transform shipTransform;
    public Transform[] checkPointArray;
    //public static Transform[] checkpointA;
    public int currentCheckpoint;
    public int currentLap;
    public Vector3 startPos;
    //public int Lap;
    //public int checkPoint;
    float startTime;
    //float endTime;
    float levelTimer;
    bool updateTimer;
    //bool alapCompleted = false;
    string minutes;
    string seconds;
    float t;
    public Text timerText;
    public Text checkpointNum;
    public Text lapNum;
    public GameObject finish;

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
       
       
        startTime = Time.time;
        startPos = transform.position;
        currentCheckpoint = 0;
        currentLap = 1;
        updateTimer = true;


        StartCoroutine(FindCheckPoints());
    }

    IEnumerator FindCheckPoints()
    {
        yield return new WaitForEndOfFrame();

        if (GameObject.FindGameObjectsWithTag("Point") != null)
        {
            //Debug.Log("the checkpoints have been found.");
        }
    }
    IEnumerator WaitThreeSeconds()
    {
       
        yield return new WaitForSeconds(8.5f);
        TimerStarted();

    }


    void Update()
    {
        StartCoroutine(WaitThreeSeconds());


        //Lap = currentLap;
        //checkPoint = currentCheckpoint;
        //checkpointA = checkPointArray;
    }

    void TimerStarted()
    {
        t = (Time.time - 8.5f) - startTime;
        minutes = Mathf.Floor((int)t / 60).ToString("00");
        seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;

        if (updateTimer)
        {
            levelTimer += startTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("This is me", transform);
        //Debug.Log("This is Checkpoint 0", Laps.checkpointA[Laps.currentCheckpoint].transform);

        //Is it the Ship that enters the collider?
        if (other.tag != "Point")
        {
            return;//If it's not the checkpoints or finish don't continue
        }

        if (other.transform == checkPointArray[currentCheckpoint].transform)
        {
            //Debug.Log("You just passed a checkpoint");

            // Set the respawn point for the ship to the last checkpoint
            transform.GetComponentInChildren<PlayerHealth>().respawnPoint = checkPointArray[currentCheckpoint].transform;

            // Set the tempNode for the AI to the currentNode at the checkpoint (when they respawn, currentNode gets set to tempNode)
            transform.GetComponent<ShipAI>().tempNode = transform.GetComponent<ShipAI>().currentNode;

            //Check so we don't exceed our checkpoint quantity
            if (currentCheckpoint < checkPointArray.Length - 1)
            {
                currentCheckpoint++;
            }
            else
            {
                //If we don't have any Checkpoints left, go back to 0 and increase currentLap
                currentCheckpoint = 0;
                currentLap++;
            }

            checkpointNum.text = "Checkpoints: " + currentCheckpoint;
            lapNum.text = "Lap Number: " + currentLap;
        }
    }

    //void LevelFinished()
    //{
    //    if (currentLap == 3)
    //    {
    //        updateTimer = false;
    //        SceneManager.LoadScene("MainMenu");
            
    //    }
    //}
}