using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint_v2 : MonoBehaviour {
    static Transform playerTransform;
    public Text checkpointNum;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Crash").transform;
        if (GameObject.FindGameObjectWithTag("Crash") != null)
        {
            Debug.Log("It has been found.");
        }
       

    }

    void OnTriggerEnter(Collider other)
    {
       

        //Debug.Log("This is me", transform);
        //Debug.Log("This is Checkpoint 0", Laps.checkpointA[Laps.currentCheckpoint].transform);
        //Is it the Ship that enters the collider?
        if (other.gameObject.transform.tag != "Crash")
        {
            return;//If it's not the ship dont continue

        }
        if (transform == Laps.checkpointA[Laps.currentCheckpoint].transform)
        {
            Debug.Log("You just passed a checkpoint");
            //Check so we dont exceed our checkpoint quantity
            if (Laps.currentCheckpoint + 1 < Laps.checkpointA.Length)
            {
                Debug.Log("You just passed a checkpoint");
                //Add to currentLap if currentCheckpoint is 0
                if (Laps.currentCheckpoint == 0)
                    Laps.currentLap++;
                Laps.currentCheckpoint++;
            }
            else
            {
                //If we dont have any Checkpoints left, go back to 0
                Laps.currentCheckpoint = 0;
            }
        }


    }
    void Visual ()
    {
       
    }





}


   