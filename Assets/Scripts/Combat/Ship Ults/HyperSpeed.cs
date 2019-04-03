using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperSpeed : MonoBehaviour
{
    public float boostTime;

    public float maxBoostAcceleration;
    public float maxBoostSpeed;

    PlayerInput input;
    UltCharge ultCharge;
    ShipMovement movement;

    float returnAcceleration;
    float returnMaxSpeed;

    bool boosting;
    // Use this for initialization
    void Start()
    {
        movement = GetComponent<ShipMovement>();
        ultCharge = GetComponent<UltCharge>();
        input = GetComponent<PlayerInput>();

        returnAcceleration = movement.acceleration;
        returnMaxSpeed = movement.maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //if (ultCharge.ultCharged)
        //{
        if (input.powerUp)
        {
            boosting = true;
            StartCoroutine(SpeedBoost());
            //ultCharge.CancelInvoke("UltimateCharge");

            ultCharge.ultPower = 0;
            GetComponent<HyperSpeed>().enabled = false;
        }
        //}
        if (boosting)
        {
            input.boost = false;
        }
    }

    IEnumerator SpeedBoost()
    {
        movement.acceleration = maxBoostAcceleration;
        movement.maxSpeed = maxBoostSpeed;

        yield return new WaitForSeconds(boostTime);
        //ultCharge.InvokeRepeating("UltimateCharge", ultCharge.chargeRate, ultCharge.chargeRate);
        movement.acceleration = returnAcceleration;
        movement.maxSpeed = returnMaxSpeed;
        boosting = false;

    }
}
