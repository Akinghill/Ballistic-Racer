﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorShield : MonoBehaviour {

    //To add this into the scene on a ship, a new mesh collider needs to be made.
    //Whatever ship this goes on, just duplicate the mesh collider and rename it.
    //Then, add this script to it and change the layer to "Ricochet"
    //Finally, disable the new mesh collider by unchecking the box on the component.

    
    PlayerInput input;
    PlayerHealth health;
    Collider ReflectCollider;
    UltCharge ultCharge;

    // Use this for initialization
    void Awake() {
        ultCharge = GetComponentInParent<UltCharge>();
        input = GetComponentInParent<PlayerInput>();
        health = GetComponentInParent<PlayerHealth>();
        ReflectCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update() {

        if(ultCharge.ultCharged) {
            if(input.powerUp) {
                ReflectShield();

            }
        }
    }

    public void ReflectShield() {
        ReflectCollider.enabled = true;
        health.ReflectorShield();
        if(health.StopReflect() == false) {
            ReflectCollider.enabled = false;
        }
    }
}
