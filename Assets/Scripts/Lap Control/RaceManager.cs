using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    GameObject[] shipsGO;

    public List<Checkpoint_v3> ships = new List<Checkpoint_v3>();

    public int numberOfRacers;

    GameObject finish;
    FinishLap finishLap;

    void Start()
    {
        StartCoroutine(FindShips());

        shipsGO = GameObject.FindGameObjectsWithTag("Ship");

        foreach (GameObject check in shipsGO)
        {
            ships.Add(check.GetComponentInParent<Checkpoint_v3>());
        }

        numberOfRacers = shipsGO.Length;

        finish = GameObject.FindGameObjectWithTag("Finish");
        finishLap = finish.GetComponent<FinishLap>();
    }

    IEnumerator FindShips()
    {
        yield return new WaitForEndOfFrame();
    }

    void Update()
    {
        ships.Sort(delegate (Checkpoint_v3 ship1, Checkpoint_v3 ship2)
        {
            if (ship1.currentLap > finishLap.numberOfLaps || ship2.currentLap > finishLap.numberOfLaps) return 0;
            if (ship1.currentLap > ship2.currentLap) return -1;
            if (ship1.currentLap < ship2.currentLap) return 1;
            if (ship1.currentLap == ship2.currentLap)
            {
                if (ship1.currentCheckpoint > ship2.currentCheckpoint) return -1;
                if (ship1.currentCheckpoint < ship2.currentCheckpoint) return 1;
                if (ship1.currentCheckpoint == ship2.currentCheckpoint)
                {
                    if (ship1.nextPointDistance < ship2.nextPointDistance) return -1;
                    if (ship1.nextPointDistance > ship2.nextPointDistance) return 1;
                    else return 0;
                }
                else return 0;
            }
            else return 0;
        });
    }
}

   
