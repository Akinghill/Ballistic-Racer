﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint_v3 : MonoBehaviour
{
    [Serializable]
    public class Checkpoints
    {
        public Transform[] checkPointArray;

        public Checkpoints(Transform[] array)
        {
            checkPointArray = array;
        }
    }

    public Checkpoints[] checkPointArray;

    public int currentCheckpoint;
    public int currentLap;
    public Vector3 startPos;
    float raceTimer;
    string raceTimerString;
    public bool updateTimer;
    public Text timerText;
    public Text checkpointNum;
    public Text lapNum;
    public Text positionText;
    public int position;
    public GameObject finish;

    public Transform lastCheckpoint;
    public Transform nextCheckpoint;
    public float lastPointDistance;
    public float nextPointDistance;

    public GameObject rankingManager;
    float MaxSpeedStart;
    RaceManager raceManager;

    GameObject[] check;
    int checkChildren;

    void Awake()
    {
        MaxSpeedStart = GetComponent<ShipMovement>().maxSpeed;
        rankingManager = GameObject.Find("Ranking Manager");
        raceManager = rankingManager.GetComponent<RaceManager>();
    }

    void Start()
    {
        startPos = transform.position;
        currentCheckpoint = 0;
        currentLap = 1;
        raceTimerString = FormatRaceTime(raceTimer);
        timerText.text = raceTimerString;
        lapNum.text = "Lap: " + currentLap;

        StartCoroutine(FindCheckPoints());

        check = GameObject.FindGameObjectsWithTag("Checkpoint");

        if (check.Length > 1)
        {
            checkChildren = check[0].transform.childCount;
            int checkChildren2 = check[1].transform.childCount;
            checkPointArray = new Checkpoints[]
            {
                new Checkpoints(new Transform[checkChildren]),
                new Checkpoints(new Transform[checkChildren2])
            };

            for (int i = 0; i < checkChildren; i++)
            {
                checkPointArray[0].checkPointArray[i] = check[0].transform.GetChild(i);
            }

            for (int j = 0; j < checkChildren2; j++)
            {
                checkPointArray[1].checkPointArray[j] = check[1].transform.GetChild(j);
            }

            lastCheckpoint = checkPointArray[0].checkPointArray[checkPointArray[0].checkPointArray.Length - 1];
            nextCheckpoint = checkPointArray[0].checkPointArray[0];
        }
        else
        {
            checkChildren = check[0].transform.childCount;
            checkPointArray = new Checkpoints[]
            {
                new Checkpoints(new Transform[checkChildren]),
            };

            for (int i = 0; i < checkChildren; i++)
            {
                checkPointArray[0].checkPointArray[i] = check[0].transform.GetChild(i);
            }

            lastCheckpoint = checkPointArray[0].checkPointArray[checkPointArray[0].checkPointArray.Length - 1];
            nextCheckpoint = checkPointArray[0].checkPointArray[0];
        }

        lastPointDistance = Vector3.Distance(transform.position, lastCheckpoint.transform.position);
        nextPointDistance = Vector3.Distance(transform.position, nextCheckpoint.transform.position);

        StartCoroutine(TimerDelayedStart());
    }

    IEnumerator FindCheckPoints()
    {
        yield return new WaitForEndOfFrame();

        //if (GameObject.FindGameObjectsWithTag("Point") != null)
        //{
        //    //Debug.Log("the checkpoints have been found.");
        //}
    }

    IEnumerator TimerDelayedStart()
    {
        yield return new WaitForSeconds(8.5f);
        updateTimer = true;
    }

    void Update()
    {
        if (updateTimer)
        {
            raceTimer += Time.deltaTime;
            raceTimerString = FormatRaceTime(raceTimer);
            timerText.text = raceTimerString;
        }

        lastPointDistance = Vector3.Distance(transform.position, lastCheckpoint.transform.position);
        nextPointDistance = Vector3.Distance(transform.position, nextCheckpoint.transform.position);

        position = raceManager.ships.IndexOf(this) + 1;
        if (position <= 1)
        {
            position = 1;
        }
        else if (position >= raceManager.numberOfRacers)
        {
            position = raceManager.numberOfRacers;
        }

        switch (position)
        {
            case 1:
                positionText.text = position + "st";
                GetComponent<ShipMovement>().maxSpeed = MaxSpeedStart;
                break;

            case 2:
                positionText.text = position + "nd";
                GetComponent<ShipMovement>().maxSpeed = MaxSpeedStart + 1;
                break;

            case 3:
                positionText.text = position + "rd";
                GetComponent<ShipMovement>().maxSpeed = MaxSpeedStart + 2;
                break;

            case 4:
                positionText.text = position + "th";
                GetComponent<ShipMovement>().maxSpeed = MaxSpeedStart + 5;
                break;

            case 5:
                positionText.text = position + "th";
                GetComponent<ShipMovement>().maxSpeed = MaxSpeedStart + 7;
                break;

            case 6:
                positionText.text = position + "th";
                GetComponent<ShipMovement>().maxSpeed = MaxSpeedStart + 10;
                break;

            case 7:
                positionText.text = position + "th";
                GetComponent<ShipMovement>().maxSpeed = MaxSpeedStart + 15;
                break;
        }
    }

    public string FormatRaceTime(float raceTime)
    {
        int minutes = (int)raceTime / 60;
        int seconds = (int)raceTime % 60;
        float milliseconds = raceTime * 1000;
        milliseconds = milliseconds % 1000;

        string raceTimeString = raceTime.ToString();
        raceTimeString = string.Format("{0}:{1:00}.{2:000}", minutes, seconds, milliseconds);
        return raceTimeString;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Point")
        {
            return;
        }

        for (int i = 0; i < check.Length; i++)
        {
            if (other.transform == checkPointArray[i].checkPointArray[currentCheckpoint].transform)
            {
                // Set the respawn point for the ship to the last checkpoint
                transform.GetComponentInChildren<PlayerHealth>().respawnPoint = checkPointArray[i].checkPointArray[currentCheckpoint].transform;

                // Set the tempNode for the AI to the currentNode at the checkpoint (when they respawn, currentNode gets set to tempNode)
                transform.GetComponent<ShipAI>().tempNode = transform.GetComponent<ShipAI>().currentNode;

                //Check so we don't exceed our checkpoint quantity
                if (currentCheckpoint < checkPointArray[i].checkPointArray.Length - 1)
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
                lapNum.text = "Lap: " + currentLap;

                if (currentCheckpoint != 0)
                {
                    lastCheckpoint = checkPointArray[i].checkPointArray[currentCheckpoint - 1];
                }
                else
                {
                    lastCheckpoint = checkPointArray[i].checkPointArray[0];
                }

                nextCheckpoint = checkPointArray[i].checkPointArray[currentCheckpoint];
            }
        }
        
    }
}