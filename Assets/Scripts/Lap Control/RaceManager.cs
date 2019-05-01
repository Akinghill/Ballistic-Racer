using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    GameObject[] checksGO;

    public List<Checkpoint_v3> checks = new List<Checkpoint_v3>();

    public int numberOfRacers;

    void Start()
    {
        StartCoroutine(FindShips());

        checksGO = GameObject.FindGameObjectsWithTag("Ship");

        foreach (GameObject check in checksGO)
        {
            checks.Add(check.GetComponentInParent<Checkpoint_v3>());
        }

        numberOfRacers = checksGO.Length;
    }

    IEnumerator FindShips()
    {
        yield return new WaitForEndOfFrame();
    }

    void Update()
    {
        checks.Sort(delegate (Checkpoint_v3 ship1, Checkpoint_v3 ship2)
        {
            if (ship1.currentLap > ship2.currentLap) return -1;
            else if (ship1.currentLap < ship2.currentLap) return 1;
            else if (ship1.currentLap == ship2.currentLap)
            {
                if (ship1.currentCheckpoint > ship2.currentCheckpoint) return -1;
                else if (ship1.currentCheckpoint < ship2.currentCheckpoint) return 1;
                else if (ship1.currentCheckpoint == ship2.currentCheckpoint)
                {
                    if (ship1.lastPointDistance > ship2.lastPointDistance) return -1;
                    else if (ship1.lastPointDistance < ship2.lastPointDistance) return 1;
                    else return 0;
                }
                else return 0;
            }
            else return 0;
        });
    }
}

   
