using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScreen : MonoBehaviour {

    public Text firstPlaceTime;
    public Text secondPlaceTime;
    public Text thirdPlaceTime;
    public Text fourthPlaceTime;
    public Text fifthPlaceTime;
    public Text sixthPlaceTime;

    public GameObject results;

    GameObject finish;
    FinishLap finishLap;

    GameObject rankingManager;
    RaceManager raceManager;

    private void Start()
    {
        finish = GameObject.FindGameObjectWithTag("Finish");
        finishLap = finish.GetComponent<FinishLap>();

        rankingManager = GameObject.Find("Ranking Manager");
        raceManager = rankingManager.GetComponent<RaceManager>();
    }

    private void Update()
    {
        if (finishLap.isFinished)
        {
            firstPlaceTime.text = raceManager.ships[0].timerText.text;
            secondPlaceTime.text = raceManager.ships[1].timerText.text;
            thirdPlaceTime.text = raceManager.ships[2].timerText.text;
            fourthPlaceTime.text = raceManager.ships[3].timerText.text;
            fifthPlaceTime.text = raceManager.ships[4].timerText.text;
            sixthPlaceTime.text = raceManager.ships[5].timerText.text;
        }

        if (finishLap.showResults)
        {
            results.SetActive(true);
        }
    }
}
