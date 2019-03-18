using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Checkpoint : MonoBehaviour
{
    public int numberOfLaps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Check")
        {
            Debug.Log("Passed");
            CheckTrigger checkTrigger = other.GetComponent<CheckTrigger>();
            if (checkTrigger.checkpointReached)
            {
                Debug.Log("Passed");
                CompleteLap(checkTrigger);
            }
        }
    }

    public void CompleteLap(CheckTrigger checkTrigger)
    {
        if (checkTrigger.lapsCompleted < numberOfLaps)
        {
            checkTrigger.lapsCompleted++;
            checkTrigger.checkpointReached = false;
        }
        else
        {
            checkTrigger.finish.SetActive(true);
        }
    }
}