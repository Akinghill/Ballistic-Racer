using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_v3 : MonoBehaviour
    
{
    static Transform shipTransform;



    // Use this for initialization
    void Start()
    {
        StartCoroutine(findcheckPoints());
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator findcheckPoints()
    {
        yield return new WaitForEndOfFrame();


        if (GameObject.FindGameObjectsWithTag("Point") != null)
        {
            Debug.Log("the checkpoints have been found.");
        }

    }
    void OnTriggerEnter(Collider other)
    {


        //Debug.Log("This is me", transform);
        //Debug.Log("This is Checkpoint 0", Laps.checkpointA[Laps.currentCheckpoint].transform);
        //Is it the Ship that enters the collider?
        if (other.gameObject.transform.tag != "Point")
        {
            return;//If it's not the checkpoints dont continue

        }
        if (other.transform == Laps.checkpointA[Laps.currentCheckpoint].transform)
        {
            Debug.Log("You just passed a checkpoint");
            //Check so we dont exceed our checkpoint quantity
            if (Laps.currentCheckpoint + 1 < Laps.checkpointA.Length)
            {
                //Debug.Log("You just passed a checkpoint");
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

            other.GetComponent<Laps>().checkpointNum.text = "Checkpoints: " + Laps.currentCheckpoint;
            other.GetComponent<Laps>().lapNum.text = "Lap Number: " + Laps.currentLap;

            // Set the respawn point for the ship to the last checkpoint
            transform.GetComponentInParent<PlayerHealth>().respawnPoint = Laps.checkpointA[Laps.currentCheckpoint - 1].transform;

            // Set the tempNode for the AI to the currentNode at the checkpoint (when they respawn, currentNode gets set to tempNode)
            transform.GetComponentInParent<ShipAI>().tempNode = transform.GetComponentInParent<ShipAI>().currentNode;
        }
    }
}