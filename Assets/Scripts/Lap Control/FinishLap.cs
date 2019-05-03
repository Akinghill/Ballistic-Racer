using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLap : MonoBehaviour
{
    public float restartDelay = 5f;
    public float restartTimer;
    public float resultsDelay = 15f;
    public float resultsTimer;
    public int numberOfLaps;
    public bool isFinished = false;
    public int finishes = 0;
    public bool raceIsOver;
    public bool showResults;
    //GameObject results;

    private void Start()
    {
        raceIsOver = false;
        showResults = false;
        //results = GameObject.Find("Results Screen");
        //results.SetActive(false);
    }

    private void Update()
    {
        if (isFinished == true)
        {
            restartTimer += Time.deltaTime;
            if (restartTimer >= restartDelay)
            {
                showResults = true;
                //results.SetActive(true);
                resultsTimer += Time.deltaTime;
                if (resultsTimer >= resultsDelay)
                {
                    raceIsOver = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.tag == "Ship")
        {
            Checkpoint_v3 checkpoint_V3 = other.transform.parent.parent.GetComponent<Checkpoint_v3>();
            PlayerInput playerInput = other.transform.parent.parent.GetComponent<PlayerInput>();
            ShipAI shipAI = other.transform.parent.parent.GetComponent<ShipAI>();

            CompleteLap(checkpoint_V3, playerInput, shipAI);
        }
    }

    public void CompleteLap(Checkpoint_v3 checkpoint_V3, PlayerInput playerInput, ShipAI shipAI)
    {
        if (checkpoint_V3.currentLap > numberOfLaps)
        {
            checkpoint_V3.finish.SetActive(true);
            checkpoint_V3.updateTimer = false;
            playerInput.controllerNumber = 0;
            shipAI.currentNode = 0;

            if (playerInput.controllerNumber != 0)
            {
                finishes++;
            }
            
            if (PlayerManager.numOfPlayers == 1 && finishes == 1)
            {
                isFinished = true;
            }
            if (PlayerManager.numOfPlayers == 2 && finishes == 2)
            {
                isFinished = true;
            }
            if (PlayerManager.numOfPlayers == 3 && finishes == 3)
            {
                isFinished = true;
            }
            if (PlayerManager.numOfPlayers == 4 && finishes == 4)
            {
                isFinished = true;
            }
        }
    }
}
