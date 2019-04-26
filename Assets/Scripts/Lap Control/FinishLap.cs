using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLap : MonoBehaviour
{
    public float restartDelay = 5f;
    public float restartTimer;
    public int numberOfLaps;
    public bool isFinished = false;
    public int finishes = 0;
    public bool raceIsOver;

    private void Start()
    {
        raceIsOver = false;
    }

    private void Update()
    {
        if (isFinished == true)
        {
            restartTimer += Time.deltaTime;
            if (restartTimer >= restartDelay)
            {
                raceIsOver = true;
                //ReturnToMenu();
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

            if (playerInput.controllerNumber != 0)
            {
                //Debug.Log("Passed");
                CompleteLap(checkpoint_V3, playerInput, shipAI);
            }
        }
    }

    public void CompleteLap(Checkpoint_v3 checkpoint_V3, PlayerInput playerInput, ShipAI shipAI)
    {
        if (checkpoint_V3.currentLap > numberOfLaps)
        {
            checkpoint_V3.finish.SetActive(true);
            playerInput.controllerNumber = 0;
            shipAI.currentNode = 0;
            finishes++;
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

    //void ReturnToMenu()
    //{
    //    SceneManager.LoadScene("MainMenu");
    //}
}
