using System.Collections;
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

    void Awake()
    {
        GameObject check = GameObject.FindGameObjectWithTag("Checkpoint");
        int checkChildren = check.transform.childCount;
        checkPointArray = new Transform[checkChildren];

        for (int i = 0; i < checkChildren; i++)
        {
            checkPointArray[i] = check.transform.GetChild(i);
        }

        lastCheckpoint = checkPointArray[checkPointArray.Length - 1];
        nextCheckpoint = checkPointArray[0];
        lastPointDistance = Vector3.Distance(transform.position, lastCheckpoint.transform.position);
        nextPointDistance = Vector3.Distance(transform.position, nextCheckpoint.transform.position);

        MaxSpeedStart = GetComponent<ShipMovement>().maxSpeed;
        rankingManager = GameObject.Find("Ranking Manager");
        raceManager = rankingManager.GetComponent<RaceManager>();
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

        lastPointDistance = Vector3.Distance(transform.position, lastCheckpoint.transform.position);
        nextPointDistance = Vector3.Distance(transform.position, nextCheckpoint.transform.position);

        position = raceManager.checks.IndexOf(this) + 1;
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
            lapNum.text = "Lap: " + currentLap;

            lastCheckpoint = checkPointArray[currentCheckpoint - 1];

            nextCheckpoint = checkPointArray[currentCheckpoint];
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