using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltCharge : MonoBehaviour {

    public Slider ultimateSlider;

    public Text ultName;

    public int ultPower;

    public int ultPowerMax;

    public float chargeRate;

    public bool ultCharged;

    //public GameObject ships;
    public GameObject empParticles;
    public Collider reflectCollider;

    int powerUpNum;

    //int chargeSpeed;

    //CheckTrigger check;

    // Use this for initialization
    //void Start () {
    //check = GetComponentInChildren<CheckTrigger>();
    //InvokeRepeating("UltimateCharge", chargeRate, chargeRate);
    //}

    // Update is called once per frame
    void Update()
    {
        ultimateSlider.value = ultPower / (ultPowerMax * 1f);
        if(ultPower == ultPowerMax)
        {
            ultCharged = true;
        }
        else
        {
            ultCharged = false;
            ultName.text = " ";
        }
    }
    //chargeSpeed = check.position + 10;

    //if(ultPower == ultPowerMax) {
    //ultCharged = true;
    //} else {
    //ultCharged = false;
    //}
    //if(ultPower > ultPowerMax) {
    //ultPower = ultPowerMax;
    //}
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            ultPower = ultPowerMax;
            ultimateSlider.value = ultPowerMax;
            powerUpNum = Random.Range(0, 4);
            if (powerUpNum == 0)
            {
                GetComponent<Invulnerability>().enabled = true;
                empParticles.GetComponent<EmpController>().enabled = false;
                GetComponent<HyperSpeed>().enabled = false;
                reflectCollider.GetComponent<ReflectorShield>().enabled = false;
                ultName.text = "Invulnerability";
            }
            if (powerUpNum == 1)
            {
                GetComponent<Invulnerability>().enabled = false;
                empParticles.GetComponent<EmpController>().enabled = true;
                GetComponent<HyperSpeed>().enabled = false;
                reflectCollider.GetComponent<ReflectorShield>().enabled = false;
                ultName.text = "EMP Burst";
            }
            if (powerUpNum == 2)
            {
                GetComponent<Invulnerability>().enabled = false;
                empParticles.GetComponent<EmpController>().enabled = false;
                GetComponent<HyperSpeed>().enabled = true;
                reflectCollider.GetComponent<ReflectorShield>().enabled = false;
                ultName.text = "Hyper Speed";
            }
            if (powerUpNum == 3)
            {
                GetComponent<Invulnerability>().enabled = false;
                empParticles.GetComponent<EmpController>().enabled = false;
                GetComponent<HyperSpeed>().enabled = false;
                reflectCollider.GetComponent<ReflectorShield>().enabled = true;
                ultName.text = "Reflector Shield";
            }
        }
    }


    //void UltimateCharge() {
        //ultPower += chargeSpeed;
    //}
}
