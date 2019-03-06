using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Checkpoint : MonoBehaviour
{
    public int numberOfLaps;

    void Update(CheckTrigger checkTrigger)
    {
        if(checkTrigger.finish.activeInHierarchy)
        {
            foreach(GameObject item in GameObject.FindGameObjectsWithTag("Respawn"))
            {
                item.SetActive(true);
            }
            checkTrigger.finish.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Check")
        {
            Debug.Log("Passed");
            CheckTrigger checkTrigger = other.GetComponent<CheckTrigger>();
            if (checkTrigger.checkpointReached)
            {
                CompleteLap(checkTrigger);
            }
            other.gameObject.SetActive(false);
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
