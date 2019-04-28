//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;


//public class Checkpoint : MonoBehaviour
//{
//    public float restartDelay = 5f;
//    public float restartTimer;
//    public int numberOfLaps;
//    public bool isFinished = false;
//    public int finishes = 0;

//    private void Update()
//    {
//        if(isFinished == true)
//        {
//            restartTimer += Time.deltaTime;
//            if (restartTimer >= restartDelay)
//            {
//                ReturnToMenu();
//            }
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Check")
//        {
//            Debug.Log("Passed");
//            CheckTrigger checkTrigger = other.GetComponent<CheckTrigger>();
//            if (checkTrigger.checkpointReached)
//            {
//                Debug.Log("Passed");
//                CompleteLap(checkTrigger);
//            }
//        }
//    }

//    public void CompleteLap(CheckTrigger checkTrigger)
//    {
//        if (checkTrigger.lapsCompleted < numberOfLaps)
//        {
//            checkTrigger.lapsCompleted++;
//            checkTrigger.checkpointReached = false;
//        }
//        else
//        {
//            checkTrigger.finish.SetActive(true);
//            finishes++;
//            if(PlayerManager.numOfPlayers == 1 && finishes == 1)
//            {
//                isFinished = true;
//            }
//            if(PlayerManager.numOfPlayers == 2 && finishes == 2)
//            {
//                isFinished = true;
//            }
//            if (PlayerManager.numOfPlayers == 3 && finishes == 3)
//            {
//                isFinished = true;
//            }
//            if (PlayerManager.numOfPlayers == 4 && finishes == 4)
//            {
//                isFinished = true;
//            }
//        }
//    }

//    void ReturnToMenu()
//    {
//        SceneManager.LoadScene("MainMenu");
//    }
//}