using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    public Text lapNumber;

    public Text racePosition;

    public bool racerFinished = false;

    private Transform[] checkpoint;


    public bool checkpointReached;

    GameObject[] checksGO;

    List<RaceManager> checks = new List<RaceManager>();

    private Transform lastCheckpoint;
    private Transform nextCheckpoint;

    public GameObject miniMapIcon;

    public int position;

    public float lastPointDistance;
    public float nextPointDistance;

    public int pointID;

    int numberOfRacers;

    private float MaxSpeedStart;
    int currCheckpoint;

    void Start()
    {
        checksGO = GameObject.FindGameObjectsWithTag("Ship");
        foreach (GameObject check in checksGO)
        {
            checks.Add(check.GetComponent<RaceManager>());
        }
        currCheckpoint = GetComponent<Checkpoint_v3>().currentCheckpoint;
        numberOfRacers = checksGO.Length;
        position = numberOfRacers;
        checkpoint = GetComponent<Checkpoint_v3>().checkPointArray;

        lastCheckpoint = GetComponent<Checkpoint_v3>().checkPointArray[currCheckpoint - 1];
        nextCheckpoint = GetComponent<Checkpoint_v3>().checkPointArray[currCheckpoint + 1];

        MaxSpeedStart = GetComponentInParent<ShipMovement>().maxSpeed;
    }

    void Update()
    {
        lastPointDistance = Vector3.Distance(transform.position, lastCheckpoint.transform.position);
        nextPointDistance = Vector3.Distance(transform.position, nextCheckpoint.transform.position);
        switch (position)
        {
            case 1:
                racePosition.text = position + "st";
                GetComponentInParent<ShipMovement>().maxSpeed = MaxSpeedStart;
            break;

            case 2:
                racePosition.text = position + "nd";
                GetComponentInParent<ShipMovement>().maxSpeed = MaxSpeedStart + 1;
            break;

            case 3:
                racePosition.text = position + "rd";
                GetComponentInParent<ShipMovement>().maxSpeed = MaxSpeedStart + 2;
            break;

            case 4:
                racePosition.text = position + "th";
                GetComponentInParent<ShipMovement>().maxSpeed = MaxSpeedStart + 5;
            break;

            case 5:
                racePosition.text = position + "th";
                GetComponentInParent<ShipMovement>().maxSpeed = MaxSpeedStart + 7;
            break;

            case 6:
                racePosition.text = position + "th";
                //racePosition.text = "hah ur last u suck"
                GetComponentInParent<ShipMovement>().maxSpeed = MaxSpeedStart + 10;
            break;
        }
        

        foreach (RaceManager check in checks)
        {
            if (check != this && check.lapsCompleted > lapsCompleted)
            {
                position++;
            }
            else if (check != this && check.lapsCompleted < lapsCompleted)
            {
                position--;
            }
            else if (check != this && check.lapsCompleted == lapsCompleted)
            {
                if (check.pointID > pointID && check != this)
                {
                    position++;
                }
                else if (check.pointID < pointID && check != this)
                {
                    position--;
                }
                else if (check.pointID == pointID && check != this)
                {
                    if (check.lastPointDistance > lastPointDistance && check != this)
                    {
                        position++;
                    }
                    else if (check.lastPointDistance < lastPointDistance && check != this)
                    {
                        position--;
                    }
                }
            }
        }
        if (position <= 1)
        {
            position = 1;
        }
        else if (position >= numberOfRacers)
        {
            position = numberOfRacers;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Respawn")
        {
            checkpointReached = true;
            Debug.Log("Passed");
        }
        if (other.tag == "Respawn" || other.tag == "Checkpoint" || other.tag == "Finish")
        {
            playerHealth.respawnPoint = other.transform;
            lastPoint = other.gameObject;
        }
        if (other.tag == "Respawn" || other.tag == "Checkpoint")
        {
            pointID++;
        }
        if (other.tag == "Finish")
        {
            pointID = 0;
        }
    }
}
