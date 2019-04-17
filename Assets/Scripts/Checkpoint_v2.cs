//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Checkpoint_v2 : MonoBehaviour {
//    static Transform playerTransform;
//    //public Text checkpointNum;
//    //public Text lapNum;
    
//    /*float startTime;
//    float endTime;
//    float levelTimer;
//    bool updateTimer;
    
//    string minutes;
//    string seconds;
//    float t;*/
//    bool alapCompleted = false;

//    void Start()
//    {
//        StartCoroutine(FindShips());
//    }
//    void Update()
//    {
      
//    }

//    IEnumerator FindShips()
//    {
//        yield return new WaitForEndOfFrame();
//        playerTransform = GameObject.FindGameObjectWithTag("Crash").transform;
//        if (GameObject.FindGameObjectWithTag("Crash") != null)
//        {
//            Debug.Log("It has been found.");
//        }
//        Visual();
//    }

//    void OnTriggerEnter(Collider other)
//    {
       

//        //Debug.Log("This is me", transform);
//        //Debug.Log("This is Checkpoint 0", Laps.checkpointA[Laps.currentCheckpoint].transform);
//        //Is it the Ship that enters the collider?
//        if (other.gameObject.transform.tag != "Crash")
//        {
//            return;//If it's not the ship dont continue

//        }
//        if (transform == Laps.checkpointA[Laps.currentCheckpoint].transform)
//        {
//            Debug.Log("You just passed a checkpoint");
//            //Check so we dont exceed our checkpoint quantity
//            if (Laps.currentCheckpoint + 1 < Laps.checkpointA.Length)
//            {
//                //Debug.Log("You just passed a checkpoint");
//                //Add to currentLap if currentCheckpoint is 0
//                if (Laps.currentCheckpoint == 0)
//                    Laps.currentLap++;
//                Laps.currentCheckpoint++;
//            }
//            else
//            {
//                //If we dont have any Checkpoints left, go back to 0
//                Laps.currentCheckpoint = 0;
//                alapCompleted = true;
//            }
//            Visual();
//            playerTransform.GetComponent<Laps>().checkpointNum.text = "Checkpoints: " + Laps.currentCheckpoint;
//            playerTransform.GetComponent<Laps>().lapNum.text = "Lap Number: " + Laps.currentLap;

//            // Set the respawn point for the ship to the last checkpoint
//            playerTransform.GetComponentInParent<PlayerHealth>().respawnPoint = Laps.checkpointA[Laps.currentCheckpoint - 1].transform;

//            // Set the tempNode for the AI to the currentNode at the checkpoint (when they respawn, currentNode gets set to tempNode)
//            playerTransform.GetComponentInParent<ShipAI>().tempNode = playerTransform.GetComponentInParent<ShipAI>().currentNode;
//        }


//    }
//    void Visual ()
//    {
//        foreach (Transform checkpoint in playerTransform.GetComponent<Laps>().checkPointArray)
//        {

//            Color color = checkpoint.GetComponent<Renderer>().material.color;
//            color.a = 0.2f;
//            checkpoint.GetComponent<Renderer>().material.color = color;




//        }
//        Color otherColor = playerTransform.GetComponent<Laps>().checkPointArray[Laps.currentCheckpoint].GetComponent<Renderer>().material.color;
//        otherColor.a = 0.8f;
//        playerTransform.GetComponent<Laps>().checkPointArray[Laps.currentCheckpoint].GetComponent<Renderer>().material.color = otherColor;



//    }





//}


   