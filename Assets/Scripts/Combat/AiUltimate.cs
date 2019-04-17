using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiUltimate : MonoBehaviour
{
    //UltCharge ultCharge;
    //PlayerInput playerInput;

    //HyperSpeed hyperSpeed;
    //Mine mine;

    //Invulnerability invulnerability;
    //ReflectorShield reflectorShield;

    //BulletHell bulletHell;
    //EmpController empController;
    //PlayerShooting playerShooting;

    void Update ()
    {
        AiUseUltimate();
    }

    void AiUseUltimate ()
    {
        if (GetComponent<UltCharge>().ultCharged) // If the ultimate is charged...
        {
            if(GetComponent<HyperSpeed>().enabled /*|| mine*/) // ... and is the HyperSpeed or Mine powerup...
            {
                GetComponent<PlayerInput>().powerUp = true; // ...Trigger the ultimate
            }

            if (GetComponent<Invulnerability>().enabled || GetComponentInChildren<ReflectorShield>().enabled) // ... and is the invulnerability or reflectorShield powerup...
            {
                if (GetComponentInChildren<PlayerHealth>().currentHealth < 100) // ... and is taking damage...
                {
                    GetComponent<PlayerInput>().powerUp = true; // ...Trigger the ultimate
                }
            }

            if(/*bulletHell ||*/ GetComponentInChildren<EmpController>().enabled) // ... and is BulletHell or EmpController...
            {
                if (GetComponentInChildren<PlayerShooting>().gunLine.enabled) // and can shoot...
                {
                    GetComponent<PlayerInput>().powerUp = true; // ...Trigger the ultimate
                }
            }
        }
    }
}
