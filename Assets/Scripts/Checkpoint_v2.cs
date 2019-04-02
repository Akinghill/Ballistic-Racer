using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint_v2 : MonoBehaviour {
    static Transform playerTransform;
    public Text checkpointNum;
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Ship").transform;

    }

    void OnTriggerEnter(Collider other)
    {
        //Is it the Player who enters the collider?
        if (!other.CompareTag("Ship"))
           return;//If it's not the player dont continue


        if (transform == Laps.checkpointA[Laps.currentCheckpoint].transform)
        {
           
            //Check so we dont exceed our checkpoint quantity
            if (Laps.currentCheckpoint + 1 < Laps.checkpointA.Length)
            {
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


   