using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivation : MonoBehaviour {

    public bool trapActive;
    public GameObject trap;
    public float timer;

    void Start()
    {
        trapActive = false;
    }

    void Update()
    {
        if (trapActive)
        {
            timer -= Time.deltaTime;
            trap.SetActive(true);
            if (timer <= 0)
            {
                trapActive = false;
                trap.SetActive(false);
                timer = 5f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable") && trapActive == false)
        {
            trapActive = true;
        }
    }
}
